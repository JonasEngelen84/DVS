using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS
{
    public partial class App : Application
    {
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly DVSListingViewModel _dVSListingViewModel;
        private readonly DVSDetailedViewModel _dVSDetailedViewModel;
        private readonly DVSHeadViewModel _dVSHeadViewModel;

        public App()
        {
            _categoryStore = new();
            _seasonStore = new();
            _clothesStore = new();
            _employeeStore = new();
            _modalNavigationStore = new();

            _dVSListingViewModel = new(_clothesStore,
                                       _employeeStore,
                                       _modalNavigationStore,
                                       _categoryStore,
                                       _seasonStore);

            _dVSDetailedViewModel = new(_dVSListingViewModel,
                                        _modalNavigationStore,
                                        _categoryStore,
                                        _seasonStore,
                                        _clothesStore,
                                        _employeeStore);

            _dVSHeadViewModel = new(_dVSListingViewModel,
                                    _modalNavigationStore,
                                    _categoryStore,
                                    _seasonStore,
                                    _clothesStore,
                                    _employeeStore);
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_dVSHeadViewModel,
                                                _dVSDetailedViewModel,
                                                _modalNavigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
