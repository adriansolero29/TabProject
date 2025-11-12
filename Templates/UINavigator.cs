using Base;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using static Base.EventAggregators;

namespace Templates
{
    public class UINavigator : PrismBaseViewModel
    {
        public async static Task<object?> ShowDialogHost(IContainerProvider? container, string viewName, string dialogName)
        {
            object? output = null;
            object? view = container?.Resolve<object>(viewName);
            if (view != null)
                output = await DialogHost.Show(view, dialogName);

            view = null;

            return output;
        }

        public async static Task OpenDialog(IContainerProvider container, IDialogService dialogService, string? viewName, object? param1 = null, object? param2 = null, object? param3 = null, object? param4 = null, object? param5 = null)
        {
            var param = new DialogParameters();
            if (param1 != null) param.Add("p1", param1);
            if (param2 != null) param.Add("p2", param2);
            if (param3 != null) param.Add("p3", param3);
            if (param4 != null) param.Add("p4", param4);
            if (param5 != null) param.Add("p5", param5);

            await dialogService.ShowDialogAsync(viewName, param);
        }

        public async static Task ShowDialogPassData<T>(IContainerProvider? container, string viewName, string dialogName, IEventAggregator? eventAggregator, T? data, string command, IDialogService dialogService) where T : class
        {
            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                dialogService.Show(viewName);

                await Task.Run(() =>
                {
                    Task.Delay(500);
                    eventAggregator?.GetEvent<PassData<T>>().Publish(new Payload<T> { Data = data, Command = command });
                });
            });
        }

        public async static void MessageBox(IContainerProvider? container, string viewName, string dialogName)
        {
            var view = container?.Resolve<object>(viewName);
            if (view != null)
                await DialogHost.Show(view, dialogName);
        }
    }
}
