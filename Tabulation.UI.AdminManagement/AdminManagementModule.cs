using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabulation.UI.AdminManagement.ViewModels;
using Tabulation.UI.AdminManagement.Views;

namespace Tabulation.UI.AdminManagement
{
    public class AdminManagementModule : IModule
    {
        private readonly IRegionManager regionManager;

        public AdminManagementModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UIContainer>(Helpers.ViewRegionNames.UIContainer);
            containerRegistry.RegisterForNavigation<MainViewer>(Helpers.ViewRegionNames.MainViewerForm);
            regionManager.RegisterViewWithRegion(Helpers.ViewRegionNames.ShellWindowRegion, Helpers.ViewRegionNames.UIContainer);

            containerRegistry.RegisterForNavigation<AddContestWindow>(Helpers.ViewRegionNames.AddContestWindow);
            containerRegistry.RegisterForNavigation<Templates.CustomMessageBox.Views.Success>(Helpers.DialogNames.SuccessDialog);
            containerRegistry.RegisterForNavigation<Templates.CustomMessageBox.Views.YesNo>(Helpers.DialogNames.YesNoDialog);
            containerRegistry.RegisterForNavigation<Templates.CustomMessageBox.Views.Warning>(Helpers.DialogNames.WarningDialog);
        }
    }
}
