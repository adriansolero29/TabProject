using Base;
using Helpers;
using ObjectLoader.Event;
using Prism.Events;
using Prism.Navigation.Regions;
using RepositoryServices.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates;

namespace Tabulation.UI.AdminManagement.BaseViewModels
{
    public abstract class BaseVMWithCRUDCommands : PrismBaseViewModel
    {
        #region Commands

        private DelegateCommand<string>? _add;
        public DelegateCommand<string>? Add =>
            _add ?? (_add = new DelegateCommand<string>(add));

        private DelegateCommand<string>? _edit;
        public DelegateCommand<string>? Edit =>
            _edit ?? (_edit = new DelegateCommand<string>(edit));

        private DelegateCommand<string>? _delete;
        public DelegateCommand<string>? Delete =>
            _delete ?? (_delete = new DelegateCommand<string>(delete));

        private DelegateCommand<string>? _refresh;
        public DelegateCommand<string>? Refresh =>
            _refresh ?? (_refresh = new DelegateCommand<string>(refresh));

        #endregion

        #region Methods

        public abstract void add(string? p = null);
        public abstract void edit(string? p = null);
        public abstract void delete(string? p = null);
        public abstract void refresh(string? p = null);

        #endregion
    }
}
