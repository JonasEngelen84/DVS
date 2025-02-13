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
            AvailableClothesSizeItem newDclivm;

            AvailableClothesSizeItem? existingClothes = _addEditEmployeeListingViewModel.GetClothesFrom_availableClothesSizes();

            if (existingClothes != null)
            {
                
                AvailableClothesSizeItem? existingClothesSize = _addEditEmployeeListingViewModel.GetClothesSizeFrom_availableClothesSizes();

                if (existingClothesSize != null)
                {
                    existingClothesSize.Quantity += 1;

                    UpdateEditedClothesList(existingClothesSize.ClothesSize.GuidId);
                }                    
                else
                {
                    newDclivm = CreateNewAcsi(_addEditEmployeeListingViewModel);

                    _addItemToAvailableClothesList?.Invoke(newDclivm);

                    UpdateEditedClothesList(newDclivm.ClothesSize.GuidId);
                }
            }
            else
            {
                if (Confirm($"Diese Bekleidung ist nicht Im Bestand.\nSoll ein neues Objekt dieser Bekleidung angelegt werden?" +
                $"\nAndernfalls wird die zu entfernende Bekleidung gelöscht!", "Bekleidung nicht im Vorrat"))
                {
                    //newDclivm = CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel);

                    //_addItemToAvailableClothesList?.Invoke(CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel));

                    //UpdateEditedClothesList(newDclivm);
                }
            }
        }

        private static AvailableClothesSizeItem CreateNewAcsi(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            AvailableClothesSizeItem newAcsi = new(_addEditEmployeeListingViewModel.SelectedClothesSizeItem.ClothesSize)
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
                //DetailedClothesListingItemViewModel newDclivm = new(dclivm.Clothes, dclivm.ClothesSize)
                //{
                //    Quantity = dclivm.Quantity
                //};

                _addItemToEditedClothesList?.Invoke(guidId);
            }
            
        }
    }
}
