using Base;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows.Controls;
using static Base.EventAggregators;

namespace Templates
{
    public class UINavigator : PrismBaseViewModel
    {
        public async static Task<object?> ShowDialog(IContainerProvider? container, string viewName, string dialogName)
        {
            object? output = null;
            object? view = container?.Resolve<object>(viewName);
            if (view != null)
                output = await DialogHost.Show(view, dialogName);

            view = null;

            return output;
        }

        public async static Task ShowDialogPassData<T>(IContainerProvider? container, string viewName, string dialogName, IEventAggregator? eventAggregator, T? data, string command) where T : class
        {
            object? view = container?.Resolve<object>(viewName);
            if (view != null)
            {
                await Task.Run(() =>
                {
                    Task.Delay(500);
                    eventAggregator?.GetEvent<PassData<T>>().Publish(new Payload<T> { Data = data, Command = command });
                });

                await DialogHost.Show(view, dialogName);
            }

            view = null;
        }

        public async static void MessageBox(IContainerProvider? container, string viewName, string dialogName)
        {
            var view = container?.Resolve<object>(viewName);
            if (view != null)
                await DialogHost.Show(view, dialogName);
        }
    }
}
