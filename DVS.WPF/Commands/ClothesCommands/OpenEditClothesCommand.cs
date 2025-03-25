using DVS.Domain.Models;
using DVS.Domain.Services.Interfaces;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class OpenEditClothesCommand(
        ClothesListingItemViewModel clothesListingItemViewModel,
        ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        IDirtyEntitySaver dirtyEntitySaver)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            Clothes clothes = clothesListingItemViewModel.Clothes;

            EditClothesViewModel EditClothesViewModel = new(
                clothes,
                modalNavigationStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                dirtyEntitySaver);

            modalNavigationStore.CurrentViewModel = EditClothesViewModel;
        }
    }
}
