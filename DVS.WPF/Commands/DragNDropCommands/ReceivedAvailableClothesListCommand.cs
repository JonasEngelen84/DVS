using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ReceivedAvailableClothesListCommand(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        Action<AvailableClothesSizeItem> addItemToAvailableClothesList,
        Action<AvailableClothesSizeItem> addItemToEditedClothesSizesList,
        Action<AvailableClothesSizeItem> removeItemFromEditedClothesSizesList,
        Action<Clothes> addItemToEditedClothesList,
        Action<Clothes> removeItemFromEditedClothesList)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AvailableClothesSizeItem existingAcsi = addEditEmployeeListingViewModel.GetAvailableClothesSizeItemFrom_availableClothesSizes();
            Clothes? existingClothes = addEditEmployeeListingViewModel.GetClothesFrom_availableClothesSizes();

            if (existingAcsi != null)
                existingAcsi.Quantity += 1;
            else
            {
                if (existingClothes == null)
                {
                    if (Confirm($"Diese Bekleidung ist nicht Im Bestand.\nSoll ein neues Objekt dieser Bekleidung angelegt werden?" +
                                $"\nAndernfalls wird die zu entfernende Bekleidung gelöscht!", "Bekleidung nicht im Vorrat"))
                    {
                        
                    }
                }
                else
                {
                    AvailableClothesSizeItem newAcsi = CreateNewAcsi(addEditEmployeeListingViewModel);
                    addItemToAvailableClothesList?.Invoke(newAcsi);
                    UpdateEditedLists(newAcsi);
                }
            }
        }

        private static AvailableClothesSizeItem CreateNewAcsi(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            AvailableClothesSizeItem newAcsi = new(_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.ClothesSize)
            {
                Quantity = 1
            };

            return newAcsi;
        }

        private void UpdateEditedLists(AvailableClothesSizeItem acsi)
        {
            AvailableClothesSizeItem? existingAcsi = addEditEmployeeListingViewModel.GetClothesSizeFrom_editedClothesSizesList();
            if (existingAcsi == null) addItemToEditedClothesSizesList.Invoke(acsi);

            Clothes? existingClothes = addEditEmployeeListingViewModel.GetClothesFrom_editedClothesList();
            if (existingClothes == null) addItemToEditedClothesList.Invoke(acsi.ClothesSize.Clothes);
        }
    }
}
