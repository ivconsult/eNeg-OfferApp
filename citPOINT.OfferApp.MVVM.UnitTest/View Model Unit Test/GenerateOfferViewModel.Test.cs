
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using citPOINT.OfferApp.Data.Web;
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
    /// Generate Offer View Model Test class
    /// </summary>
    [TestClass]
    public class GenerateOfferViewModel_Test
    {
        #region → Fields         .
        private GenerateOfferViewModel GenerateOffervm;
        private string ErrorMessage;
        string currentScreen = null;
        #endregion

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        /// <value>The VM.</value>
        public GenerateOfferViewModel TheVM
        {
            get { return GenerateOffervm; }
            set { GenerateOffervm = value; }
        }
        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateOfferViewModel_Test"/> class.
        /// </summary>
        [TestInitialize]
        public void BuildUp()
        {
            OfferAppConfigurations.NegotiationIDParameter = Guid.NewGuid();
            OfferAppConfigurations.ConversationIDParameter = Guid.NewGuid();

            TheVM = new GenerateOfferViewModel(new MockGenerateOfferModel());

            #region → Registeration for needed messages in eNegMessenger
            // register for RaiseErrorMessage
            OfferAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            OfferAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreenMessage);

            #endregion
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message   .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;
                }
                else
                    ErrorMessage = ex.Message;
            }
        }

        #endregion

        #region → Change Screen Message .

        /// <summary>
        /// Called when [change screen message].
        /// </summary>
        /// <param name="screenName">Name of the screen.</param>
        private void OnChangeScreenMessage(string screenName)
        {
            this.currentScreen = screenName;
        }

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Tests the basics.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(TheVM, "Failed to retrieve the View Model");
        }

        /// <summary>
        /// Gets the Prefernce Set without condition return collection.
        /// </summary>
        [TestMethod]
        public void GetPrefernceSet_WithoutCondition_ReturnCollection()
        {
            #region → Arrange .

            #endregion

            #region → Act     .

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.OfferSource.Count() > 0, "No Pereference Set Found");

            #endregion
        }

        /// <summary>
        /// Gets the Negotiation Settings without condition return collection.
        /// </summary>
        [TestMethod]
        public void GetNegotiationSettings_WithoutCondition_ReturnCollection()
        {
            #region → Arrange .

            #endregion

            #region → Act     .

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.CurrentNegotiationOfferSetting != null, "No Negotiation settings Found");

            #endregion
        }

        /// <summary>
        /// Gets the Conversation Settings without condition return collection.
        /// </summary>
        [TestMethod]
        public void GetConversationSettings_WithoutCondition_ReturnCollection()
        {
            #region → Arrange .

            #endregion

            #region → Act     .

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.CurrentConversationOfferSetting != null, "No Conversation settings Found");

            #endregion
        }

        /// <summary>
        /// Gets the Max Percentage without condition return collection.
        /// </summary>
        [TestMethod]
        public void GetMaxPercentage_WithoutCondition_ReturnCollection()
        {
            #region → Arrange .

            #endregion

            #region → Act     .

            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.PreferenceSetMaxPercentage == 97, "No Max Percentage Found");

            #endregion
        }
        
        /// <summary>
        /// Test Exit Generate Offer Command.
        /// </summary>
        [TestMethod]
        public void Text_ExitGenerateOfferCommand_Success()
        {
            #region → Arrange .
            this.currentScreen = string.Empty;
            #endregion

            #region → Act     .
            TheVM.ExitGenerateOfferCommand.Execute(null);
            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(this.currentScreen == OfferAppViewTypes.ClosePopupView, "Exit Generate Offer Failed");

            #endregion
        }

        #endregion

        #endregion
    }
}
