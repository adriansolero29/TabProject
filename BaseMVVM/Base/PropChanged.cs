using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Base
{
    [Serializable]
    public class PropChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
