#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using citPOINT.eNeg.Common;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.Data.Web;
using citPOINT.OfferApp.Data.Web.PrefAppService;
using citPOINT.OfferApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 04.03.12     M.Wahab     • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.OfferApp.ViewModel
{
    #region → Using  MEF to export GenerateOfferViewModel
    /// <summary>
    /// this class is to Message Template View Model.
    /// </summary>
    [Export(OfferAppViewModelTypes.GenerateOfferViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class GenerateOfferViewModel : ViewModelBase
    {
        #region → Fields         .

        private IGenerateOfferModel mGenerateOfferModel;

        private bool mIsGenerateOfferBusy = false;
        private bool RunQueueForApplyChanges;
        private bool mIsBusy;


        private List<OfferDetails> mOfferSource;

        private NegotiationOfferSetting mCurrentNegotiationOfferSetting;
        private ConversationOfferSetting mCurrentConversationOfferSetting;

        private RelayCommand mGenerateEmailCommand;
        private RelayCommand mCopyToClipboardCommand;
        private RelayCommand mGenerateOfferCommand;
        private RelayCommand mOpenGenerateOfferViewCommand;
        private RelayCommand mExitGenerateOfferCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return mIsBusy; }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");

                if (!this.IsBusy)
                {
                    if (RunQueueForApplyChanges)
                    {
                        ApplyChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is generate offer busy.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is generate offer busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsGenerateOfferBusy
        {
            get
            {
                return mIsGenerateOfferBusy;
            }
            set
            {
                mIsGenerateOfferBusy = value;
                this.RaisePropertyChanged("IsGenerateOfferBusy");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [preference set exist].
        /// </summary>
        /// <value><c>true</c> if [preference set exist]; otherwise, <c>false</c>.</value>
        public bool PreferenceSetExist { get; set; }

        /// <summary>
        /// Gets or sets the preference set max percentage.
        /// </summary>
        /// <value>The preference set max percentage.</value>
        public decimal PreferenceSetMaxPercentage { get; set; }

        /// <summary>
        /// Gets or sets the message source.
        /// </summary>
        /// <value>The message source.</value>
        public List<OfferDetails> OfferSource
        {
            get { return mOfferSource; }
            set
            {
                mOfferSource = value;
                this.RaisePropertyChanged("OfferSource");
            }
        }

        /// <summary>
        /// Gets or sets the current negotiation offer setting.
        /// </summary>
        /// <value>The current negotiation offer setting.</value>
        public NegotiationOfferSetting CurrentNegotiationOfferSetting
        {
            get
            {
                return mCurrentNegotiationOfferSetting;
            }

            set
            {
                mCurrentNegotiationOfferSetting = value;
                this.RaisePropertyChanged("CurrentNegotiationOfferSetting");
                this.RaisePropertyChanged("CurrentOfferSetting");

            }
        }

        /// <summary>
        /// Gets or sets the current conversation offer setting.
        /// </summary>
        /// <value>The current conversation offer setting.</value>
        public ConversationOfferSetting CurrentConversationOfferSetting
        {
            get
            {
                return mCurrentConversationOfferSetting;
            }

            set
            {
                mCurrentConversationOfferSetting = value;
                this.RaisePropertyChanged("CurrentConversationOfferSetting");
                this.RaisePropertyChanged("CurrentOfferSetting");
            }
        }

        /// <summary>
        /// Gets the current offer setting.
        /// </summary>
        /// <value>The current offer setting.</value>
        public IOfferSetting CurrentOfferSetting
        {
            get
            {
                if (OfferAppConfigurations.ConversationIDParameter == Guid.Empty)
                {
                    return this.CurrentNegotiationOfferSetting;
                }
                else
                {
                    return this.CurrentConversationOfferSetting;
                }
            }
        }

        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateOfferViewModel"/> class.
        /// </summary>
        /// <param name="GenerateOfferModel">The five dimension model.</param>
        [ImportingConstructor]
        public GenerateOfferViewModel(IGenerateOfferModel GenerateOfferModel)
        {
            this.mGenerateOfferModel = GenerateOfferModel;

            #region → Set up event handling       .
            this.mGenerateOfferModel.GetPreferenceSetComplete += new EventHandler<eNegEntityResultArgs<OfferDetails>>(mGenerateOfferModel_GetPreferenceSetComplete);

            //Get Offer sttings.
            this.mGenerateOfferModel.GetNegotiationOfferSettingComplete += new EventHandler<eNegEntityResultArgs<NegotiationOfferSetting>>(mGenerateOfferModel_GetNegotiationOfferSettingComplete);
            this.mGenerateOfferModel.GetConversationOfferSettingComplete += new EventHandler<eNegEntityResultArgs<ConversationOfferSetting>>(mGenerateOfferModel_GetConversationOfferSettingComplete);

            //Get Next Target
            this.mGenerateOfferModel.GetNextExpectedTargetForNegotiationComplete += new EventHandler<eNegEntityResultArgs<ExpectedTarget>>(mGenerateOfferModel_GetNextExpectedTargetComplete);
            this.mGenerateOfferModel.GetNextExpectedTargetForConversationComplete += new EventHandler<eNegEntityResultArgs<ExpectedTarget>>(mGenerateOfferModel_GetNextExpectedTargetComplete);

            //Generate Offer
            this.mGenerateOfferModel.GenerateOfferForNegotiationComplete += new EventHandler<eNegEntityResultArgs<OfferDetails>>(mGenerateOfferModel_GenerateOfferComplete);
            this.mGenerateOfferModel.GenerateOfferForConversationComplete += new EventHandler<eNegEntityResultArgs<OfferDetails>>(mGenerateOfferModel_GenerateOfferComplete);

            this.mGenerateOfferModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mGenerateOfferModel_PropertyChanged);
            this.mGenerateOfferModel.SaveChangesComplete += new EventHandler<SubmitOperationEventArgs>(mGenerateOfferModel_SaveChangesComplete);

            #endregion

            #region → Load Lookup tables          .

            this.mGenerateOfferModel.GetPreferenceSetAsync(OfferAppConfigurations.NegotiationIDParameter);
            this.mGenerateOfferModel.GetNegotiationOfferSettingAsync();

            if (OfferAppConfigurations.ConversationIDParameter != Guid.Empty)
            {
                this.mGenerateOfferModel.GetConversationOfferSettingAsync();
            }

            #endregion

            #region → Register Messages           .
            #endregion
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Call back of Get preference set complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mGenerateOfferModel_GetPreferenceSetComplete(object sender, eNegEntityResultArgs<OfferDetails> e)
        {
            if (!e.HasError)
            {
                if (e.Results.Count() <= 0)
                {
                    //Incase preference set not exist or no issues in that prefSet.
                    OfferAppMessanger.ChangeScreenMessage.Send(OfferAppViewTypes.PreferenceSetAlarmView);
                }
                else
                {
                    this.OfferSource = e.Results
                                        .OrderBy(s => s.Rank)
                                        .ToList();

                    this.PreferenceSetMaxPercentage = this.OfferSource.FirstOrDefault().MaxPercentage;

                    this.PreferenceSetExist = true;

                    this.RaiseCanExecuteChanged();

                    //Incase preference set not exist or no issues in that prefSet.
                    OfferAppMessanger.ChangeScreenMessage.Send(OfferAppViewTypes.OfferDetailsView);
                }
            }
            else
            {
                OfferAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get conversation offer setting complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mGenerateOfferModel_GetConversationOfferSettingComplete(object sender, eNegEntityResultArgs<ConversationOfferSetting> e)
        {
            if (!e.HasError)
            {
                if (e.Results.Count() <= 0)
                {
                    this.CurrentConversationOfferSetting = new ConversationOfferSetting();
                    this.AddConversationOfferSettings();
                }
                else
                {
                    this.CurrentConversationOfferSetting = e.Results
                                                            .FirstOrDefault();
                }
            }
            else
            {
                OfferAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get negotiation offer setting complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mGenerateOfferModel_GetNegotiationOfferSettingComplete(object sender, eNegEntityResultArgs<NegotiationOfferSetting> e)
        {
            if (!e.HasError)
            {
                if (e.Results.Count() <= 0)
                {
                    this.CurrentNegotiationOfferSetting = this.mGenerateOfferModel.AddNegotiationOfferSettings(true);
                }
                else
                {
                    this.CurrentNegotiationOfferSetting = e.Results.FirstOrDefault();
                }

                //Add Conversation settings for current Conversation like its negotiation
                this.AddConversationOfferSettings();
            }
            else
            {
                OfferAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Generate offer complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mGenerateOfferModel_GenerateOfferComplete(object sender, eNegEntityResultArgs<OfferDetails> e)
        {
            this.IsGenerateOfferBusy = false;

            if (!e.HasError)
            {
                if (e.Results.Count() <= 0)
                {
                    eNeg.Common.eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.TargetFailed, ImageType.Error));
                }
                else
                {
                    OfferAppMessanger.ChangeScreenMessage.Send(OfferAppViewTypes.ClosePopupView);

                    this.OfferSource = e.Results.OrderBy(s => s.Rank).ToList();
                }

                if (this.mGenerateOfferModel.HasChanges)
                {
                    this.OnSubmitChanges(true);
                }
            }
            else
            {
                OfferAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Call back of Get next expected target complete.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mGenerateOfferModel_GetNextExpectedTargetComplete(object sender, eNegEntityResultArgs<ExpectedTarget> e)
        {
            if (!e.HasError)
            {
                if (e.Results.Count() <= 0)
                {
                    eNeg.Common.eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.TargetFailed, ImageType.Error));
                }
                else
                {
                    ExpectedTarget expectedTarget = e.Results.FirstOrDefault();

                    switch (expectedTarget.Status)
                    {
                        case Status.Success:
                            this.CurrentOfferSetting.TargetValue = expectedTarget.Target;
                            this.GenerateOffer();
                            break;
                        case Status.Failed:
                            eNeg.Common.eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.TargetFailed, ImageType.Error));
                            break;
                        case Status.DateOutOfPeriod:
                            eNeg.Common.eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.TargetOutOfDate, ImageType.Error));
                            break;
                        case Status.NoSettings:
                            eNeg.Common.eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.TargetNoSettings, ImageType.Error));
                            break;
                        default:
                            eNeg.Common.eNegMessanger.SendCustomMessage.Send(new eNegMessage(Resources.TargetFailed, ImageType.Error));
                            break;
                    }
                }
            }
            else
            {
                OfferAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the mGenerateOfferModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mGenerateOfferModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Handles the SaveChangesComplete event of the mGenerateOfferModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="citPOINT.eNeg.Common.SubmitOperationEventArgs"/> instance containing the event data.</param>
        private void mGenerateOfferModel_SaveChangesComplete(object sender, SubmitOperationEventArgs e)
        {
            if (!e.HasError)
            {
                this.RaiseCanExecuteChanged();
            }
            else
            {
                // notify user if there is any error
                OfferAppMessanger.RaiseErrorMessage.Send(e.Error);
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the open generate offer view command.
        /// </summary>
        /// <value>The open generate offer view command.</value>
        public RelayCommand OpenGenerateOfferViewCommand
        {
            get
            {
                if (mOpenGenerateOfferViewCommand == null)
                {
                    mOpenGenerateOfferViewCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mGenerateOfferModel.IsBusy && this.PreferenceSetExist)
                            {
                                OfferAppMessanger.ChangeScreenMessage.Send(OfferAppViewTypes.GenerateOfferView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            OfferAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mGenerateOfferModel.IsBusy &&
                             this.PreferenceSetExist
                    );
                }
                return mOpenGenerateOfferViewCommand;
            }
        }

        /// <summary>
        /// Gets the exit generate offer command.
        /// </summary>
        /// <value>The exit generate offer command.</value>
        public RelayCommand ExitGenerateOfferCommand
        {
            get
            {
                if (mExitGenerateOfferCommand == null)
                {
                    mExitGenerateOfferCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mGenerateOfferModel.IsBusy && this.PreferenceSetExist)
                            {
                                OfferAppMessanger.ChangeScreenMessage.Send(OfferAppViewTypes.ClosePopupView);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            OfferAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mGenerateOfferModel.IsBusy &&
                             this.PreferenceSetExist
                    );
                }
                return mExitGenerateOfferCommand;
            }
        }

        /// <summary>
        /// Gets the generate offer command.
        /// </summary>
        /// <value>The generate offer command.</value>
        public RelayCommand GenerateOfferCommand
        {
            get
            {
                if (mGenerateOfferCommand == null)
                {
                    mGenerateOfferCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mGenerateOfferModel.IsBusy &&
                                this.IsOfferSettingValid())
                            {
                                //Chack Target Type
                                if (this.CurrentOfferSetting.IsUserDefined)
                                {
                                    this.GenerateOffer();
                                }
                                else
                                {
                                    if (OfferAppConfigurations.ConversationIDParameter == Guid.Empty)
                                    {
                                        this.GetNextExpectedTargetForNegotiationAsync();
                                    }
                                    else
                                    {
                                        this.GetNextExpectedTargetForConversationAsync();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            OfferAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => !mGenerateOfferModel.IsBusy &&
                             this.PreferenceSetExist
                    );
                }
                return mGenerateOfferCommand;
            }
        }

        /// <summary>
        /// Gets the generate email command.
        /// </summary>
        /// <value>The generate email command.</value>
        public RelayCommand GenerateEmailCommand
        {
            get
            {
                if (mGenerateEmailCommand == null)
                {
                    mGenerateEmailCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!mGenerateOfferModel.IsBusy)
                            {
                                string content = GetOfferDetailsAsString();

                                content = HttpUtility.UrlEncode(content).Replace("+", "%20");

                                string url = string.Format("mailto:?body={0}", content);

                                eNeg.Common.eNegNavigation.NavigateToUrl(url, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            OfferAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.PreferenceSetExist);
                }
                return mGenerateEmailCommand;
            }
        }

        /// <summary>
        /// Gets the copy to clipboard command.
        /// </summary>
        /// <value>The copy to clipboard command.</value>
        public RelayCommand CopyToClipboardCommand
        {
            get
            {
                if (mCopyToClipboardCommand == null)
                {
                    mCopyToClipboardCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            string content = GetOfferDetailsAsString().Replace("\r", "\r\n");

                            Clipboard.SetText(content);
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            OfferAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => this.PreferenceSetExist);
                }
                return mCopyToClipboardCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [submit changes].
        /// </summary>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        private void OnSubmitChanges(Boolean flag)
        {
            this.mGenerateOfferModel.SaveChangesAsync();
        }

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        private void RaiseCanExecuteChanged()
        {
            this.OpenGenerateOfferViewCommand.RaiseCanExecuteChanged();
            this.GenerateOfferCommand.RaiseCanExecuteChanged();
            this.GenerateEmailCommand.RaiseCanExecuteChanged();
            this.CopyToClipboardCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Gets the offer details as string.
        /// </summary>
        /// <returns></returns>
        private string GetOfferDetailsAsString()
        {
            StringBuilder sp = new StringBuilder("");

            foreach (var issue in this.OfferSource)
            {
                sp.AppendLine(string.Format("{0} : {1}", issue.IssueName, issue.Value));
            }
            return sp.ToString();
        }

        /// <summary>
        /// Adds the conversation offer settings.
        /// Solve problem in case Conversation Settings return before Negotiation settings.
        /// </summary>
        private void AddConversationOfferSettings()
        {
            if (OfferAppConfigurations.ConversationIDParameter != Guid.Empty &&
                this.CurrentNegotiationOfferSetting != null)
            {
                //Check if the Conversation Settings Exist otr not.
                if (this.CurrentConversationOfferSetting != null &&
                    this.CurrentConversationOfferSetting.NegotiationOfferSettingID == null)
                {
                    this.CurrentConversationOfferSetting = this.mGenerateOfferModel.AddConversationOfferSettings(true, this.CurrentNegotiationOfferSetting);
                }
            }

        }

        /// <summary>
        /// Generates the offer.
        /// </summary>
        private void GenerateOffer()
        {
            this.IsGenerateOfferBusy = true;

            if (OfferAppConfigurations.ConversationIDParameter == Guid.Empty)
            {
                this.mGenerateOfferModel.GenerateOfferForNegotiationAsync(OfferAppConfigurations.NegotiationIDParameter, this.CurrentOfferSetting.TargetValue.Value, (OfferType)this.CurrentOfferSetting.BaseOfferTypeID.Value);
            }
            else
            {
                this.mGenerateOfferModel.GenerateOfferForConversationAsync(OfferAppConfigurations.NegotiationIDParameter, OfferAppConfigurations.ConversationIDParameter, this.CurrentOfferSetting.TargetValue.Value, (OfferType)this.CurrentOfferSetting.BaseOfferTypeID.Value);
            }
        }

        /// <summary>
        /// Determines whether [is offer setting valid].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is offer setting valid]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsOfferSettingValid()
        {
            bool isAllValid = true;

            this.CurrentOfferSetting.ValidationErrors.Clear();

            if (!this.CurrentOfferSetting.TryValidateObject())
            {
                isAllValid = false;
            }

            return isAllValid;
        }

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        private void RejectChanges()
        {
            //this.mGenerateOfferModel.RejectChanges();
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the next expected target for negotiation async.
        /// </summary>
        public void GetNextExpectedTargetForNegotiationAsync()
        {
            this.mGenerateOfferModel.GetNextExpectedTargetForNegotiationAsync(
                    OfferAppConfigurations.NegotiationIDParameter,
                    (OfferType)this.CurrentOfferSetting.BaseOfferTypeID.Value,
                    this.PreferenceSetMaxPercentage,
                    DateTime.Now,
                    (TargetTypeOptions)this.CurrentOfferSetting.TargetTypeID.Value);
        }

        /// <summary>
        /// Gets the next expected target for conversation async.
        /// </summary>
        public void GetNextExpectedTargetForConversationAsync()
        {
            this.mGenerateOfferModel.GetNextExpectedTargetForConversationAsync(
                    OfferAppConfigurations.NegotiationIDParameter,
                    OfferAppConfigurations.ConversationIDParameter,
                    (OfferType)this.CurrentOfferSetting.BaseOfferTypeID.Value,
                    this.PreferenceSetMaxPercentage,
                    DateTime.Now,
                    (TargetTypeOptions)this.CurrentOfferSetting.TargetTypeID.Value);
        }

        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            RunQueueForApplyChanges = true;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (!this.IsBusy)
                {
                    RunQueueForApplyChanges = false;

                    this.mGenerateOfferModel.RejectChanges();

                    this.mGenerateOfferModel.GetPreferenceSetAsync(OfferAppConfigurations.NegotiationIDParameter);
                    this.mGenerateOfferModel.GetNegotiationOfferSettingAsync();

                    if (OfferAppConfigurations.ConversationIDParameter != Guid.Empty)
                    {
                        this.mGenerateOfferModel.GetConversationOfferSettingAsync();
                    }
                }
            });
        }

        #endregion

        #endregion
    }
}