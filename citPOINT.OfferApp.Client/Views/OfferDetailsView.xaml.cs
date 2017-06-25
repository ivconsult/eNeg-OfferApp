
#region → Usings   .
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using System.ComponentModel.Composition;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.ViewModel;
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
    /// This view is the main view that will appear in add-on for this app 
    /// </summary>
    public partial class OfferDetailsView : UserControl
    {
        #region → Constructor    .
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferDetailsView"/> class.
        /// </summary>
        public OfferDetailsView()
        {
            InitializeComponent();
        }

        #endregion
    }
}
