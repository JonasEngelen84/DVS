using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ReceivedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                              Action<ClothesSizeListingItem> addItemToAvailableClothesList,
                                              Action<Guid> addItemToEditedClothesList,
                                              Action<Guid> removeItemFromEditedClothesList)
                                              : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<ClothesSizeListingItem> _addItemToAvailableClothesList = addItemToAvailableClothesList;
        public readonly Action<Guid> _addItemToEditedClothesList = addItemToEditedClothesList;
        public readonly Action<Guid> _removeItemFromEditedClothesList = removeItemFromEditedClothesList; 

        public override void Execute(object parameter)
        {
            ClothesSizeListingItem newDclivm;

            ClothesSizeListingItem? existingClothes = _addEditEmployeeListingViewModel.GetClothesFrom_availableClothesSizes();

            if (existingClothes != null)
            {
                
                ClothesSizeListingItem? existingClothesSize = _addEditEmployeeListingViewModel.GetClothesSizeFrom_availableClothesSizes();

                if (existingClothesSize != null)
                {
                    existingClothesSize.Quantity += 1;

                    UpdateEditedClothesList(existingClothesSize.ClothesSize.GuidId);
                }                    
                else
                {
                    newDclivm = CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel);

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

        private static ClothesSizeListingItem CreateNewDetailedClothesitem(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            ClothesSizeListingItem newDclivm = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize)
            {
                Quantity = 1
            };

            return newDclivm;
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
