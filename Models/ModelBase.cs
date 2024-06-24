using System.ComponentModel;

namespace DVS.Models
{
    public class ModelBase : INotifyPropertyChanged
    {
        //TODO: ModelBase dokumentieren
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual void Dispose() { }
    }
}
