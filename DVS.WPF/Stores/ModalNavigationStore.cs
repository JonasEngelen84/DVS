using System;
using DVS.ViewModels;

namespace DVS.Stores
{
    /// <summary>
    /// Klasse zur navigation der Modals:
    /// sobald sich das aktuelle Modal (CurrentViewModelBase) ändert,
    /// wird durch Invoke() das "CurrentViewModelChanged" event aufgerufen,
    /// welches für das wechseln der Modals zuständig ist.
    /// 
    /// Eine Instanz von "ModalNavigationStore" wird in App.xaml.cs implementiert.
    /// Somit existiert diese eine Instanz die gesamte Lebensdauer der App hinweg.
    /// Von App.xaml.cs wird diese Instanz weitergereicht an das MainViewModel.
    /// </summary>
    public class ModalNavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _previousViewModel = _currentViewModel;
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        private ViewModelBase _previousViewModel;
        public ViewModelBase PreviousViewModel
        {
            get => _previousViewModel;
            set
            {
                _previousViewModel = value;
            }
        }

        // Bool zur Prüfung ob das aktuelle Modal bereits geöffnet ist.
        public bool IsOpen => CurrentViewModel != null;

        // Event zur Ausführung der Operation.
        public event Action CurrentViewModelChanged;

        // Methode zum Schließen des Modal.
        public void Close() => CurrentViewModel = null;
    }
}
