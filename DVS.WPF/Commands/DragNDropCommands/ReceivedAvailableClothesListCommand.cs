using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ReceivedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                              Action<AvailableClothesSizeItem> addItemToAvailableClothesList,
                                              Action<Guid> addItemToEditedClothesList,
                                              Action<Guid> removeItemFromEditedClothesList)
                                              : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<AvailableClothesSizeItem> _addItemToAvailableClothesList = addItemToAvailableClothesList;
        public readonly Action<Guid> _addItemToEditedClothesList = addItemToEditedClothesList;
        public readonly Action<Guid> _removeItemFromEditedClothesList = removeItemFromEditedClothesList; 

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
                        //newDclivm = CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel);
                        //_addItemToAvailableClothesList?.Invoke(CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel));
                        //UpdateEditedClothesList(newDclivm);
                        //UpdateEditedClothesList(existingClothesSize.ClothesSize.GuidId);
                    }
                }
                else
                {
                    AvailableClothesSizeItem newAcsi = CreateNewAcsi(_addEditEmployeeListingViewModel);
                    _addItemToAvailableClothesList?.Invoke(newAcsi);
                    UpdateEditedClothesList(newAcsi.ClothesSize.GuidId);
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

        private void UpdateEditedClothesList(Guid guidId)
        {
            Guid? existingClothesSize = _addEditEmployeeListingViewModel.GetClothesSizeFrom_editedClothesList();

            if (existingClothesSize == null)
            {
                _addItemToEditedClothesList?.Invoke(guidId);
            }            
        }
    }
}
