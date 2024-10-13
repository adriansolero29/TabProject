using System.Windows.Controls;
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

        private void activateNotification(string obj)
        {
            var messageQueue = exceptionSnackbar.MessageQueue;
            Task.Factory.StartNew(() => messageQueue?.Enqueue(obj));
        }
    }
}
