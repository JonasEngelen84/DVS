using DVS.Domain.Commands.Category;
using DVS.Domain.Commands.Clothes;
using DVS.Domain.Commands.ClothesSize;
using DVS.Domain.Commands.Employee;
using DVS.Domain.Commands.EmployeeClothesSize;
using DVS.Domain.Commands.Season;
using DVS.Domain.Commands.Size;
using DVS.Domain.Queries;
using DVS.EntityFramework;
using DVS.EntityFramework.Commands.Category;
using DVS.EntityFramework.Commands.Clothes;
using DVS.EntityFramework.Commands.ClothesSize;
using DVS.EntityFramework.Commands.Employee;
using DVS.EntityFramework.Commands.EmployeeClothesSize;
using DVS.EntityFramework.Commands.Season;
using DVS.EntityFramework.Commands.Size;
using DVS.EntityFramework.Queries;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace DVS.WPF
{
    public partial class App : Application
    {
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly SizeStore _sizeStore;
        private readonly ClothesSizeStore _clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly DVSListingViewModel _dVSListingViewModel;
        private readonly DVSDetailedViewModel _dVSDetailedViewModel;
        private readonly DVSHeadViewModel _dVSHeadViewModel;
        private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore;
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore;
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel;

        private readonly DVSDbContextFactory _dVSDbContextFactory;

        private readonly IGetAllCategoriesQuery _getAllCategoriesQuery;
        private readonly ICreateCategoryCommand _createCategoryCommand;
        private readonly IUpdateCategoryCommand _updateCategoryCommand;
        private readonly IDeleteCategoryCommand _deleteCategoryCommand;

        private readonly IGetAllSeasonsQuery _getAllSeasonsQuery;
        private readonly ICreateSeasonCommand _createSeasonCommand;
        private readonly IUpdateSeasonCommand _updateSeasonCommand;
        private readonly IDeleteSeasonCommand _deleteSeasonsCommand;

        private readonly IGetAllClothesQuery _getAllClothesQuery;
        private readonly ICreateClothesCommand _createClothesCommand;
        private readonly IUpdateClothesCommand _updateClothesCommand;
        private readonly IDeleteClothesCommand _deleteClothesCommand;

        private readonly IGetAllEmployeesQuery _getAllEmployeesQuery;
        private readonly ICreateEmployeeCommand _createEmployeeCommand;
        private readonly IUpdateEmployeeCommand _updateEmployeeCommand;
        private readonly IDeleteEmployeeCommand _deleteEmployeeCommand;

        private readonly IGetAllClothesSizesQuery _getAllClothesSizesQuery;
        private readonly ICreateClothesSizeCommand _createClothesSizeCommand;
        private readonly IUpdateClothesSizeCommand _updateClothesSizeCommand;
        private readonly IDeleteClothesSizeCommand _deleteClothesSizeCommand;

        private readonly IGetAllEmployeeClothesSizesQuery _getAllEmployeeClothesSizesQuery;
        private readonly ICreateEmployeeClothesSizeCommand _createEmployeeClothesSizeCommand;
        private readonly IUpdateEmployeeClothesSizeCommand _updateEmployeeClothesSizeCommand;
        private readonly IDeleteEmployeeClothesSizeCommand _deleteEmployeeClothesSizeCommand;

        private readonly IGetAllSizesQuery _getAllSizesQuery;
        private readonly IUpdateSizeCommand _updateSizeCommand;

        public App()
        {
            string connectionString = "Data Source=DVS.db";
            _dVSDbContextFactory = new(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            _getAllCategoriesQuery = new GetAllCategoriesQuery(_dVSDbContextFactory);
            _createCategoryCommand = new CreateCategoryCommand(_dVSDbContextFactory);
            _updateCategoryCommand = new UpdateCategoryCommand(_dVSDbContextFactory);
            _deleteCategoryCommand = new DeleteCategoryCommand(_dVSDbContextFactory);

            _getAllSeasonsQuery = new GetAllSeasonsQuery(_dVSDbContextFactory);
            _createSeasonCommand = new CreateSeasonCommand(_dVSDbContextFactory);
            _updateSeasonCommand = new UpdateSeasonCommand(_dVSDbContextFactory);
            _deleteSeasonsCommand = new DeleteSeasonCommand(_dVSDbContextFactory);

            _getAllClothesQuery = new GetAllClothesQuery(_dVSDbContextFactory);
            _createClothesCommand = new CreateClothesCommand(_dVSDbContextFactory);
            _updateClothesCommand = new UpdateClothesCommand(_dVSDbContextFactory);
            _deleteClothesCommand = new DeleteClothesCommand(_dVSDbContextFactory);

            _getAllEmployeesQuery = new GetAllEmployeesQuery(_dVSDbContextFactory);
            _createEmployeeCommand = new CreateEmployeeCommand(_dVSDbContextFactory);
            _updateEmployeeCommand = new UpdateEmployeeCommand(_dVSDbContextFactory);
            _deleteEmployeeCommand = new DeleteEmployeeCommand(_dVSDbContextFactory);

            _getAllClothesSizesQuery = new GetAllClothesSizesQuery(_dVSDbContextFactory);
            _createClothesSizeCommand = new CreateClothesSizeCommand(_dVSDbContextFactory);
            _updateClothesSizeCommand = new UpdateClothesSizeCommand(_dVSDbContextFactory);
            _deleteClothesSizeCommand = new DeleteClothesSizeCommand(_dVSDbContextFactory);

            _getAllEmployeeClothesSizesQuery = new GetAllEmployeeClothesSizesQuery(_dVSDbContextFactory);
            _createEmployeeClothesSizeCommand = new CreateEmployeeClothesSizeCommand(_dVSDbContextFactory);
            _updateEmployeeClothesSizeCommand = new UpdateEmployeeClothesSizeCommand(_dVSDbContextFactory);
            _deleteEmployeeClothesSizeCommand = new DeleteEmployeeClothesSizeCommand(_dVSDbContextFactory);

            _getAllSizesQuery = new GetAllSizesQuery(_dVSDbContextFactory);
            _updateSizeCommand = new UpdateSizeCommand(_dVSDbContextFactory);

            _modalNavigationStore = new();
            _selectedDetailedClothesItemStore = new();
            _selectedDetailedEmployeeClothesItemStore = new();
                        
            _sizeStore = new(_getAllSizesQuery, _updateSizeCommand);

            _categoryStore = new(_getAllCategoriesQuery,
                                 _createCategoryCommand,
                                 _updateCategoryCommand,
                                 _deleteCategoryCommand);
            
            _seasonStore = new(_getAllSeasonsQuery,
                               _createSeasonCommand,
                               _updateSeasonCommand,
                               _deleteSeasonsCommand);

            _clothesStore = new(_getAllClothesQuery,
                                _createClothesCommand,
                                _updateClothesCommand,
                                _deleteClothesCommand);

            _clothesSizeStore = new(_getAllClothesSizesQuery,
                                    _createClothesSizeCommand,
                                    _updateClothesSizeCommand,
                                    _deleteClothesSizeCommand);

            _employeeClothesSizesStore = new(_getAllEmployeeClothesSizesQuery,
                                             _createEmployeeClothesSizeCommand,
                                             _updateEmployeeClothesSizeCommand,
                                             _deleteEmployeeClothesSizeCommand);

            _employeeStore = new(_getAllEmployeesQuery,
                                 _createEmployeeCommand,
                                 _updateEmployeeCommand,
                                 _deleteEmployeeCommand);

            _addEditEmployeeListingViewModel = new(_clothesStore);

            _dVSListingViewModel = new(_sizeStore, 
                                       _clothesStore,
                                       _employeeStore,
                                       _modalNavigationStore,
                                       _categoryStore,
                                       _seasonStore,
                                       _clothesSizeStore,
                                       _employeeClothesSizesStore,
                                       _selectedDetailedClothesItemStore,
                                       _selectedDetailedEmployeeClothesItemStore,
                                       _addEditEmployeeListingViewModel);

            _dVSDetailedViewModel = new(_dVSListingViewModel,
                                        _modalNavigationStore,
                                        _sizeStore,
                                        _categoryStore,
                                        _seasonStore,
                                        _clothesStore,
                                        _clothesSizeStore,
                                        _employeeClothesSizesStore,
                                        _employeeStore,
                                        _selectedDetailedClothesItemStore,
                                        _selectedDetailedEmployeeClothesItemStore,
                                        _addEditEmployeeListingViewModel);

            _dVSHeadViewModel = new(_dVSListingViewModel,
                                    _addEditEmployeeListingViewModel,
                                    _modalNavigationStore,
                                    _sizeStore,
                                    _categoryStore,
                                    _seasonStore,
                                    _clothesStore,
                                    _clothesSizeStore,
                                    _employeeClothesSizesStore,
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
