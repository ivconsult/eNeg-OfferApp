
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using citPOINT.eNeg.Common;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.Data.Web;
using citPOINT.OfferApp.Data.Web.PrefAppService;
using citPOINT.OfferApp.Model;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 06.03.12     M.Wahab         • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.OfferApp.MVVM.UnitTest
{
    /// <summary>
    /// Mock Message Template Model
    /// </summary>
    public class MockGenerateOfferModel : IGenerateOfferModel
    {
        #region → Fields         .

        private OfferAppContext mContext;

        private List<ConversationOfferSetting> mConversationOfferSettingSource;
        private List<NegotiationOfferSetting> mNegotiationOfferSettingSource;
        private List<OfferDetails> mOfferSource;
        private List<ExpectedTarget> mExpectedTargetSource;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanges
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// property with a getter only to can use eNegService
        /// </summary>
        public OfferAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                    mContext = new OfferAppContext(new Uri("http://localhost:9004/citPOINT-OfferApp-Data-Web-OfferAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }



        #region → <1> Negotiation Offer Setting  .


        /// <summary>
        /// Gets the negotiation offer setting source.
        /// </summary>
        /// <value>The negotiation offer setting source.</value>
        public List<NegotiationOfferSetting> NegotiationOfferSettingSource
        {
            get
            {

                if (mNegotiationOfferSettingSource == null)
                {
                    mNegotiationOfferSettingSource = new List<NegotiationOfferSetting>()
                    {
                            this.AddNegotiationOfferSettings(true)
                    };

                }
                return mNegotiationOfferSettingSource;
            }
        }

        #endregion

        #region → <2> Conversation Offer Setting   .

        /// <summary>
        /// Gets the phase sources.
        /// </summary>
        /// <value>The phase sources.</value>
        public List<ConversationOfferSetting> ConversationOfferSettingSource
        {
            get
            {
                if (mConversationOfferSettingSource == null)
                {
                    mConversationOfferSettingSource = new List<ConversationOfferSetting>()
                    {
                            this.AddConversationOfferSettings(true,NegotiationOfferSettingSource.FirstOrDefault())
                    };
                }
                return mConversationOfferSettingSource;
            }
        }

        #endregion



        #region → <3> Offer Details   .

        /// <summary>
        /// Gets the offer source.
        /// </summary>
        /// <value>The offer source.</value>
        public List<OfferDetails> OfferSource
        {
            get
            {
                if (mOfferSource == null)
                {
                    mOfferSource = new List<OfferDetails>()
                    {
                        new OfferDetails(){
                             Rank=1,
                             IssueName="Price",
                             Value=string.Empty,
                             MaxPercentage=97
                        },

                        new OfferDetails(){
                             Rank=2,
                             IssueName="Color",
                             Value=string.Empty,
                             MaxPercentage=97
                        },

                        new OfferDetails(){
                             Rank=3,
                             IssueName="Size",
                             Value=string.Empty,
                             MaxPercentage=97
                        },
                        
                    };
                }
                return mOfferSource;
            }
        }

        #endregion


        #region → <4> Offer Details   .

        /// <summary>
        /// Gets the offer source.
        /// </summary>
        /// <value>The offer source.</value>
        public List<ExpectedTarget> ExpectedTargetSource
        {
            get
            {
                if (mExpectedTargetSource == null)
                {
                    mExpectedTargetSource = new List<ExpectedTarget>()
                    {
                        new ExpectedTarget(){
                             ID=1,
                              Status= Status.Success,
                               Target=55
                        }
                    };
                }
                return mExpectedTargetSource;
            }
        }

        #endregion


        #endregion

        #region → Constructors   .

        /// <summary>
        /// Initializes a new instance of the <see cref="MockGenerateOfferModel"/> class.
        /// </summary>
        public MockGenerateOfferModel()
        {
            OfferAppConfigurations.CurrentLoginUser = new LoginUser();
            OfferAppConfigurations.CurrentLoginUser.UserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB");
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

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

            return NegOfferSettings;
        }

        /// <summary>
        /// Gets the conversation offer setting async.
        /// </summary>
        public void GetConversationOfferSettingAsync()
        {
            if (this.GetConversationOfferSettingComplete != null)
            {
                this.GetConversationOfferSettingComplete(this, new eNegEntityResultArgs<ConversationOfferSetting>(this.ConversationOfferSettingSource));
            }
        }

        /// <summary>
        /// Gets the negotiation offer setting async.
        /// </summary>
        public void GetNegotiationOfferSettingAsync()
        {
            if (this.GetNegotiationOfferSettingComplete != null)
            {
                this.GetNegotiationOfferSettingComplete(this, new eNegEntityResultArgs<NegotiationOfferSetting>(this.NegotiationOfferSettingSource));
            }
        }

        /// <summary>
        /// Gets the preference set async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        public void GetPreferenceSetAsync(Guid negotiationID)
        {
            if (this.GetPreferenceSetComplete != null)
            {
                this.GetPreferenceSetComplete(this, new eNegEntityResultArgs<OfferDetails>(this.OfferSource));
            }
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
            if (GetNextExpectedTargetForNegotiationComplete != null)
            {
                GetNextExpectedTargetForNegotiationComplete(this, new eNegEntityResultArgs<Data.Web.ExpectedTarget>(this.ExpectedTargetSource));
            }
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
            if (GetNextExpectedTargetForConversationComplete != null)
            {
                GetNextExpectedTargetForConversationComplete(this, new eNegEntityResultArgs<Data.Web.ExpectedTarget>(this.ExpectedTargetSource));
            }
        }

        /// <summary>
        /// Generates the offer for negotiation async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        public void GenerateOfferForNegotiationAsync(Guid negotiationID, decimal target, OfferType offerType)
        {
            if (this.GenerateOfferForNegotiationComplete != null)
            {
                this.GenerateOfferForNegotiationComplete(this, new eNegEntityResultArgs<OfferDetails>(this.OfferSource));
            }
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
            if (this.GenerateOfferForConversationComplete != null)
            {
                this.GenerateOfferForConversationComplete(this, new eNegEntityResultArgs<OfferDetails>(this.OfferSource));
            }
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public void SaveChangesAsync()
        {
            if (SaveChangesComplete != null)
            {
                SaveChangesComplete(this, new SubmitOperationEventArgs(null, null));
            }
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
