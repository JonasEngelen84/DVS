using DVS.Domain.Services.Interfaces;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class OpenAddClothesCommand(
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
            AddClothesViewModel addClothesViewModel = new(
                modalNavigationStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                dirtyEntitySaver);

            modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
