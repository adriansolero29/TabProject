using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabulation.UI.AdminManagement.BaseViewModels
{
    public class MainProviderComposition
    {
        public MainProviderComposition(IRegionManager regionManager, IDialogService dialogService, IContainerProvider containerProvider, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            DialogService = dialogService;
            ContainerProvider = containerProvider;
            EventAggregator = eventAggregator;
        }

        public IRegionManager RegionManager { get; }
        public IDialogService DialogService { get; }
        public IContainerProvider ContainerProvider { get; }
        public IEventAggregator EventAggregator { get; }
    }
}
