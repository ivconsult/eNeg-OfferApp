
#region → Usings   .
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using citPOINT.eNeg.Common;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 05.03.12   Yousra Reda       Creation
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
    /// This view will be a content to a pop uo window to enable 
    /// user to define his settings and genrate next expected offer
    /// </summary>
    public partial class GenerateOfferView : UserControl, ICleanup
    {
        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateOfferView"/> class.
        /// </summary>
        public GenerateOfferView()
        {
            InitializeComponent();

            eNegMessanger.SendCustomMessage.Register(this, OnSendCustomMessage);
        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Called when [send custom message].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnSendCustomMessage(eNegMessage message)
        {
            Dispatcher.BeginInvoke(() =>
            {
                uxMsgNextTargetResult.MessageText = message.Message;
                uxMsgNextTargetResult.Completed = message.ShowMessageCompleted;
                uxMsgNextTargetResult.Show();
            });
        }
        #endregion

        #endregion

        #region → Public         .
        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }
        #endregion
    }
}
