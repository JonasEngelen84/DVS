using System.ComponentModel;

namespace DVS.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        //TODO: Klasse dokumentieren
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Dispose() { }
    }
}
