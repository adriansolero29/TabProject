using System;
using System.Collections.Generic;
using System.Text;

namespace Base
{
    public abstract class BaseViewModel : PropChanged
    {
        private string? _dialogGuid;
        public string? DialogGuid
        {
            get
            {
                return _dialogGuid;
            }
            set
            {
                _dialogGuid = value;
                OnPropertyChanged();
            }
        }
    }
}
