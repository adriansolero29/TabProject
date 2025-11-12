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
using Tabulation.UI.AdminManagement.BaseViewModels;
using Templates;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class CriteriaViewerViewModel : PrismBaseViewModel
    {
        public MainProviderComposition MainProviderComposition { get; }

        private readonly ICriteriaService criteriaService;
        private readonly ICriterionService criterionService;

        public CriteriaViewerViewModel(MainProviderComposition mainProviderComposition, ICriteriaService criteriaService, ICriterionService criterionService)
        {
            MainProviderComposition = mainProviderComposition;
            this.criteriaService = criteriaService ?? throw new ArgumentNullException(nameof(criteriaService));
            this.criterionService = criterionService ?? throw new ArgumentNullException(nameof(criterionService));

            MainProviderComposition.EventAggregator.GetEvent<PassData<Contest>>().Subscribe(SubscribeContestData);
        }

        #region Commands

        private DelegateCommand<CustomCriteria>? _editCriteria;
        public DelegateCommand<CustomCriteria>? EditCriteria =>
            _editCriteria ?? (_editCriteria = new DelegateCommand<CustomCriteria>(editCriteria));

        private DelegateCommand? _deleteCriteria;
        public DelegateCommand? DeleteCriteria =>
            _deleteCriteria ?? (_deleteCriteria = new DelegateCommand(deleteCriteria));

        private DelegateCommand? _addCriteria;
        public DelegateCommand? AddCriteria =>
            _addCriteria ?? (_addCriteria = new DelegateCommand(addCriteria));

        private DelegateCommand? _refreshCriteria;
        public DelegateCommand? RefreshCriteria =>
            _refreshCriteria ?? (_refreshCriteria = new DelegateCommand(refreshCriteria));

        private DelegateCommand _addCandidate;
        public DelegateCommand AddCandidate =>
            _addCandidate ?? (_addCandidate = new DelegateCommand(addCandidate));

        #endregion

        #region Methods

        private async void addCandidate()
        {
            await UINavigator.OpenDialog(MainProviderComposition.ContainerProvider, MainProviderComposition.DialogService, Helpers.ViewRegionNames.AddCandidateWindow, ContestInfo);
            MainProviderComposition.EventAggregator.GetEvent<PassData<Contest>>().Publish(new Payload<Contest> { Data = ContestInfo });
        }

        void refreshCriteria()
        {
        }

        private async void addCriteria()
        {
            await UINavigator.OpenDialog(MainProviderComposition.ContainerProvider, MainProviderComposition.DialogService, Helpers.ViewRegionNames.AddCriteriaWindow, ContestInfo);
            MainProviderComposition.EventAggregator.GetEvent<PassData<Contest>>().Publish(new Payload<Contest> { Data = ContestInfo });
        }

        private void deleteCriteria()
        {

        }

        private async void editCriteria(CustomCriteria o)
        {
            await UINavigator.ShowDialogPassData<CustomCriteria>(MainProviderComposition.ContainerProvider, Helpers.ViewRegionNames.AddCriteriaWindow, "", MainProviderComposition.EventAggregator, o, "update", MainProviderComposition.DialogService);
        }

        private async void SubscribeContestData(Payload<Contest> payload)
        {
            try
            {
                ContestInfo = Helpers.ObjectHelper<Contest>.CloneObjectJson(payload.Data);
                await loadCriterias(ContestInfo?.Id);

                var result = await criterionService.GetAll();

                MainProviderComposition.EventAggregator.GetEvent<PassData<Contest>>().Unsubscribe(SubscribeContestData);
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex, MainProviderComposition.EventAggregator);
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
                Helpers.ErrorNotification.SendErrorNotification(ex, MainProviderComposition.EventAggregator);
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
