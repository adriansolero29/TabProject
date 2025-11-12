using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabulation.UI.AdminManagement.BaseViewModels
{
    public abstract class DialogCreateBase : DialogsBaseViewModel 
    {
        private DelegateCommand? _save;
        public DelegateCommand? Save =>
            _save ?? (_save = new DelegateCommand(ExecuteSave));

        private DelegateCommand? _cancel;
        public DelegateCommand? Cancel =>
            _cancel ?? (_cancel = new DelegateCommand(ExecuteCancel));

        public abstract void ExecuteCancel();

        public abstract void ExecuteSave();
    }
}
