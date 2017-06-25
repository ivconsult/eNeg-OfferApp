#region → Usings   .
using System;
using System.ServiceModel.DomainServices.Client;
using citPOINT.OfferApp.Data.Web;
using GalaSoft.MvvmLight.Messaging;
using System.IO;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 16.08.11     Yousra Reda       Creation
 * 16.08.11     Yousra Reda       Create classes for needed messages
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.OfferApp.Common
{
    /// <summary>
    /// Strategy App Messanger
    /// </summary>
    public class OfferAppMessanger
    {
        #region → Enums                    .

        /// <summary>
        /// Enumerator represent the set of messages that can be sent
        /// </summary>
        enum MessageTypes
        {
            RaiseError,
            SubmitChanges,
            ChangeScreen
        }
        #endregion

        #region → Raise Error Message      .
        /// <summary>
        /// Class to handle any raised exception
        /// </summary>
        public static class RaiseErrorMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(Exception ex)
            {
                Messenger.Default.Send<Exception>(ex, MessageTypes.RaiseError);
            }

            /// <summary>
            /// Register to receive that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be handle the excption send and appear friendly message</param>
            public static void Register(object recipient, Action<Exception> action)
            {
                Messenger.Default.Register<Exception>(recipient, MessageTypes.RaiseError, action);
            }
        }

        #endregion

        #region → Submit Changes Message   .

        /// <summary>
        /// Class to submit any pending changes
        /// </summary>
        public static class SubmitChangesMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send()
            {
                Messenger.Default.Send<Boolean>(true, MessageTypes.SubmitChanges);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<Boolean> action)
            {
                Messenger.Default.Register<Boolean>(recipient, MessageTypes.SubmitChanges, action);
            }
        }
        #endregion

        #region → Change Screen Message    .

        /// <summary>
        /// Class to changes the current screen loaded
        /// </summary>
        public static class ChangeScreenMessage
        {
            /// <summary>
            /// Send this type of message to any recipient who want to register that type of messages
            /// </summary>
            public static void Send(string screenName)
            {
                Messenger.Default.Send<string>(screenName, MessageTypes.ChangeScreen);
            }

            /// <summary>
            /// Register to recieve that type of message
            /// </summary>
            /// <param name="recipient">The recipient that has register for this type of message</param>
            /// <param name="action">Method that will be called when message is recieved</param>
            public static void Register(object recipient, Action<string> action)
            {
                Messenger.Default.Register<string>(recipient, MessageTypes.ChangeScreen, action);
            }
        }
        #endregion
    }
}
