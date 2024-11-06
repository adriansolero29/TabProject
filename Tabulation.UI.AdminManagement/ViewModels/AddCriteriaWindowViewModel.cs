using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabulation.UI.AdminManagement.BaseViewModels;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class AddCriteriaWindowViewModel : DialogsBaseViewModel
    {
        private readonly IContainerProvider container;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IDialogService dialogService;

        public AddCriteriaWindowViewModel(IContainerProvider container, IRegionManager regionManager, IEventAggregator eventAggregator, IDialogService dialogService)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.dialogService = dialogService;

        }

        #region Commands

        private DelegateCommand? _addCriterion;
        public DelegateCommand? AddCriterion =>
            _addCriterion ?? (_addCriterion = new DelegateCommand(addCriterion));

        #endregion

        #region Methods

        void addCriterion()
        {
            CriterionListInfo?.Add(new Criterion { });
        }

        #endregion

        #region Collections

        private ObservableCollection<Criterion>? _criterionListInfo;
        public ObservableCollection<Criterion>? CriterionListInfo
        {
            get
            {
                if (_criterionListInfo == null)
                    _criterionListInfo = new ObservableCollection<Criterion>();
                return _criterionListInfo;
            }
            set { SetProperty(ref _criterionListInfo, value); }
        }

        #endregion

        #region DialogAware

        public override bool CanCloseDialog() => true;

        public override void OnDialogClosed()
        {
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #endregion
    }
}
