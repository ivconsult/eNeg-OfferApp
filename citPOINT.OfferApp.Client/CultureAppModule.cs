using System.ComponentModel.Composition;
using System.Windows;
using citPOINT.OfferApp.Client;
using citPOINT.OfferApp.Common;
using MainPlatform.Common.Interfaces;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================

namespace citPOINT.OfferApp.Client
{
    [ModuleExport(typeof(OfferAppModule))]
    public class OfferAppModule : IModule
    {
        private readonly IRegionManager regionManager;

        [ImportingConstructor()]
        public OfferAppModule(IRegionManager regionManager, IMainPlatformInfo MainPlatformInfo)
        {
            this.regionManager = regionManager;

            OfferAppConfigurations.MainPlatformInfo = MainPlatformInfo;

            OfferAppConfigurations.MainPlatformInfo.DataHasChanged += new System.EventHandler<System.EventArgs>(MainPlatformInfo_DataHasChanged);

            AdaptSettings();

        }


        private void AdaptSettings()
        {
            OfferAppConfigurations.ConversationIDParameter = OfferAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;

            OfferAppConfigurations.NegotiationIDParameter = OfferAppConfigurations.MainPlatformInfo.CurrentNegotiation.NegotiationID;

            OfferAppConfigurations.CurrentLoginUser = new citPOINT.OfferApp.Data.Web.LoginUser()
            {
                FullName = OfferAppConfigurations.MainPlatformInfo.UserInfo.DisplayName,
                UserID = OfferAppConfigurations.MainPlatformInfo.UserInfo.UserID,
                EmailAddress = OfferAppConfigurations.MainPlatformInfo.UserInfo.EmailAddress
            };

        }

        void MainPlatformInfo_DataHasChanged(object sender, System.EventArgs e)
        {
            AdaptSettings();

            //DefaultView.LastInstance.ViewModel.GenerateOfferCommand;

        }

        public void Initialize()
        {
            try
            {
                regionManager.RegisterViewWithRegion("OfferAppRegion", typeof(DefaultView));
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

    }
}
