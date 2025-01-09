using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Commands.SizeCommands;
using DVS.Domain.Queries;
using DVS.EntityFramework;
using DVS.EntityFramework.Commands.CategoryCommands;
using DVS.EntityFramework.Commands.ClothesCommands;
using DVS.EntityFramework.Commands.ClothesSizeCommands;
using DVS.EntityFramework.Commands.EmployeeCommands;
using DVS.EntityFramework.Commands.EmployeeClothesSizeCommands;
using DVS.EntityFramework.Commands.SeasonCommands;
using DVS.EntityFramework.Commands.SizeCommands;
using DVS.EntityFramework.Queries;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DVS.WPF
{
    public partial class App : Application
    {
        //private readonly CategoryStore _categoryStore;
        //private readonly SeasonStore _seasonStore;
        //private readonly ClothesStore _clothesStore;
        //private readonly EmployeeStore _employeeStore;
        //private readonly SizeStore _sizeStore;
        //private readonly ClothesSizeStore _clothesSizeStore;
        //private readonly EmployeeClothesSizesStore _employeeClothesSizesStore;
        //private readonly ModalNavigationStore _modalNavigationStore;
        //private readonly DVSListingViewModel _dVSListingViewModel;
        //private readonly DVSDetailedViewModel _dVSDetailedViewModel;
        //private readonly DVSHeadViewModel _dVSHeadViewModel;
        //private readonly SelectedDetailedClothesItemStore _selectedDetailedClothesItemStore;
        //private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore;
        //private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel;

        //private readonly DVSDbContextFactory _dVSDbContextFactory;

        //private readonly IGetAllCategoriesQuery _getAllCategoriesQuery;
        //private readonly ICreateCategoryCommand _createCategoryCommand;
        //private readonly IUpdateCategoryCommand _updateCategoryCommand;
        //private readonly IDeleteCategoryCommand _deleteCategoryCommand;

        //private readonly IGetAllSeasonsQuery _getAllSeasonsQuery;
        //private readonly ICreateSeasonCommand _createSeasonCommand;
        //private readonly IUpdateSeasonCommand _updateSeasonCommand;
        //private readonly IDeleteSeasonCommand _deleteSeasonsCommand;

        //private readonly IGetAllClothesQuery _getAllClothesQuery;
        //private readonly ICreateClothesCommand _createClothesCommand;
        //private readonly IUpdateClothesCommand _updateClothesCommand;
        //private readonly IDeleteClothesCommand _deleteClothesCommand;

        //private readonly IGetAllEmployeesQuery _getAllEmployeesQuery;
        //private readonly ICreateEmployeeCommand _createEmployeeCommand;
        //private readonly IUpdateEmployeeCommand _updateEmployeeCommand;
        //private readonly IDeleteEmployeeCommand _deleteEmployeeCommand;

        //private readonly IGetAllClothesSizesQuery _getAllClothesSizesQuery;
        //private readonly ICreateClothesSizeCommand _createClothesSizeCommand;
        //private readonly IUpdateClothesSizeCommand _updateClothesSizeCommand;
        //private readonly IDeleteClothesSizeCommand _deleteClothesSizeCommand;

        //private readonly IGetAllEmployeeClothesSizesQuery _getAllEmployeeClothesSizesQuery;
        //private readonly ICreateEmployeeClothesSizeCommand _createEmployeeClothesSizeCommand;
        //private readonly IUpdateEmployeeClothesSizeCommand _updateEmployeeClothesSizeCommand;
        //private readonly IDeleteEmployeeClothesSizeCommand _deleteEmployeeClothesSizeCommand;

        //private readonly IGetAllSizesQuery _getAllSizesQuery;
        //private readonly IUpdateSizeCommand _updateSizeCommand;

        //TODO: Beschreibung
        // Nuget Paket: Microsoft.Extensions.Hosting
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string connectionString = context.Configuration.GetConnectionString("sqlite");

                    services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                    services.AddSingleton<DVSDbContextFactory>();

                    services.AddSingleton<IGetAllCategoriesQuery, GetAllCategoriesQuery>();
                    services.AddSingleton<ICreateCategoryCommand, CreateCategoryCommand>();
                    services.AddSingleton<IUpdateCategoryCommand, UpdateCategoryCommand>();
                    services.AddSingleton<IDeleteCategoryCommand, DeleteCategoryCommand>();                    
                    services.AddSingleton<IGetAllSeasonsQuery, GetAllSeasonsQuery>();
                    services.AddSingleton<ICreateSeasonCommand, CreateSeasonCommand>();
                    services.AddSingleton<IUpdateSeasonCommand, UpdateSeasonCommand>();
                    services.AddSingleton<IDeleteSeasonCommand, DeleteSeasonCommand>();
                    services.AddSingleton<IGetAllClothesQuery, GetAllClothesQuery>();
                    services.AddSingleton<ICreateClothesCommand, CreateClothesCommand>();
                    services.AddSingleton<IUpdateClothesCommand, UpdateClothesCommand>();
                    services.AddSingleton<IDeleteClothesCommand, DeleteClothesCommand>();
                    services.AddSingleton<IGetAllEmployeesQuery, GetAllEmployeesQuery>();
                    services.AddSingleton<ICreateEmployeeCommand, CreateEmployeeCommand>();
                    services.AddSingleton<IUpdateEmployeeCommand, UpdateEmployeeCommand>();
                    services.AddSingleton<IDeleteEmployeeCommand, DeleteEmployeeCommand>();
                    services.AddSingleton<IGetAllClothesSizesQuery, GetAllClothesSizesQuery>();
                    services.AddSingleton<ICreateClothesSizeCommand, CreateClothesSizeCommand>();
                    services.AddSingleton<IUpdateClothesSizeCommand, UpdateClothesSizeCommand>();
                    services.AddSingleton<IDeleteClothesSizeCommand, DeleteClothesSizeCommand>();
                    services.AddSingleton<IGetAllEmployeeClothesSizesQuery, GetAllEmployeeClothesSizesQuery>();
                    services.AddSingleton<ICreateEmployeeClothesSizeCommand, CreateEmployeeClothesSizeCommand>();
                    services.AddSingleton<IUpdateEmployeeClothesSizeCommand, UpdateEmployeeClothesSizeCommand>();
                    services.AddSingleton<IDeleteEmployeeClothesSizeCommand, DeleteEmployeeClothesSizeCommand>();
                    services.AddSingleton<IGetAllSizesQuery, GetAllSizesQuery>();
                    services.AddSingleton<IUpdateSizeCommand, UpdateSizeCommand>();

                    services.AddSingleton<DVSHeadViewModel>();
                    services.AddSingleton<DVSListingViewModel>();
                    services.AddSingleton<DVSDetailedViewModel>();
                    services.AddSingleton<AddEditEmployeeListingViewModel>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<SelectedDetailedClothesItemStore>();
                    services.AddSingleton<SelectedDetailedEmployeeClothesItemStore>();
                    services.AddSingleton<CategoryStore>();
                    services.AddSingleton<SeasonStore>();
                    services.AddSingleton<ClothesStore>();
                    services.AddSingleton<EmployeeStore>();
                    services.AddSingleton<SizeStore>();
                    services.AddSingleton<ClothesSizeStore>();
                    services.AddSingleton<EmployeeClothesSizesStore>();

                    services.AddTransient<DVSListingViewModel>(CreateDVSListingViewModel);

                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();

            //_dVSDbContextFactory = new(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            //_getAllCategoriesQuery = new GetAllCategoriesQuery(_dVSDbContextFactory);
            //_createCategoryCommand = new CreateCategoryCommand(_dVSDbContextFactory);
            //_updateCategoryCommand = new UpdateCategoryCommand(_dVSDbContextFactory);
            //_deleteCategoryCommand = new DeleteCategoryCommand(_dVSDbContextFactory);

            //_getAllSeasonsQuery = new GetAllSeasonsQuery(_dVSDbContextFactory);
            //_createSeasonCommand = new CreateSeasonCommand(_dVSDbContextFactory);
            //_updateSeasonCommand = new UpdateSeasonCommand(_dVSDbContextFactory);
            //_deleteSeasonsCommand = new DeleteSeasonCommand(_dVSDbContextFactory);

            //_getAllClothesQuery = new GetAllClothesQuery(_dVSDbContextFactory);
            //_createClothesCommand = new CreateClothesCommand(_dVSDbContextFactory);
            //_updateClothesCommand = new UpdateClothesCommand(_dVSDbContextFactory);
            //_deleteClothesCommand = new DeleteClothesCommand(_dVSDbContextFactory);

            //_getAllEmployeesQuery = new GetAllEmployeesQuery(_dVSDbContextFactory);
            //_createEmployeeCommand = new CreateEmployeeCommand(_dVSDbContextFactory);
            //_updateEmployeeCommand = new UpdateEmployeeCommand(_dVSDbContextFactory);
            //_deleteEmployeeCommand = new DeleteEmployeeCommand(_dVSDbContextFactory);

            //_getAllClothesSizesQuery = new GetAllClothesSizesQuery(_dVSDbContextFactory);
            //_createClothesSizeCommand = new CreateClothesSizeCommand(_dVSDbContextFactory);
            //_updateClothesSizeCommand = new UpdateClothesSizeCommand(_dVSDbContextFactory);
            //_deleteClothesSizeCommand = new DeleteClothesSizeCommand(_dVSDbContextFactory);

            //_getAllEmployeeClothesSizesQuery = new GetAllEmployeeClothesSizesQuery(_dVSDbContextFactory);
            //_createEmployeeClothesSizeCommand = new CreateEmployeeClothesSizeCommand(_dVSDbContextFactory);
            //_updateEmployeeClothesSizeCommand = new UpdateEmployeeClothesSizeCommand(_dVSDbContextFactory);
            //_deleteEmployeeClothesSizeCommand = new DeleteEmployeeClothesSizeCommand(_dVSDbContextFactory);

            //_getAllSizesQuery = new GetAllSizesQuery(_dVSDbContextFactory);
            //_updateSizeCommand = new UpdateSizeCommand(_dVSDbContextFactory);

            //_modalNavigationStore = new();
            //_selectedDetailedClothesItemStore = new();
            //_selectedDetailedEmployeeClothesItemStore = new();
            //_sizeStore = new(_getAllSizesQuery, _updateSizeCommand);
            //_categoryStore = new(_getAllCategoriesQuery,
            //                     _createCategoryCommand,
            //                     _updateCategoryCommand,
            //                     _deleteCategoryCommand);
            //_seasonStore = new(_getAllSeasonsQuery,
            //                   _createSeasonCommand,
            //                   _updateSeasonCommand,
            //                   _deleteSeasonsCommand);
            //_clothesStore = new(_getAllClothesQuery,
            //                    _createClothesCommand,
            //                    _updateClothesCommand,
            //                    _deleteClothesCommand);
            //_clothesSizeStore = new(_getAllClothesSizesQuery,
            //                        _createClothesSizeCommand,
            //                        _updateClothesSizeCommand,
            //                        _deleteClothesSizeCommand);
            //_employeeClothesSizesStore = new(_getAllEmployeeClothesSizesQuery,
            //                                 _createEmployeeClothesSizeCommand,
            //                                 _updateEmployeeClothesSizeCommand,
            //                                 _deleteEmployeeClothesSizeCommand);
            //_employeeStore = new(_getAllEmployeesQuery,
            //                     _createEmployeeCommand,
            //                     _updateEmployeeCommand,
            //                     _deleteEmployeeCommand);

            //_addEditEmployeeListingViewModel = new(_clothesStore);
            //_dVSListingViewModel = DVSListingViewModel.LoadViewModel(_sizeStore,
            //                                                         _clothesStore,
            //                                                         _employeeStore,
            //                                                         _modalNavigationStore,
            //                                                         _categoryStore,
            //                                                         _seasonStore,
            //                                                         _clothesSizeStore,
            //                                                         _employeeClothesSizesStore,
            //                                                         _selectedDetailedClothesItemStore,
            //                                                         _selectedDetailedEmployeeClothesItemStore,
            //                                                         _addEditEmployeeListingViewModel);
            //_dVSDetailedViewModel = new(_dVSListingViewModel,
            //                            _modalNavigationStore,
            //                            _sizeStore,
            //                            _categoryStore,
            //                            _seasonStore,
            //                            _clothesStore,
            //                            _clothesSizeStore,
            //                            _employeeClothesSizesStore,
            //                            _employeeStore,
            //                            _selectedDetailedClothesItemStore,
            //                            _selectedDetailedEmployeeClothesItemStore,
            //                            _addEditEmployeeListingViewModel,
            //                            _dVSDbContextFactory);
            //_dVSHeadViewModel = new(_dVSListingViewModel,
            //                        _addEditEmployeeListingViewModel,
            //                        _modalNavigationStore,
            //                        _sizeStore,
            //                        _categoryStore,
            //                        _seasonStore,
            //                        _clothesStore,
            //                        _clothesSizeStore,
            //                        _employeeClothesSizesStore,
            //                        _employeeStore,
            //                        _dVSDbContextFactory);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            //MainWindow = new MainWindow()
            //{
            //    DataContext = new MainViewModel(_dVSHeadViewModel,
            //                                    _dVSDetailedViewModel,
            //                                    _modalNavigationStore)
            //};

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

        private DVSListingViewModel CreateDVSListingViewModel(IServiceProvider services)
        {
            return DVSListingViewModel.LoadViewModel(
                services.GetRequiredService<SizeStore>(),
                services.GetRequiredService<ClothesStore>(),
                services.GetRequiredService<EmployeeStore>(),
                services.GetRequiredService<ModalNavigationStore>(),
                services.GetRequiredService<CategoryStore>(),
                services.GetRequiredService<SeasonStore>(),
                services.GetRequiredService<ClothesSizeStore>(),
                services.GetRequiredService<EmployeeClothesSizesStore>(),
                services.GetRequiredService<SelectedDetailedClothesItemStore>(),
                services.GetRequiredService<SelectedDetailedEmployeeClothesItemStore>(),
                services.GetRequiredService<AddEditEmployeeListingViewModel>());
        }
    }
}
