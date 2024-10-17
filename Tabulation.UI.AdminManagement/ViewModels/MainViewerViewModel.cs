using Base;
using ObjectLoader.Event;
using RepositoryServices.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class MainViewerViewModel : PrismBaseViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ICriteriaService criteriaService;

        public MainViewerViewModel(IEventAggregator eventAggregator, ICriteriaService criteriaService)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.criteriaService = criteriaService ?? throw new ArgumentNullException(nameof(criteriaService));

            eventAggregator.GetEvent<PassData<Contest>>().Subscribe(SubscribeContestData);
        }

        #region Methods

        private async void SubscribeContestData(Payload<Contest> payload)
        {
            ContestInfo = Helpers.ObjectHelper<Contest>.CloneObjectJson(payload.Data);
            await loadCriterias(ContestInfo?.Id);

            eventAggregator.GetEvent<PassData<Contest>>().Unsubscribe(SubscribeContestData);
        }

        private async Task loadCriterias(Guid? contestId)
        {
            try
            {
                var result = await criteriaService.GetByContest(contestId);
                CriteriaList?.Clear();
                CriteriaList = new ObservableCollection<Criteria?>(result);
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
