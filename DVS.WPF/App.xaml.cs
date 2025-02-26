using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Commands.ClothesCommands;
using DVS.Domain.Commands.ClothesSizeCommands;
using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Commands.EmployeeCommands;
using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Services;
using DVS.EntityFramework;
using DVS.EntityFramework.Commands.CategoryCommands;
using DVS.EntityFramework.Commands.ClothesCommands;
using DVS.EntityFramework.Commands.ClothesSizeCommands;
using DVS.EntityFramework.Commands.EmployeeClothesSizeCommands;
using DVS.EntityFramework.Commands.EmployeeCommands;
using DVS.EntityFramework.Commands.SeasonCommands;
using DVS.EntityFramework.Services;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace DVS.WPF
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string? connectionString = context.Configuration.GetConnectionString("sqlite");

                    services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
                    services.AddSingleton<DVSDbContextFactory>();

                    services.AddSingleton<IDataLoaderService, DataLoaderService>();
                    services.AddSingleton<ICreateCategoryCommand, CreateCategoryCommand>();
                    services.AddSingleton<IUpdateCategoryCommand, UpdateCategoryCommand>();
                    services.AddSingleton<IDeleteCategoryCommand, DeleteCategoryCommand>();
                    services.AddSingleton<ICreateSeasonCommand, CreateSeasonCommand>();
                    services.AddSingleton<IUpdateSeasonCommand, UpdateSeasonCommand>();
                    services.AddSingleton<IDeleteSeasonCommand, DeleteSeasonCommand>();
                    services.AddSingleton<ICreateClothesCommand, CreateClothesCommand>();
                    services.AddSingleton<IUpdateClothesCommand, UpdateClothesCommand>();
                    services.AddSingleton<IDeleteClothesCommand, DeleteClothesCommand>();
                    services.AddSingleton<ICreateEmployeeCommand, CreateEmployeeCommand>();
                    services.AddSingleton<IUpdateEmployeeCommand, UpdateEmployeeCommand>();
                    services.AddSingleton<IDeleteEmployeeCommand, DeleteEmployeeCommand>();
                    services.AddSingleton<ICreateClothesSizeCommand, CreateClothesSizeCommand>();
                    services.AddSingleton<IUpdateClothesSizeCommand, UpdateClothesSizeCommand>();
                    services.AddSingleton<IDeleteClothesSizeCommand, DeleteClothesSizeCommand>();
                    services.AddSingleton<ICreateEmployeeClothesSizeCommand, CreateEmployeeClothesSizeCommand>();
                    services.AddSingleton<IUpdateEmployeeClothesSizeCommand, UpdateEmployeeClothesSizeCommand>();
                    services.AddSingleton<IDeleteEmployeeClothesSizeCommand, DeleteEmployeeClothesSizeCommand>();

                    services.AddSingleton<DVSHeadViewModel>();
                    services.AddSingleton<DVSSizeViewModel>();
                    services.AddSingleton<DVSListingViewModel>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<SelectedClothesSizeStore>();
                    services.AddSingleton<SelectedEmployeeClothesSizeStore>();
                    services.AddSingleton<CategoryStore>();
                    services.AddSingleton<SeasonStore>();
                    services.AddSingleton<ClothesStore>();
                    services.AddSingleton<EmployeeStore>();
                    services.AddSingleton<ClothesSizeStore>();
                    services.AddSingleton<EmployeeClothesSizeStore>();

                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            LoadData();
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

        private async void LoadData()
        {
            var dataLoader = _host.Services.GetRequiredService<IDataLoaderService>();
            var categoryStore = _host.Services.GetRequiredService<CategoryStore>();
            var seasonStore = _host.Services.GetRequiredService<SeasonStore>();
            var clothesStore = _host.Services.GetRequiredService<ClothesStore>();
            var employeeStore = _host.Services.GetRequiredService<EmployeeStore>();
            var clothesSizeStore = _host.Services.GetRequiredService<ClothesSizeStore>();
            var employeeClothesSizeStore = _host.Services.GetRequiredService<EmployeeClothesSizeStore>();

            var categories = await dataLoader.LoadCategoriesAsync();
            var seasons = await dataLoader.LoadSeasonsAsync();
            var clothes = await dataLoader.LoadClothesAsync();
            var employees = await dataLoader.LoadEmployeesAsync();
            var clothesSizes = await dataLoader.LoadClothesSizesAsync();
            var employeeClothesSizes = await dataLoader.LoadEmployeeClothesSizesAsync();

            categoryStore.Load(categories);
            seasonStore.Load(seasons);
            clothesStore.Load(clothes);
            employeeStore.Load(employees);
            clothesSizeStore.Load(clothesSizes);
            employeeClothesSizeStore.Load(employeeClothesSizes);
        }
    }
}
