using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows;

namespace DVS
{
    /// <summary>
    /// App.xaml.cs wird genutzt um Konfigurationenen, bei Programmstart, festzulegen.
    /// In App.xaml werden zB. Dictionaries implementiert, welche im gesamten code genutzt weden können.
    /// </summary>
    public partial class App : Application
    {
        // Einzige Instanzen von:
        // DVSViewModel, ModalNavigationStore, SelectedClothesStore und SelectedEmployeeClothesStore
        // Bestehen die ganze App-Lebensdauer und werden der MainViewModel Instanz übergeben.
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly DVSViewModel _dVSViewModel;

        public App()
        {
            _selectedClothesStore = new();
            _selectedEmployeeClothesStore = new();
            _modalNavigationStore = new();
            _dVSViewModel = new(_selectedClothesStore, _selectedEmployeeClothesStore, _modalNavigationStore);
        }


        // Festlegen des DataContext von MainWindow.cs auf MainViewModel.cs
        // Bei der Erstellung der einzigen Istanz von "MainViewModel", wird dieser,
        // die einzigen Instanzen von "ModalNavigationStore" und "DVSViewModel" übergeben.
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_dVSViewModel, _selectedClothesStore, _selectedEmployeeClothesStore, _modalNavigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
