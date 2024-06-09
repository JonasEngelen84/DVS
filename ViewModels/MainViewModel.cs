using DVS.Stores;
using DVS.ViewModels.Views;

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
        public DVSDetailedViewModel DVSDetailedViewModel { get; }
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        // Pointer auf das aktuelle Modal(ViewModel) 
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        // Pointer auf "_modalNavigationStore.IsOpen" zur Prüfung ob ein Modal bereits geöffnet ist.
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(DVSDetailedViewModel dVSViewModel,
                             SelectedClothesStore selectedClothesStore,
                             SelectedEmployeeClothesStore selectedEmployeeClothesStore,
                             SelectedCategoryStore _selectedCategoryStore,
                             SelectedSeasonStore _selectedSeasonStore,
                             ModalNavigationStore modalNavigationStore)
        {
            DVSDetailedViewModel = dVSViewModel;
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _modalNavigationStore = modalNavigationStore;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        }

        
        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;

            base.Dispose();
        }

        // Aktualisieren des Modal-ViewModel.
        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
