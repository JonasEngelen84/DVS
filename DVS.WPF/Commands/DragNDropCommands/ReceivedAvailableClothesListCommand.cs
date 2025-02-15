using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ReceivedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                              Action<AvailableClothesSizeItem> addItemToAvailableClothesList,
                                              Action<AvailableClothesSizeItem> addItemToEditedClothesSizesList,
                                              Action<AvailableClothesSizeItem> removeItemFromEditedClothesSizesList,
                                              Action<Clothes> addItemToEditedClothesList,
                                              Action<Clothes> removeItemFromEditedClothesList)
                                              : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<AvailableClothesSizeItem> _addItemToAvailableClothesList = addItemToAvailableClothesList;
        public readonly Action<AvailableClothesSizeItem> _addItemToEditedClothesSizesList = addItemToEditedClothesSizesList;
        public readonly Action<AvailableClothesSizeItem> _removeItemFromEditedClothesSizesList = removeItemFromEditedClothesSizesList;
        public readonly Action<Clothes> _addItemToEditedClothesList = addItemToEditedClothesList;
        public readonly Action<Clothes> _removeItemFromEditedClothesList = removeItemFromEditedClothesList;

        public override void Execute(object parameter)
        {
            AvailableClothesSizeItem existingAcsi = _addEditEmployeeListingViewModel.GetAvailableClothesSizeItemFrom_availableClothesSizes();
            Clothes? existingClothes = _addEditEmployeeListingViewModel.GetClothesFrom_availableClothesSizes();

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
                    AvailableClothesSizeItem newAcsi = CreateNewAcsi(_addEditEmployeeListingViewModel);
                    _addItemToAvailableClothesList?.Invoke(newAcsi);
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
            AvailableClothesSizeItem? existingAcsi = _addEditEmployeeListingViewModel.GetClothesSizeFrom_editedClothesSizesList();
            if (existingAcsi == null) _addItemToEditedClothesSizesList.Invoke(acsi);

            Clothes? existingClothes = _addEditEmployeeListingViewModel.GetClothesFrom_editedClothesList();
            if (existingClothes == null) _addItemToEditedClothesList.Invoke(acsi.ClothesSize.Clothes);
        }
    }
}
