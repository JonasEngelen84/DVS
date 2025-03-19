using System.ComponentModel;
using System.Windows;

namespace DVS.WPF.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public static bool Confirm(string text, string title)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(text, title, button, icon);
            return dialog == MessageBoxResult.Yes;
        }

        protected virtual void Dispose() { }
    }
}
