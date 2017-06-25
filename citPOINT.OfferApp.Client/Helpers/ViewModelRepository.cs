
#region → Usings   .
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight;
using citPOINT.OfferApp.ViewModel;
#endregion

#region → History  .

/* Date         User          Change
 * 
 * 23.04.12     Yousra Reda         Creation
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
    /// View Model Repository.
    /// Shared View Models forcing that all view models are intialized.
    /// </summary>
    public class ViewModelRepository : ICleanup
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the generate offer view model.
        /// </summary>
        /// <value>The generate offer view model.</value>
        [Import(OfferApp.Common.OfferAppViewModelTypes.GenerateOfferViewModel)]
        public GenerateOfferViewModel GenerateOfferViewModel { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRepository"/> class.
        /// </summary>
        [ImportingConstructor]
        public ViewModelRepository()
        {
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                OfferAppModule.Container.SatisfyImportsOnce(this);
            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.GenerateOfferViewModel.Cleanup();
        }

        #endregion
    }
}
