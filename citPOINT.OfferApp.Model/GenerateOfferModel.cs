#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel.Composition;
using System.Threading;
using citPOINT.OfferApp.Data.Web;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.Data.Web.PrefAppService;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 04.03.12     Yousra         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.OfferApp.Model
{
    #region  Using MEF to export GenerateOfferModel
    /// <summary>
    /// Model Layer for Generate Offer Model.
    /// </summary>
    [Export(typeof(IGenerateOfferModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class GenerateOfferModel : IGenerateOfferModel
    {
        #region → Fields         .
        private OfferAppContext mContext;
        private Boolean mHasChanges = false;
        private Boolean mIsBusy = false;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private OfferAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    if (mContext == null)
                    {
                        mContext = new OfferAppContext(OfferAppConfigurations.MainServiceUri);
                    }
                    mContext.PropertyChanged += new PropertyChangedEventHandler(ctx_PropertyChanged);
                }

                return mContext;
            }
        }

        /// <summary>
        /// True if _ctx.HasChanges is true; otherwise, false
        /// </summary>
        public Boolean HasChanges
        {
            get
            {
                return this.mHasChanges;
            }

            private set
            {
                if (this.mHasChanges != value)
                {
                    this.mHasChanges = value;
                    this.OnPropertyChanged("HasChanges");
                }
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "HasChanges":
                    this.HasChanges = mContext.HasChanges;
                    break;
                case "IsLoading":
                    this.IsBusy = mContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mContext.IsSubmitting;
                    break;
            }
        }
        #endregion

        #region → Events         .

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
 
        /// <summary>
        /// Occurs when [get conversation offer setting complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ConversationOfferSetting>> GetConversationOfferSettingComplete;

        /// <summary>
        /// Occurs when [get negotiation offer setting complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<NegotiationOfferSetting>> GetNegotiationOfferSettingComplete;

        /// <summary>
        /// Occurs when [get preference set complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OfferDetails>> GetPreferenceSetComplete;

        /// <summary>
        /// Occurs when [generate offer for negotiation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OfferDetails>> GenerateOfferForNegotiationComplete;

        /// <summary>
        /// Occurs when [generate offer for conversation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<OfferDetails>> GenerateOfferForConversationComplete;

        /// <summary>
        /// Occurs when [get next expected target for negotiation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ExpectedTarget>> GetNextExpectedTargetForNegotiationComplete;

        /// <summary>
        /// Occurs when [get next expected target for conversation complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<ExpectedTarget>> GetNextExpectedTargetForConversationComplete;

        /// <summary>
        /// SaveChangesComplete
        /// </summary>
        public event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {
            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        #endregion

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation

        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion

        #region → Public         .


        /// <summary>
        /// Adds the conversation offer settings.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="negOfferSettings">The neg offer settings.</param>
        /// <returns></returns>
        public ConversationOfferSetting AddConversationOfferSettings(bool setInContext, NegotiationOfferSetting negOfferSettings)
        {
            ConversationOfferSetting ConvOfferSettings = new ConversationOfferSetting()
            {
                ConversationOfferSettingID = Guid.NewGuid(),
                NegotiationOfferSettingID = negOfferSettings.NegotiationOfferSettingID,
                ConversationID = OfferAppConfigurations.ConversationIDParameter,
                TargetTypeID = negOfferSettings.TargetTypeID,
                BaseOfferTypeID = negOfferSettings.BaseOfferTypeID,
                TargetValue = negOfferSettings.TargetValue,
                DeletedBy = OfferAppConfigurations.CurrentLoginUser.UserID,
                Deleted = false,
                DeletedOn = DateTime.Now
            };

            if (setInContext)
            {
                Context.ConversationOfferSettings.Add(ConvOfferSettings);
            }

            return ConvOfferSettings;
        }

        /// <summary>
        /// Adds the negotiation offer settings.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        public NegotiationOfferSetting AddNegotiationOfferSettings(bool setInContext)
        {
            NegotiationOfferSetting NegOfferSettings = new NegotiationOfferSetting()
            {
                NegotiationOfferSettingID = Guid.NewGuid(),
                NegotiationID = OfferAppConfigurations.NegotiationIDParameter,
                TargetTypeID = (int)TargetTypeOptions.UserDefined,
                BaseOfferTypeID = (int)OfferType.Own,
                TargetValue = 100,
                DeletedBy = OfferAppConfigurations.CurrentLoginUser.UserID,
                Deleted = false,
                DeletedOn = DateTime.Now
            };

            if (setInContext)
            {
                Context.NegotiationOfferSettings.Add(NegOfferSettings);
            }

            return NegOfferSettings;
        }

          /// <summary>
        /// Gets the conversation offer setting async.
        /// </summary>
        public void GetConversationOfferSettingAsync()
        {
            PerformQuery<ConversationOfferSetting>(Context.GetConversationOfferSettingsQuery()
                .Where(s => s.Deleted == false && s.ConversationID == OfferAppConfigurations.ConversationIDParameter),
                GetConversationOfferSettingComplete);
        }

        /// <summary>
        /// Gets the negotiation offer setting async.
        /// </summary>
        public void GetNegotiationOfferSettingAsync()
        {
            PerformQuery<NegotiationOfferSetting>(Context.GetNegotiationOfferSettingsQuery()
                .Where(s => s.Deleted == false && s.NegotiationID == OfferAppConfigurations.NegotiationIDParameter),
                GetNegotiationOfferSettingComplete);
        }

        /// <summary>
        /// Gets the preference set async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        public void GetPreferenceSetAsync(Guid negotiationID)
        {
            PerformQuery<OfferDetails>(Context.GetPreferenceSetQuery(negotiationID), GetPreferenceSetComplete);
        }

        /// <summary>
        /// Gets the next expected target for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <param name="maxPercentage">The max percentage.</param>
        /// <param name="currentDate">The current date.</param>
        /// <param name="targetType">Type of the target.</param>
        public void GetNextExpectedTargetForNegotiationAsync(Guid negotiationID, OfferType offerType, decimal maxPercentage, DateTime currentDate, TargetTypeOptions targetType)
        {
            PerformQuery<ExpectedTarget>(Context.GetNextExpectedTargetForNegotiationQuery(negotiationID, offerType, maxPercentage, currentDate, targetType), GetNextExpectedTargetForNegotiationComplete);
        }

        /// <summary>
        /// Gets the next expected target for conversation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <param name="maxPercentage">The max percentage.</param>
        /// <param name="currentDate">The current date.</param>
        /// <param name="targetType">Type of the target.</param>
        public void GetNextExpectedTargetForConversationAsync(Guid negotiationID, Guid conversationID, OfferType offerType, decimal maxPercentage, DateTime currentDate, TargetTypeOptions targetType)
        {
            PerformQuery<ExpectedTarget>(Context.GetNextExpectedTargetForConversationQuery(negotiationID, conversationID, offerType, maxPercentage, currentDate, targetType), GetNextExpectedTargetForConversationComplete);
        }

        /// <summary>
        /// Generates the offer for negotiation async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        public void GenerateOfferForNegotiationAsync(Guid negotiationID, decimal target, OfferType offerType)
        {
            PerformQuery<OfferDetails>(Context.GetGeneratedOfferForNegotiationQuery(negotiationID, target, offerType), GenerateOfferForNegotiationComplete);
        }

        /// <summary>
        /// Generates the offer for conversation async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        public void GenerateOfferForConversationAsync(Guid negotiationID, Guid conversationID, decimal target, OfferType offerType)
        {
            PerformQuery<OfferDetails>(Context.GetGeneratedOfferForConversationQuery(negotiationID, conversationID, target, offerType), GenerateOfferForConversationComplete);
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            this.Context.SubmitChanges(s =>
            {
                if (SaveChangesComplete != null)
                {
                    try
                    {
                        Exception ex = null;
                        if (s.HasError)
                        {
                            ex = s.Error;
                            s.MarkErrorAsHandled();
                        }
                        SaveChangesComplete(this, new SubmitOperationEventArgs(s, ex));
                    }
                    catch (Exception ex)
                    {
                        SaveChangesComplete(this, new SubmitOperationEventArgs(ex));
                    }
                }
            }, null);
        }

        /// <summary>
        /// Reject all changes happen on current Context
        /// </summary>
        public void RejectChanges()
        {
            this.Context.RejectChanges();
        }

        #endregion

        #endregion


    }
}
