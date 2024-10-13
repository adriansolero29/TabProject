using Base;
using Helpers;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using ObjectLoader.Event;
using RepositoryServices.Event;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using Tabulation.UI.AdminManagement.BaseViewModels;
using Tabulation.UI.AdminManagement.Views;
using Templates;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.ViewModels
{
    public class UIContainerViewModel : BaseVMWithCRUDCommands
    {
        private readonly DialogCoordinator? _dialogCoordinator;
        private readonly IContestService contestService;
        private readonly IContainerProvider container;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public UIContainerViewModel(IContainerProvider container, IEventAggregator eventAggregator, IRegionManager regionManager, IContestService? contestService)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            this.regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            this.contestService = contestService ?? throw new ArgumentNullException(nameof(contestService));
            _dialogCoordinator = (DialogCoordinator?)DialogCoordinator.Instance;

            Init();
        }

        #region Startup

        private async void ShowLogin()
        {
            if (_dialogCoordinator != null)
            {
                await Task.Delay(3000);
                var loginData = await _dialogCoordinator.ShowLoginAsync(this, "Administrator Login", "Enter Username and Password");
                var loading = await _dialogCoordinator.ShowProgressAsync(this, "Loading", "Checking Credentials");
                // login logic

                loading.SetIndeterminate();
                // after login is checked
                await Task.Delay(2000);
                // if login accepted
                await _dialogCoordinator.ShowMessageAsync(this, "Login Successful", "Welcome User");
                await loading.CloseAsync();
            }
        }

        #endregion

        #region Methods

        private async void Init()
        {
            try
            {
                var contestList = await contestService.GetAll();
                ContestList?.Clear();
                ContestList?.AddRange(contestList);
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex.Message, eventAggregator);
            }
        }

        private void openDetails()
        {
            regionManager.RequestNavigate(Helpers.ViewRegionNames.MainViewerRegion, Helpers.ViewRegionNames.MainViewerForm);
        }

        public async override void add(string? p = null)
        {
            try
            {
                await UINavigator.ShowDialog(container, Helpers.ViewRegionNames.AddContestWindow, Helpers.DialogNames.UIContainerDialog);
                refresh();
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex.Message, eventAggregator);
            }
        }

        public async override void edit(string? p = null)
        {
            if (Helpers.SelectedItem.IsItemValid(SelectedContest?.Id))
                await UINavigator.ShowDialogPassData<Contest>(container, Helpers.ViewRegionNames.AddContestWindow, Helpers.DialogNames.UIContainerDialog, eventAggregator, Helpers.ObjectHelper<Contest>.CloneObjectJson(SelectedContest), "update");
            else
                await UINavigator.ShowDialog(container, Helpers.DialogNames.WarningDialog, Helpers.DialogNames.UIContainerDialog);

            refresh();
        }

        public async override void delete(string? p = null)
        {
            try
            {
                if (Helpers.SelectedItem.IsItemValid(SelectedContest?.Id))
                {
                    var delete = await UINavigator.ShowDialog(container, Helpers.DialogNames.YesNoDialog, DialogNames.UIContainerDialog) ?? false;
                    if ((bool)delete)
                        await contestService.Delete(SelectedContest);
                }
                else
                    await UINavigator.ShowDialog(container, Helpers.DialogNames.WarningDialog, Helpers.DialogNames.UIContainerDialog);

                refresh();
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex.Message, eventAggregator);
            }
        }

        public override void refresh(string? p = null)
        {
            Init();
        }

        #endregion

        #region Properties

        private Contest? _selectedContest;
        public Contest? SelectedContest
        {
            get
            {
                if (_selectedContest == null)
                    _selectedContest = new Contest();
                return _selectedContest;
            }
            set 
            { 
                SetProperty(ref _selectedContest, value);
                openDetails();
            }
        }

        #endregion

        #region Collections

        private ObservableCollection<Contest?>? _contestList;
        public ObservableCollection<Contest?>? ContestList
        {
            get
            {
                if (_contestList == null)
                    _contestList = new ObservableCollection<Contest?>();
                return _contestList;
            }
            set { SetProperty(ref _contestList, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand? _openDetails;
        public DelegateCommand? OpenDetails => 
            _openDetails ?? (_openDetails = new DelegateCommand(openDetails));

        #endregion

    }
}
