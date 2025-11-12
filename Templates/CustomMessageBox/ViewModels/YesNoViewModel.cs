using Base;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templates.CustomMessageBox.ViewModels
{
    public  class YesNoViewModel : PrismBaseViewModel
    {
        #region Commands

        private DelegateCommand? _yes;
        public DelegateCommand? Yes =>
            _yes ?? (_yes = new DelegateCommand(yes));

        private DelegateCommand? _cancel;
        public DelegateCommand? Cancel =>
            _cancel ?? (_cancel = new DelegateCommand(cancel));

        #endregion

        #region Methods

        void cancel()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void yes()
        {
            DialogHost.CloseDialogCommand.Execute(true, null);
        }

        #endregion
    }
}
