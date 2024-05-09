using DVS.Stores;
using DVS.ViewModels.AddViewModels;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Windows.Media;

namespace DVS.ViewModels
{
    /// <summary>
    /// Hauptklasse zur Implementierung der allgemeinen Betrefflickeiten der App:
    /// Dieser Klasse wird in App.xaml.cs der Datenkontext zugeteilt.
    /// sowie die Instanzen von "ModalNavigationStore" und "DVSViewModel" übergeben.
    /// 
    /// In dieser Klasse wird das handling der Modals vorgenommen.
    /// Sobald sich das aktuelle Modal ändert wird _modalNavigationStore.CurrentViewModelChanged()
    /// aufgerufen, und das neue aktuelle Modal sowie der Zustand von IsOpen, anhand der Methode
    /// ModalNavigationStore_CurrentViewModelChanged() übergeben.
    /// Anhand von Dispose() wird das aktuelle Modal geschlossen.
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        public DVSViewModel DVSViewModel { get; }
        private readonly ModalNavigationStore _modalNavigationStore;

        // Pointer auf das aktuelle Modal(ViewModel) 
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        // Pointer auf "_modalNavigationStore.IsOpen" zur Prüfung ob das aktuelle Modal bereits geöffnet ist.
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(ModalNavigationStore modalNavigationStore, DVSViewModel dVSViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            DVSViewModel = dVSViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;

            _modalNavigationStore.CurrentViewModel = new AddViewModel();
        }

        // Schließen des aktuellen Modal.
        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }

        // Aktualisieren des aktuellen Modal.
        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
