#region → Usings   .

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using citPOINT.eNeg.Apps.Common.Interfaces;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using citPOINT.OfferApp.Common;
using citPOINT.OfferApp.Model;
using citPOINT.OfferApp.ViewModel;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 23.04.12     Yousra Reda       Creation
 */

# endregion History

#region → ToDos    .
/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion ToDos

namespace citPOINT.OfferApp.Client
{
    /// <summary>
    /// Offer App Module.
    /// </summary>
    [ModuleExport(typeof(OfferAppModule))]
    public class OfferAppModule : IModule
    {
        #region → Fields         .

        private readonly IRegionManager regionManager;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public static CompositionContainer Container { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferAppModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="MainPlatformInfo">The main platform info.</param>
        [ImportingConstructor()]
        public OfferAppModule(IRegionManager regionManager, IMainPlatformInfo MainPlatformInfo)
        {
            this.regionManager = regionManager;

            OfferAppConfigurations.MainPlatformInfo = MainPlatformInfo;

            this.IntializeContainer();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Intializes the container.
        /// </summary>
        private void IntializeContainer()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(OfferAppConfigurations).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(GenerateOfferViewModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(GenerateOfferModel).Assembly));

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(CultureFiveDimension).Assembly));

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(PreferenceSetNeg).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            try
            {
                regionManager.RegisterViewWithRegion
                    (OfferAppConfigurations.AppName.Replace(" ", "") + "Region",
                     typeof(MainPageView));
            }
            catch (System.Exception ex)
            {
                OfferAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, OfferAppConfigurations.AppName);
            }
        }

        #endregion

        #endregion

    }
}