using Base;
using MaterialDesignThemes.Wpf;
using ObjectLoader.Event;
using RepositoryServices.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabulation.UI.AdminManagement.BaseViewModels;
using Templates;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class AddContestWindowViewModel : DialogsBaseViewModel
    {
        public MainProviderComposition MainProviderComposition { get; }
        private readonly IContestService contestService;

        public AddContestWindowViewModel(MainProviderComposition mainProviderComposition, IContestService? contestService)
        {
            MainProviderComposition = mainProviderComposition;
            this.contestService = contestService ?? throw new ArgumentNullException(nameof(contestService));

            MainProviderComposition.EventAggregator?.GetEvent<PassData<Contest>>().Subscribe(SetData);
        }

        #region Commands

        private DelegateCommand? _save;
        public DelegateCommand? Save =>
            _save ?? (_save = new DelegateCommand(save));

        #endregion

        #region Methods

        private async void save()
        {
            await contestService.Create(Contest);
            RequestClose.Invoke();
            MainProviderComposition.EventAggregator.GetEvent<NotifyData>().Publish(nameof(UIContainerViewModel));
        }

        private void SetData(Payload<Contest> payload)
        {
            if (payload != null && payload.Data != null )
            {
                if (payload.Command == "update")
                    Contest = payload.Data;
            }
        }

        #endregion

        #region Properties

        private Contest? _contest;

        public Contest? Contest
        {
            get
            {
                if (_contest == null)
                    _contest = new Contest();
                return _contest;
            }
            set { SetProperty(ref _contest, value); }
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
