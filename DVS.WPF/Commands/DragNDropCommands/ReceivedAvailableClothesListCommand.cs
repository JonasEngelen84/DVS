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
            if (addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem == null) return;

            AvailableClothesSizeItem? editedAcsi = addEditEmployeeListingViewModel
                .GetAvailableClothesSizeItemFrom_availableClothesSizes(addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem.ClothesSizeId);

            editedAcsi.Quantity += 1;

            AvailableClothesSizeItem? existingAcsi = addEditEmployeeListingViewModel
                .GetClothesSizeFrom_clothesSizesToEdit(addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem.ClothesSizeId);

            if (existingAcsi == null) addItemToEditedClothesSizesList.Invoke(editedAcsi);

            Clothes? existingClothes = addEditEmployeeListingViewModel
                .GetClothesFrom_clothesToEdit(addEditEmployeeListingViewModel.SelectedEmployeeClothesSizeItem.ClothesId);

            if (existingClothes == null) addItemToEditedClothesList.Invoke(editedAcsi.ClothesSize.Clothes);
        }
    }
}
