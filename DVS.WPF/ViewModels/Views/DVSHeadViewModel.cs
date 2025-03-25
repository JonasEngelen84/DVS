using DVS.Domain.Services.Interfaces;
using DVS.WPF.Commands.ClothesCommands;
using DVS.WPF.Commands.EmployeeCommands;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class DVSHeadViewModel(
        DVSListingViewModel dVSListingViewModel,
        ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        IDirtyEntitySaver dirtyEntitySaver)
        : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;

        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(
            employeeStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            modalNavigationStore,
            dVSListingViewModel);

        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(
            modalNavigationStore,
            categoryStore,
            seasonStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            employeeStore,
            dirtyEntitySaver);
    }
}
