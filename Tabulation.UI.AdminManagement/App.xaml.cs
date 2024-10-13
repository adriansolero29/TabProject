using System.Configuration;
using System.Data;
using System.Windows;
using Tabulation.UI.AdminManagement.Views;

namespace Tabulation.UI.AdminManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstapper = new AdminManagementBoostrapper();
            bootstapper.Run();
        }
    }
}
