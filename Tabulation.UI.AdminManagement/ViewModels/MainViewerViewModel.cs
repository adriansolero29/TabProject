using Base;
using ObjectLoader.Event;
using RepositoryServices.CustomModel;
using RepositoryServices.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class MainViewerViewModel : PrismBaseViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IDialogService dialogService;
        private readonly IRegionManager regionManager;
        private readonly IContainerProvider container;
        private readonly ICriteriaService criteriaService;
        private readonly ICriterionService criterionService;

        public MainViewerViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IRegionManager regionManager, IContainerProvider container, ICriteriaService criteriaService, ICriterionService criterionService)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            this.regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.criteriaService = criteriaService ?? throw new ArgumentNullException(nameof(criteriaService));
            this.criterionService = criterionService ?? throw new ArgumentNullException(nameof(criterionService));

            eventAggregator.GetEvent<PassData<Contest>>().Subscribe(SubscribeContestData);
        }

        #region Commands

        private DelegateCommand<object>? _editCriterion;
        public DelegateCommand<object>? EditCriterion =>
            _editCriterion ?? (_editCriterion = new DelegateCommand<object>(editCriterion));

        private DelegateCommand? _deleteCriterion;
        public DelegateCommand? DeleteCriterion =>
            _deleteCriterion ?? (_deleteCriterion = new DelegateCommand(deleteCriterion));

        private DelegateCommand? _addCriteria;
        public DelegateCommand? AddCriteria =>
            _addCriteria ?? (_addCriteria = new DelegateCommand(addCriteria));

        private DelegateCommand? _refreshCriteria;
        public DelegateCommand? RefreshCriteria =>
            _refreshCriteria ?? (_refreshCriteria = new DelegateCommand(refreshCriteria));

        #endregion

        #region Methods

        void refreshCriteria()
        {
        }

        private async void addCriteria()
        {
            await UINavigator.OpenDialog(container, dialogService, Helpers.ViewRegionNames.AddCriteriaWindow);
        }

        private void deleteCriterion()
        {

        }

        private void editCriterion(object o)
        {

        }

        private async void SubscribeContestData(Payload<Contest> payload)
        {
            try
            {
                ContestInfo = Helpers.ObjectHelper<Contest>.CloneObjectJson(payload.Data);
                await loadCriterias(ContestInfo?.Id);

                var result = await criterionService.GetAll();

                eventAggregator.GetEvent<PassData<Contest>>().Unsubscribe(SubscribeContestData);
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex.Message, eventAggregator);
                throw;
            }
        }

        private async Task loadCriterias(Guid? contestId)
        {
            try
            {
                var result = await criteriaService.GetFullCriteriaByContest(contestId) ?? new List<CustomCriteria>();
                CustomCriteriaList = new ObservableCollection<CustomCriteria>(result);
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex.Message, eventAggregator);
            }
        }

        #endregion

        #region Property

        private Contest? _contestInfo;
        public Contest? ContestInfo
        {
            get
            {
                if (_contestInfo == null)
                    _contestInfo = new Contest();
                return _contestInfo;
            }
            set { SetProperty(ref _contestInfo, value); }
        }

        #endregion

        #region Collections

        private ObservableCollection<CustomCriteria>? _customCriteriaList;
        public ObservableCollection<CustomCriteria>? CustomCriteriaList
        {
            get
            {
                if (_customCriteriaList == null)
                    _customCriteriaList = new ObservableCollection<CustomCriteria>();
                return _customCriteriaList;
            }
            set { SetProperty(ref _customCriteriaList, value); }
        }

        private ObservableCollection<Criteria?>? _criteriaList;
        public ObservableCollection<Criteria?>? CriteriaList
        {
            get
            {
                if (_criteriaList == null)
                    _criteriaList = new ObservableCollection<Criteria?>();
                return _criteriaList;
            }
            set { SetProperty(ref _criteriaList, value); }
        }

        #endregion

    }
}
