
#region → Usings   .
using System;
using System.Collections.Generic;
using System.ServiceModel.DomainServices.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.Data.Web;

#endregion

#region → History  .

/* Date         User            Change
 * 
 * 29.02.12     Yousra Reda     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
*/

# endregion

namespace citPOINT.OfferApp.Data.Web.Test
{
    /// <summary>
    /// Class for testing [Insert - Update - Delete] 
    /// operations for OfferApp Database
    /// </summary>
    [TestClass]
    public class OfferAppServiceTest
    {
        #region → Fields         .
        OfferAppContext mContext;

        private List<NegotiationOfferSetting> mNegotiationOfferSettingSource;
        private List<ConversationOfferSetting> mConversationOfferSettingSource;
       
        int CountOfAllEntries = 0;
        private TestContext testContextInstance;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the car negotiation.
        /// </summary>
        /// <value>The car negotiation.</value>
        public Guid CarNegotiation
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        /// <summary>
        /// Gets the laptop negotiation.
        /// </summary>
        /// <value>The laptop negotiation.</value>
        public Guid LaptopNegotiation
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        /// <summary>
        /// Gets the car conversation1.
        /// </summary>
        /// <value>The car conversation1.</value>
        public Guid CarConversation1
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        /// <summary>
        /// Gets the car conversation2.
        /// </summary>
        /// <value>The car conversation2.</value>
        public Guid CarConversation2
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        #region Mock Objects

        #region → Negotiation Offer Setting  .

        /// <summary>
        /// Gets the phase sources.
        /// </summary>
        /// <value>The phase sources.</value>
        public List<NegotiationOfferSetting> NegotiationOfferSettingSource
        {
            get
            {
                if (mNegotiationOfferSettingSource == null)
                {
                    mNegotiationOfferSettingSource = new List<NegotiationOfferSetting>()
                    {
                        new  NegotiationOfferSetting
                        {
                            NegotiationID=this.CarNegotiation,
                            BaseOfferTypeID = 2,
                            TargetTypeID = 2,
                            TargetValue = 65,
                            Deleted=false,
                            DeletedBy=OfferAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        },

                        new  NegotiationOfferSetting
                        {
                            NegotiationID=this.LaptopNegotiation,
                            BaseOfferTypeID = 1,
                            TargetTypeID = 1,
                            TargetValue = 50,
                            Deleted=false,
                            DeletedBy=OfferAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        },

                    };
                }
                return mNegotiationOfferSettingSource;
            }
        }

        #endregion

        #region → Conversation Offer Setting .

        /// <summary>
        /// Gets the conversation Offer setting source.
        /// </summary>
        /// <value>The conversation Offer setting source.</value>
        public List<ConversationOfferSetting> ConversationOfferSettingSource
        {
            get
            {
                if (mConversationOfferSettingSource == null)
                {
                    mConversationOfferSettingSource = new List<ConversationOfferSetting>()
                    {
                        new  ConversationOfferSetting
                        {
                            ConversationOfferSettingID = Guid.NewGuid(),
                            BaseOfferTypeID = 2,
                            TargetTypeID = 2,
                            TargetValue = 80,
                            ConversationID = this.CarConversation1,                           
                            NegotiationOfferSetting=this.NegotiationOfferSettingSource[0],
                            Deleted=false,
                            DeletedBy=OfferAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        },
                        new  ConversationOfferSetting
                        {
                            ConversationOfferSettingID = Guid.NewGuid(),
                            BaseOfferTypeID = 1,
                            TargetTypeID = 1,
                            TargetValue = 70,
                            ConversationID = this.CarConversation2,                           
                            NegotiationOfferSetting=this.NegotiationOfferSettingSource[0],
                            Deleted=false,
                            DeletedBy=OfferAppConfigurations.CurrentLoginUser.UserID,
                            DeletedOn=DateTime.Now
                        } 

                    };
                }
                return mConversationOfferSettingSource;
            }
        }

        #endregion

        #endregion

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
                    mContext = new OfferAppContext(new Uri("http://localhost:9008/citPOINT-OfferApp-Data-Web-OfferAppService.svc", UriKind.Absolute));
                }
                return mContext;
            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion Properties
    
        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferAppServiceTest"/> class.
        /// </summary>
        public OfferAppServiceTest()
        {
            OfferAppConfigurations.CurrentLoginUser = new LoginUser();
            OfferAppConfigurations.CurrentLoginUser.UserID = new Guid("C7CAD9E5-FA25-4EB9-82E6-E4D66D2D03BB");
            OfferAppConfigurations.NegotiationIDParameter = this.CarNegotiation;
            OfferAppConfigurations.ConversationIDParameter = this.CarConversation1;

            CountOfAllEntries = this.NegotiationOfferSettingSource.Count +
                                this.ConversationOfferSettingSource.Count;
        }

        #endregion

        #region → Methods        .

        #region Test Insert All Entries
        /// <summary>
        ///A test for Insert All Entries
        ///</summary>
        [TestMethod]
        [Description(@"Test Insert Operations for all entries")]
        public void InsertAllEntries()
        {
            try
            {
                foreach (var item in this.NegotiationOfferSettingSource)
                {
                    this.Context.NegotiationOfferSettings.Add(item);
                }

                foreach (var item in this.ConversationOfferSettingSource)
                {
                    this.Context.ConversationOfferSettings.Add(item);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(InsertAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntries", ex);
            }
        }

        /// <summary>
        /// Inserts all entries complete.
        /// </summary>
        /// <param name="subOp">The sub op.</param>
        private void InsertAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    UpdateAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "InsertAllEntriesComplete", subOp.Error);
            }
        }

        #endregion

        #region Test Update All Entries

        /// <summary>
        ///A test for Update All Entries
        ///</summary>
        public void UpdateAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                foreach (var item in this.NegotiationOfferSettingSource)
                {
                    item.TargetValue -= 10;
                }

                foreach (var item in this.ConversationOfferSettingSource)
                {
                    item.TargetValue -= 10;
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(UpdateAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntries", ex);
            }
        }

        /// <summary>
        /// Event Complete of  Update All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        private void UpdateAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {
                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", "Number of Records updated is not right.");
                }
                else
                {
                    DeleteAllEntries();
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "UpdateAllEntriesComplete", subOp.Error);
            }
        }

        #endregion

        #region Test Delete All Entries

        /// <summary>
        ///A test for Delete All Entries
        ///only for Delete Flag
        ///</summary>
        public void DeleteAllEntries()
        {
            try
            {
                this.Context.RejectChanges();

                while (this.ConversationOfferSettingSource.Count > 0)
                {
                    this.Context.ConversationOfferSettings.Remove(this.ConversationOfferSettingSource[0]);
                    this.ConversationOfferSettingSource.RemoveAt(0);
                }


                while (this.NegotiationOfferSettingSource.Count > 0)
                {
                    this.Context.NegotiationOfferSettings.Remove(this.NegotiationOfferSettingSource[0]);
                    this.NegotiationOfferSettingSource.RemoveAt(0);
                }

                this.Context.SubmitChanges(new Action<SubmitOperation>(DeleteAllEntriesComplete), null);
            }
            catch (Exception ex)
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntries", ex);
            }
        }

        /// <summary>
        /// Event Complete of  Delete All Entries
        /// </summary>
        /// <param name="subOp">Value of subOp</param>
        private void DeleteAllEntriesComplete(SubmitOperation subOp)
        {
            if (!subOp.HasError)
            {

                if (subOp.ChangeSet.AddedEntities.Count == 0 &&
                    subOp.ChangeSet.ModifiedEntities.Count == 0 &&
                    subOp.ChangeSet.RemovedEntities.Count != this.CountOfAllEntries)
                {
                    eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", "Number of Records Inserted is not right.");
                }
                else
                {
                    eNegMessageBox.ShowMessageBox(true, "Inset - Update - Delete All Entries ", DeleteString);
                }
            }
            else
            {
                eNegMessageBox.ShowMessageBox(false, "DeleteAllEntriesComplete", subOp.Error);
            }
        }

        #endregion

        /// <summary>
        /// get SQL Statement to Clear Database
        /// </summary>
        private string DeleteString
        {
            get
            {
                return @"
---------------------------------------------------
You must run these SQL commands Before retest again
---------------------------------------------------

DELETE [ConversationOfferSetting];
DELETE [NegotiationOfferSetting];
";
            }
        }

        #endregion Methods
    }
}
