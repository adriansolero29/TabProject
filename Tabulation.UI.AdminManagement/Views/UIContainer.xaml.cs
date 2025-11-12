using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static Base.EventAggregators;

namespace Tabulation.UI.AdminManagement.Views
{
    /// <summary>
    /// Interaction logic for UIContainer.xaml
    /// </summary>
    public partial class UIContainer : UserControl
    {
        public UIContainer(IEventAggregator? eventAggregator)
        {
            InitializeComponent();

            eventAggregator?.GetEvent<SendExceptionNotification>().Subscribe(activateNotification);
        }

        private void activateNotification(Exception obj)
        {
            var messageQueue = exceptionSnackbar.MessageQueue;
            Task.Factory.StartNew(() => messageQueue?.Enqueue($"ERROR MESSAGE: {obj.Message} - {new StackTrace(obj, true)?.GetFrame(1)?.GetMethod()}"));
        }
    }
}
