
namespace citPOINT.OfferApp.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // Implements application logic using the Entities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class OfferAppService : LinqToEntitiesDomainService<OfferAppEntities>
    {
        
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ConversationOfferSettings' query.
        public IQueryable<ConversationOfferSetting> GetConversationOfferSettings()
        {
            return this.ObjectContext.ConversationOfferSettings;
        }

        public void InsertConversationOfferSetting(ConversationOfferSetting conversationOfferSetting)
        {
            if ((conversationOfferSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(conversationOfferSetting, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ConversationOfferSettings.AddObject(conversationOfferSetting);
            }
        }

        public void UpdateConversationOfferSetting(ConversationOfferSetting currentConversationOfferSetting)
        {
            this.ObjectContext.ConversationOfferSettings.AttachAsModified(currentConversationOfferSetting, this.ChangeSet.GetOriginal(currentConversationOfferSetting));
        }

        public void DeleteConversationOfferSetting(ConversationOfferSetting conversationOfferSetting)
        {
            if ((conversationOfferSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(conversationOfferSetting, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ConversationOfferSettings.Attach(conversationOfferSetting);
                this.ObjectContext.ConversationOfferSettings.DeleteObject(conversationOfferSetting);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'NegotiationOfferSettings' query.
        public IQueryable<NegotiationOfferSetting> GetNegotiationOfferSettings()
        {
            return this.ObjectContext.NegotiationOfferSettings;
        }

        public void InsertNegotiationOfferSetting(NegotiationOfferSetting negotiationOfferSetting)
        {
            if ((negotiationOfferSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationOfferSetting, EntityState.Added);
            }
            else
            {
                this.ObjectContext.NegotiationOfferSettings.AddObject(negotiationOfferSetting);
            }
        }

        public void UpdateNegotiationOfferSetting(NegotiationOfferSetting currentNegotiationOfferSetting)
        {
            this.ObjectContext.NegotiationOfferSettings.AttachAsModified(currentNegotiationOfferSetting, this.ChangeSet.GetOriginal(currentNegotiationOfferSetting));
        }

        public void DeleteNegotiationOfferSetting(NegotiationOfferSetting negotiationOfferSetting)
        {
            if ((negotiationOfferSetting.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(negotiationOfferSetting, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.NegotiationOfferSettings.Attach(negotiationOfferSetting);
                this.ObjectContext.NegotiationOfferSettings.DeleteObject(negotiationOfferSetting);
            }
        }

    }
}


