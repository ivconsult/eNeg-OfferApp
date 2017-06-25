
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using citPOINT.OfferApp.Data.Web.PrefAppService;
using citPOINT.OfferApp.Data.Web.StrategyAppService;
using System.ServiceModel.DomainServices.Server;
#endregion

#region → History  .

/* Date         User          Change
 * 
 * 28.02.12     Yousra Reda       • Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.OfferApp.Data.Web
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OfferAppService
    {
        #region → Fields         .

        PrefAppServiceSoapClient mLoaderPrefApp;
        StrategyAppServiceSoapClient mLoaderStrategyApp;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the loader of the pref app.
        /// </summary>
        /// <value>The loader.</value>
        public PrefAppServiceSoapClient LoaderPrefApp
        {
            get
            {
                if (mLoaderPrefApp == null)
                {
                    mLoaderPrefApp = new PrefAppServiceSoapClient();
                    InjectCredentialsForPrefApp();
                }
                return mLoaderPrefApp;
            }
        }

        /// <summary>
        /// Gets the loader of the strategy app.
        /// </summary>
        /// <value>The loader.</value>
        public StrategyAppServiceSoapClient LoaderStrategyApp
        {
            get
            {
                if (mLoaderStrategyApp == null)
                {
                    mLoaderStrategyApp = new StrategyAppServiceSoapClient();
                    InjectCredentialsForStrategyApp();
                }
                return mLoaderStrategyApp;
            }
        }

        /// <summary>
        /// Gets or sets the included results.
        /// </summary>
        /// <value>The included results.</value>
        private List<object> IncludedResults { get; set; }

        /// <summary>
        /// Gets or sets the complete preference set.
        /// </summary>
        /// <value>The complete preference set.</value>
        private CompletePreferenceSet CompletePreferenceSet { get; set; }

        /// <summary>
        /// Gets the max resolution.
        /// for Offer Agnet .
        /// </summary>
        /// <value>The max resolution.</value>
        private Int64 MaxResolution
        {
            get
            {
                try
                {
                    Int64 outValue = 0;

                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["maximumresolution"]) &&
                        Int64.TryParse(ConfigurationManager.AppSettings["maximumresolution"], out outValue) &&
                        outValue > 0)
                    {
                        return outValue;

                    }
                }
                catch (Exception)
                { }

                return 100000;
            }
        }

        #endregion

        #region → Methods        .

        #region → Public         .

        #region → Special for Offer App .

        /// <summary>
        /// Gets the preference set.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <returns></returns>
        public List<OfferDetails> GetPreferenceSet(Guid negotiationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                var result = LoaderPrefApp.GetCompletePreferenceSetForNegotiation(negotiationID);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    List<OfferDetails> offerList = new List<OfferDetails>();


                    if (result.RootResults == null || result.RootResults.FirstOrDefault() == null)
                    {
                        return new List<OfferDetails>();
                    }

                    this.CompletePreferenceSet = result.RootResults.FirstOrDefault();

                    if (result.IncludedResults == null || result.IncludedResults.Count() <= 0)
                    {
                        return new List<OfferDetails>();
                    }

                    this.IncludedResults = result.IncludedResults.ToList();


                    foreach (var issue in this.CompletePreferenceSet.GetIssues(IncludedResults).OrderBy(s => s.Rank))
                    {

                        offerList.Add(new OfferDetails()
                        {
                            Rank = issue.Rank,
                            IssueName = issue.Name,
                            MaxPercentage = CompletePreferenceSet.MaxPercentage
                        });

                        //completeNumeric = issue.GetNumeric(IncludedResults);
                        //if (completeNumeric != null)
                        //{ }

                        //options = issue.GetOptions(IncludedResults);
                        //if (options != null)
                        //{
                        //    foreach (var option in options)
                        //    { }
                        //}
                    }

                    return offerList;
                }
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
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
        /// <returns></returns>
        public IQueryable<ExpectedTarget> GetNextExpectedTargetForNegotiation(Guid negotiationID, OfferType offerType,
            decimal maxPercentage, DateTime currentDate, TargetTypeOptions targetType)
        {
            if (ServiceAuthentication.IsValid())
            {
                if (targetType == TargetTypeOptions.PrefApp)
                {
                    var result = LoaderPrefApp.GetNextExpectedTargetForNegotiation(negotiationID, offerType, maxPercentage).RootResults.FirstOrDefault();
                    if (result != null)
                    {
                        return CopyExpectedTarget(result);
                    }
                }
                else if (targetType == TargetTypeOptions.StrategyApp)
                {
                    var result = LoaderStrategyApp.GetNextExpectedTargetForNegotiation(negotiationID, maxPercentage, currentDate).RootResults.FirstOrDefault();
                    if (result != null)
                    {
                        return CopyExpectedTarget(result);
                    }
                }
                return null;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the next expected target for conversation.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <param name="maxPercentage">The max percentage.</param>
        /// <param name="currentDate">The current date.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        public IQueryable<ExpectedTarget> GetNextExpectedTargetForConversation(Guid negotiationID, Guid conversationID, OfferType offerType,
            decimal maxPercentage, DateTime currentDate, TargetTypeOptions targetType)
        {
            if (ServiceAuthentication.IsValid())
            {
                if (targetType == TargetTypeOptions.PrefApp)
                {
                    var result = LoaderPrefApp.GetNextExpectedTargetForConversation(conversationID, offerType, maxPercentage).RootResults.FirstOrDefault();
                    if (result != null)
                    {
                        return CopyExpectedTarget(result);
                    }
                }
                else if (targetType == TargetTypeOptions.StrategyApp)
                {
                    var result = LoaderStrategyApp.GetNextExpectedTargetForConversation(negotiationID, conversationID, maxPercentage, currentDate).RootResults.FirstOrDefault();
                    if (result != null)
                    {
                        return CopyExpectedTarget(result);
                    }
                }
                return null;
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the generated offer for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns></returns>
        public List<OfferDetails> GetGeneratedOfferForNegotiation(Guid negotiationID, decimal target, OfferType offerType)
        {
            if (ServiceAuthentication.IsValid())
            {
                return GetGeneratedOffer(negotiationID, null, target, offerType);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Gets the generated offer for conversation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns></returns>
        public List<OfferDetails> GetGeneratedOfferForConversation(Guid negotiationID, Guid conversationID, decimal target, OfferType offerType)
        {
            if (ServiceAuthentication.IsValid())
            {
                return GetGeneratedOffer(negotiationID, conversationID, target, offerType);
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                       
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        }

        /// <summary>
        /// Updates the offer details.
        /// Solver problem of Editiong Offer Details object in Grid
        /// </summary>
        /// <seealso cref="http://stackoverflow.com/questions/2445634/ria-services-entityset-does-not-support-edit-operation"/>
        /// <param name="newOfferDetails">The new offer details.</param>
        [Update]
        public void UpdateOfferDetails(OfferDetails newOfferDetails)
        { }

        #endregion

        #endregion

        #region → Private        .

        /// <summary>
        /// Copies the expected target.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private IQueryable<ExpectedTarget> CopyExpectedTarget(PrefAppService.ExpectedTarget result)
        {
            ExpectedTarget expectedTarget = new ExpectedTarget();
            expectedTarget.ID = result.ID;
            switch (result.Status)
            {
                case PrefAppService.Status.Success:
                    expectedTarget.Status = Status.Success;
                    break;
                case PrefAppService.Status.Failed:
                    expectedTarget.Status = Status.Failed;
                    break;
                case PrefAppService.Status.DateOutOfPeriod:
                    expectedTarget.Status = Status.DateOutOfPeriod;
                    break;
                case PrefAppService.Status.NoSettings:
                    expectedTarget.Status = Status.NoSettings;
                    break;
            }
            expectedTarget.Target = result.Target;

            return new List<ExpectedTarget>() { expectedTarget }.AsQueryable();
        }

        /// <summary>
        /// Copies the expected target.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private IQueryable<ExpectedTarget> CopyExpectedTarget(StrategyAppService.ExpectedTarget result)
        {
            ExpectedTarget expectedTarget = new ExpectedTarget();
            expectedTarget.ID = result.ID;
            switch (result.Status)
            {
                case StrategyAppService.Status.Success:
                    expectedTarget.Status = Status.Success;
                    break;
                case StrategyAppService.Status.Failed:
                    expectedTarget.Status = Status.Failed;
                    break;
                case StrategyAppService.Status.DateOutOfPeriod:
                    expectedTarget.Status = Status.DateOutOfPeriod;
                    break;
                case StrategyAppService.Status.NoSettings:
                    expectedTarget.Status = Status.NoSettings;
                    break;
                default:
                    break;
            }
            expectedTarget.Target = result.Target;
            return new List<ExpectedTarget>() { expectedTarget }.AsQueryable();
        }

        /// <summary>
        /// Gets the base offer for negotiation.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns></returns>
        private List<OfferItem> GetBaseOfferForNegotiation(Guid negotiationID, OfferType offerType)
        {
            return LoaderPrefApp.GetBaseOfferForNegotiation(negotiationID, offerType).RootResults.ToList();
        }

        /// <summary>
        /// Gets the base offer for conversation.
        /// </summary>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns></returns>
        private List<OfferItem> GetBaseOfferForConversation(Guid conversationID, OfferType offerType)
        {
            return LoaderPrefApp.GetBaseOfferForConversation(conversationID, offerType).RootResults.ToList();
        }

        /// <summary>
        /// Gets the get generated offer.
        /// </summary>
        /// <param name="negotiationID">The negotiation ID.</param>
        /// <param name="conversationID">The conversation ID.</param>
        /// <param name="target">The target.</param>
        /// <param name="offerType">Type of the offer.</param>
        /// <returns></returns>
        private List<OfferDetails> GetGeneratedOffer(Guid negotiationID, Guid? conversationID, decimal target, OfferType offerType)
        {

            #region → Get Preference Set and Issues Details .

            this.CompletePreferenceSet = null;
            this.IncludedResults = null;

            this.GetPreferenceSet(negotiationID);

            if (CompletePreferenceSet == null ||
                this.IncludedResults == null ||
                this.IncludedResults.Count <= 0)
            {
                return new List<OfferDetails>();
            }

            #endregion

            #region → Get Base Offer                        .

            List<OfferItem> tmpBaseOffer = null;

            if (conversationID.HasValue)
            {
                tmpBaseOffer = this.GetBaseOfferForConversation(conversationID.Value, offerType).ToList();
            }
            else
            {
                tmpBaseOffer = this.GetBaseOfferForNegotiation(negotiationID, offerType).ToList();
            }

            #endregion

            #region → Run The Agent                         .

            OfferAgent.OfferAgent testAgnet
                = new OfferAgent.OfferAgent(
                                           this.MaxResolution,
                                            this.CompletePreferenceSet,
                                            this.IncludedResults,
                                            tmpBaseOffer);

            testAgnet.GenerateNextOffer((int)Math.Ceiling(target));

            Debug.Write(testAgnet.GetAgentDebugInfo());

            #endregion

            #region → Adapt the result ot the Offer details .

            List<OfferDetails> offerList = new List<OfferDetails>();

            foreach (var issue in testAgnet.NextOffer.Issues)
            {
                offerList.Add(
                    new OfferDetails()
                    {
                        Rank = issue.Rank,
                        IssueName = issue.Name,
                        Value = issue.CurrentPrintValue,
                        MaxPercentage = CompletePreferenceSet.MaxPercentage
                    });

            }
            #endregion

            return offerList;
        }

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentialsForPrefApp()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)LoaderPrefApp.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentialsForStrategyApp()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)LoaderStrategyApp.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }

        #endregion

        #endregion
    }
}
