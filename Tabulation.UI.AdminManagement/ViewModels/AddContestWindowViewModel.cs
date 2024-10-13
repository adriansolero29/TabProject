using Base;
using MaterialDesignThemes.Wpf;
using ObjectLoader.Event;
using RepositoryServices.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class AddContestWindowViewModel : PrismBaseViewModel
    {
        private readonly IContainerProvider container;
        private readonly IEventAggregator? eventAggregator;
        private readonly IContestService contestService;
        public AddContestWindowViewModel(IContainerProvider? container, IEventAggregator? eventAggregator, IContestService? contestService)
        {
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.contestService = contestService ?? throw new ArgumentNullException(nameof(contestService));

            eventAggregator?.GetEvent<PassData<Contest>>().Subscribe(SetData);
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
            DialogHost.CloseDialogCommand.Execute(null, null);
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

    }
}
