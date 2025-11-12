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
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
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
    public class UIContainerViewModel : CRUDViewModelBase
    {
        public MainProviderComposition MainProviderComposition { get; }

        private readonly DialogCoordinator? _dialogCoordinator;
        private readonly IContestService contestService;

        public UIContainerViewModel(MainProviderComposition mainProviderComposition, IContestService? contestService)
        {
            MainProviderComposition = mainProviderComposition;
            _dialogCoordinator = (DialogCoordinator?)DialogCoordinator.Instance;
            this.contestService = contestService ?? throw new ArgumentNullException(nameof(contestService));
            MainProviderComposition.EventAggregator.GetEvent<NotifyData>().Subscribe(reload);

            LoadData();
        }

        private void reload(string obj)
        {
            if (obj == nameof(UIContainerViewModel))
            {
                LoadData();
            }
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

        private async void LoadData()
        {
            try
            {
                var contestList = await contestService.GetAll() ?? new List<Contest>();
                ContestList?.Clear();
                ContestList = new ObservableCollection<Contest?>(contestList);
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex, MainProviderComposition.EventAggregator);
            }
        }

        private void openDetails()
        {
            MainProviderComposition.RegionManager.Regions[Helpers.ViewRegionNames.MainViewerRegion].RemoveAll();
            MainProviderComposition.RegionManager.RequestNavigate(Helpers.ViewRegionNames.MainViewerRegion, Helpers.ViewRegionNames.MainViewerForm);
            MainProviderComposition.EventAggregator.GetEvent<PassData<Contest>>().Publish(new Payload<Contest> { Data = SelectedContest });
        }

        public async override void add(string? p = null)
        {
            try
            {
                await UINavigator.OpenDialog(MainProviderComposition.ContainerProvider, MainProviderComposition.DialogService, Helpers.ViewRegionNames.AddContestWindow);
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex, MainProviderComposition.EventAggregator);
            }
        }

        public async override void edit(string? p = null)
        {
            if (Helpers.SelectedItem.IsItemValid(SelectedContest?.Id))
                await UINavigator.ShowDialogPassData<Contest>(MainProviderComposition.ContainerProvider, Helpers.ViewRegionNames.AddContestWindow, Helpers.DialogNames.UIContainerDialog, MainProviderComposition.EventAggregator, Helpers.ObjectHelper<Contest>.CloneObjectJson(SelectedContest), "update", MainProviderComposition.DialogService);
            else
                await UINavigator.ShowDialogHost(MainProviderComposition.ContainerProvider, Helpers.DialogNames.WarningDialog, Helpers.DialogNames.UIContainerDialog);
        }

        public async override void delete(string? p = null)
        {
            try
            {
                if (Helpers.SelectedItem.IsItemValid(SelectedContest?.Id))
                {
                    var delete = await UINavigator.ShowDialogHost(MainProviderComposition.ContainerProvider, Helpers.DialogNames.YesNoDialog, DialogNames.UIContainerDialog) ?? false;
                    if ((bool)delete)
                        await contestService.Delete(SelectedContest);
                }
                else
                    await UINavigator.ShowDialogHost(MainProviderComposition.ContainerProvider, Helpers.DialogNames.WarningDialog, Helpers.DialogNames.UIContainerDialog);

                refresh();
            }
            catch (Exception ex)
            {
                Helpers.ErrorNotification.SendErrorNotification(ex, MainProviderComposition.EventAggregator);
            }
        }

        public override void refresh(string? p = null)
        {
            LoadData();
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
                if (value != null && value?.Id != null)
                {
                    openDetails();
                }
                else
                    MainProviderComposition.RegionManager.Regions[Helpers.ViewRegionNames.MainViewerRegion].RemoveAll();
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
