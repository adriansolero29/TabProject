using ObjectLoader.Event;
using RepositoryServices.CustomModel;
using RepositoryServices.Event;
using System.Collections.ObjectModel;
using System.Windows;
using Tabulation.UI.AdminManagement.BaseViewModels;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class AddCriteriaWindowViewModel : DialogCreateBase
    {
        private readonly ICriteriaService criteriaService;
        public MainProviderComposition MainProviderComposition { get; }
        public override DialogCloseListener RequestClose => base.RequestClose;

        public AddCriteriaWindowViewModel(MainProviderComposition mainProviderComposition, ICriteriaService criteriaService)
        {
            MainProviderComposition = mainProviderComposition;
            this.criteriaService = criteriaService;
            MainProviderComposition.EventAggregator.GetEvent<PassData<CustomCriteria>>().Subscribe(SubscribeCriteria);
        }

        #region Commands

        private DelegateCommand? _addCriterion;
        public DelegateCommand? AddCriterion =>
            _addCriterion ?? (_addCriterion = new DelegateCommand(addCriterion));

        private DelegateCommand<Criterion>? _removeCriterion;
        public DelegateCommand<Criterion>? RemoveCriterion =>
            _removeCriterion ?? (_removeCriterion = new DelegateCommand<Criterion>(removeCriterion));

        #endregion

        #region Methods

        public override void ExecuteCancel()
        {
            MainProviderComposition.EventAggregator.GetEvent<PassData<CustomCriteria>>().Unsubscribe(SubscribeCriteria);
            RequestClose.Invoke();
        }

        public override async void ExecuteSave()
        {
            try
            {
                CustomCriteriaInfo.CriteriaInfo.Contest = ContestInfo;
                CustomCriteriaInfo.CriterionList = CriterionListInfo ?? new ObservableCollection<Criterion>();
                await criteriaService.CreateFullCriteria(CustomCriteriaInfo);
            }
            catch (Exception ex)
            {
                MainProviderComposition.EventAggregator.GetEvent<SendExceptionNotification>().Publish(ex);
            }
        }

        void addCriterion()
        {
            CriterionListInfo?.Add(new Criterion { Criteria = CriteriaInfo });
        }

        void removeCriterion(Criterion obj)
        {
            CriterionListInfo?.Remove(obj);
        }

        private async void SubscribeCriteria(Payload<CustomCriteria> payload)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                CustomCriteriaInfo = payload?.Data ?? new CustomCriteria();

                CriterionListInfo?.Clear();
                CriterionListInfo?.AddRange(CustomCriteriaInfo?.CriterionList);
            });
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
            if (parameters.GetValue<Contest>("p1") is Contest contest)
            {
                ContestInfo = Helpers.ObjectHelper<Contest>.CloneObjectJson(contest);
            }
        }

        #endregion

        #region Property

        private Criteria? _criteriaInfo;
        public Criteria? CriteriaInfo
        {
            get
            {
                if (_criteriaInfo == null)
                    _criteriaInfo = new Criteria();
                return _criteriaInfo;
            }
            set { SetProperty(ref _criteriaInfo, value); }
        }

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

        private CustomCriteria? _customCriteriaInfo;

        public CustomCriteria CustomCriteriaInfo
        {
            get
            {
                if (_customCriteriaInfo == null)
                    _customCriteriaInfo = new CustomCriteria();
                return _customCriteriaInfo;
            }
            set { SetProperty(ref _customCriteriaInfo, value); }
        }


        #endregion
    }
}
