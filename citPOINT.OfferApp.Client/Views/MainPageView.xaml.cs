#region → Usings   .
using citPOINT.eNeg.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.ViewModel;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Apps.Common.Interfaces;
using Telerik.Windows.Controls;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 05.03.12   Yousra reda       Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.OfferApp.Client
{
    /// <summary>
    /// Main Page View
    /// </summary>
    [Export]
    public partial class MainPageView : UserControl, IObserverApp
    {
        #region → Fields         .

        private OfferDetailsView mOfferDetailsView;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the view model repository.
        /// </summary>
        /// <value>The view model repository.</value>
        private ViewModelRepository ViewModelRepository { get; set; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get { return OfferAppConfigurations.AppName; }
        }

        /// <summary>
        /// Gets the offer details view.
        /// </summary>
        /// <value>The offer details view.</value>
        public OfferDetailsView OfferDetailsView
        {
            get
            {
                if (mOfferDetailsView == null)
                {
                    mOfferDetailsView = new OfferDetailsView();
                }
                return mOfferDetailsView;
            }
        }
        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageView"/> class.
        /// </summary>
        public MainPageView()
        {
            InitializeComponent();

            #region Register needed messages

            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                OfferAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreen);

                OfferAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);
            }
            #endregion
            
            try
            {
                this.ApplyChanges(false);

                OfferAppConfigurations.MainPlatformInfo.TrackChanges.AddObserverApp(this);
            }
            catch (Exception ex)
            {
                OfferAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, OfferAppConfigurations.AppName);
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex"></param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            OfferAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, OfferAppConfigurations.AppName);
        }

        /// <summary>
        /// Called when [change screen].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnChangeScreen(string PageName)
        {
            switch (PageName)
            {
                case OfferAppViewTypes.OfferDetailsView:
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    this.uxMainContent.Content = OfferDetailsView;
                    break;

                case OfferAppViewTypes.PreferenceSetAlarmView:
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    this.uxMainContent.Content = new PrefSetAlarmView();
                    break;

                case OfferAppViewTypes.GenerateOfferView:
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    PopUpWindow OfferSettingsPopUp = new PopUpWindow("Generate Offer");
                    OfferSettingsPopUp.DataContext = this.DataContext;
                    OfferSettingsPopUp.Content = new GenerateOfferView();
                    OfferSettingsPopUp.ShowDialog();
                    break;
            }
        }

        #endregion

        #region → public         .

        /// <summary>
        /// Applies the changes.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public void ApplyChanges(bool isActive)
        {
            if (isActive)
            {
                this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;

                #region → Change Negotiation      .

                if (OfferAppConfigurations.MainPlatformInfo.CurrentNegotiation != null)
                {
                    OfferAppConfigurations.NegotiationIDParameter = OfferAppConfigurations.MainPlatformInfo.CurrentNegotiation.NegotiationID;
                }
                else
                {
                    OfferAppConfigurations.NegotiationIDParameter = Guid.Empty;
                }

                #endregion

                #region → Change Conversation     .

                if (OfferAppConfigurations.MainPlatformInfo.CurrentConversation != null)
                {
                    OfferAppConfigurations.ConversationIDParameter = OfferAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;
                }
                else
                {
                    OfferAppConfigurations.ConversationIDParameter = Guid.Empty;
                }

                #endregion

                #region → Change User             .

                if (OfferAppConfigurations.CurrentLoginUser != null && OfferAppConfigurations.CurrentLoginUser.UserID != OfferAppConfigurations.MainPlatformInfo.UserInfo.UserID)
                {
                    if (this.ViewModelRepository != null)
                    {
                        this.ViewModelRepository.Cleanup();

                        this.ViewModelRepository = null;
                    }
                }

                OfferAppConfigurations.CurrentLoginUser = OfferAppConfigurations.MainPlatformInfo.UserInfo;

                #endregion

                #region → View Model Repository   .

                if (ViewModelRepository != null)
                {
                    ViewModelRepository.GenerateOfferViewModel.ApplyChanges();
                }
                else
                {
                    ViewModelRepository = new ViewModelRepository();
                }

                this.DataContext = ViewModelRepository.GenerateOfferViewModel;

                #endregion

                #region → Adjust Widht and Heihgt .

                this.uxMainContent.Width = OfferAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Width;
                this.uxMainContent.MinWidth = this.uxMainContent.Width;
                this.uxMainContent.MaxWidth = this.uxMainContent.Width;

                this.uxMainContent.Height = OfferAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Height;
                this.uxMainContent.MinHeight = this.uxMainContent.Height;
                this.uxMainContent.MaxHeight = this.uxMainContent.Height;

                #endregion
            }
            else
            {
                this.uxMainContent.Content = new RadBusyIndicator() { IsBusy = true };
            }
        }
        #endregion

        #endregion
    }
}
