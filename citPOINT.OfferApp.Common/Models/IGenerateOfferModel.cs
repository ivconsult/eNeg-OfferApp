
#region → Usings   .
using System;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using citPOINT.OfferApp.Data.Web;
using citPOINT.eNeg.Common;
using citPOINT.OfferApp.Data.Web.PrefAppService;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 01.03.12     Yousra Reda       Creation
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
    /// <summary>
    /// interface for the Generate Offer Model
    /// </summary>
    public interface IGenerateOfferModel
    {

        #region → Properties     .

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has changes; otherwise, <c>false</c>.
        /// </value>
        bool HasChanges { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        bool IsBusy { get; }
        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [generate offer for negotiation complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<OfferDetails>> GenerateOfferForNegotiationComplete;

        /// <summary>
        /// Occurs when [generate offer for conversation complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<OfferDetails>> GenerateOfferForConversationComplete;

        /// <summary>
        /// Occurs when [get conversation offer setting complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ConversationOfferSetting>> GetConversationOfferSettingComplete;

        /// <summary>
        /// Occurs when [get negotiation offer setting complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<NegotiationOfferSetting>> GetNegotiationOfferSettingComplete;

        /// <summary>
        /// Occurs when [get preference set complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<OfferDetails>> GetPreferenceSetComplete;
                
        /// <summary>
        /// Occurs when [get next expected target for negotiation complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ExpectedTarget>> GetNextExpectedTargetForNegotiationComplete;

        /// <summary>
        /// Occurs when [get next expected target for conversation complete].
        /// </summary>
        event EventHandler<eNegEntityResultArgs<ExpectedTarget>> GetNextExpectedTargetForConversationComplete;

        /// <summary>
        /// Occurs when [save changes complete].
        /// </summary>
        event EventHandler<SubmitOperationEventArgs> SaveChangesComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        /// <summary>
        /// Adds the conversation offer settings.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <param name="negOfferSettings">The neg offer settings.</param>
        /// <returns></returns>
        ConversationOfferSetting AddConversationOfferSettings(bool setInContext, NegotiationOfferSetting negOfferSettings);

        /// <summary>
        /// Adds the negotiation offer settings.
        /// </summary>
        /// <param name="setInContext">if set to <c>true</c> [set in context].</param>
        /// <returns></returns>
        NegotiationOfferSetting AddNegotiationOfferSettings(bool setInContext);

        /// <summary>
        /// Gets the conversation offer setting async.
        /// </summary>
        void GetConversationOfferSettingAsync();

        /// <summary>
        /// Gets the negotiation offer setting async.
        /// </summary>
        void GetNegotiationOfferSettingAsync();

        /// <summary>
        /// Gets the preference set async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        void GetPreferenceSetAsync(Guid negotiationID);

        /// <summary>
        /// Gets the next expected target for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <param name="maxPercentage">The max percentage.</param>
        /// <param name="currentDate">The current date.</param>
        /// <param name="targetType">Type of the target.</param>
        void GetNextExpectedTargetForNegotiationAsync(Guid negotiationID,
                                                 OfferType offerType,
                                                 decimal maxPercentage,
                                                 DateTime currentDate,
                                                 TargetTypeOptions targetType);

        /// <summary>
        /// Gets the next expected target for conversation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <param name="maxPercentage">The max percentage.</param>
        /// <param name="currentDate">The current date.</param>
        /// <param name="targetType">Type of the target.</param>
        void GetNextExpectedTargetForConversationAsync(Guid negotiationID,
                                                  Guid conversationID,
                                                  OfferType offerType,
                                                  decimal maxPercentage,
                                                  DateTime currentDate,
                                                  TargetTypeOptions targetType);

        /// <summary>
        /// Generates the offer for negotiation async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        void GenerateOfferForNegotiationAsync(Guid negotiationID, decimal target, OfferType offerType);

        /// <summary>
        /// Generates the offer for conversation async.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        void GenerateOfferForConversationAsync(Guid negotiationID, Guid conversationID, decimal target, OfferType offerType);

        /// <summary>
        /// Rejects the changes.
        /// </summary>
        void RejectChanges();

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        void SaveChangesAsync();
        #endregion


    }
}
