using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ReceivedAvailableClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<AvailableClothesSizeItem> addItemToEditedClothesSizesList,
        Action<Clothes> addItemToEditedClothesList)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AvailableClothesSizeItem? existingAcsi = addEditEmployeeListingViewModel.GetAvailableClothesSizeItemFrom_availableClothesSizes();
            existingAcsi.Quantity += 1;

            existingAcsi = addEditEmployeeListingViewModel.GetClothesSizeFrom_clothesSizesToEdit();
            if (existingAcsi == null) addItemToEditedClothesSizesList.Invoke(existingAcsi);

            Clothes? existingClothes = addEditEmployeeListingViewModel.GetClothesFrom_clothesToEdit();
            if (existingClothes == null) addItemToEditedClothesList.Invoke(existingAcsi.ClothesSize.Clothes);
        }
    }
}
