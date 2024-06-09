using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS
{
    /// <summary>
    /// App.xaml.cs wird genutzt um Konfigurationenen, bei Programmstart, festzulegen.
    /// In App.xaml werden zB. Dictionaries implementiert, welche im gesamten code genutzt weden können.
    /// </summary>
    public partial class App : Application
    {
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly SelectedSeasonStore _selectedSeasonStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly DVSDetailedViewModel _dVSDetailedViewModel;
        private readonly DVSEmployeesViewModel _dVSEmployeesViewModel;

        public App()
        {
            _categoryStore = new();
            _seasonStore = new();
            _selectedCategoryStore = new();
            _selectedSeasonStore = new();
            _clothesStore = new();
            _employeeStore = new();
            _selectedClothesStore = new();
            _selectedEmployeeClothesStore = new();
            _modalNavigationStore = new();

            _dVSEmployeesViewModel = new();

            _dVSDetailedViewModel = new(_modalNavigationStore,
                                        _categoryStore,
                                        _seasonStore,
                                        _selectedCategoryStore,
                                        _selectedSeasonStore,
                                        _clothesStore,
                                        _employeeStore,
                                        _selectedClothesStore,
                                        _selectedEmployeeClothesStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_dVSDetailedViewModel,
                                                _selectedClothesStore,
                                                _selectedEmployeeClothesStore,
                                                _selectedCategoryStore,
                                                _selectedSeasonStore,
                                                _modalNavigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
