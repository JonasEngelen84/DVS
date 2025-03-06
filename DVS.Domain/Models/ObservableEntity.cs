using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DVS.Domain.Models
{
    public class ObservableEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isDirty;
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                OnPropertyChanged(nameof(IsDirty));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // Sobald irgendeine Property sich ändert: Setze IsDirty = true
            if (propertyName != nameof(IsDirty))
            {
                IsDirty = true;
            }
        }
    }
}
