using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabulation.UI.AdminManagement.BaseViewModels
{
    public abstract class DialogsBaseViewModel : PrismBaseViewModel, IDialogAware
    {
        public virtual DialogCloseListener RequestClose { get; }
        public abstract bool CanCloseDialog();
        public abstract void OnDialogClosed();
        public abstract void OnDialogOpened(IDialogParameters parameters);
    }
}
