using System.ComponentModel;

namespace DVS.Domain.Models
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //public static bool Confirm(string text, string title)
        //{
        //    MessageBoxButton button = MessageBoxButton.YesNo;
        //    MessageBoxImage icon = MessageBoxImage.Warning;
        //    MessageBoxResult dialog = MessageBox.Show(text, title, button, icon);
        //    return dialog == MessageBoxResult.Yes;
        //}

        protected virtual void Dispose() { }
    }
}
