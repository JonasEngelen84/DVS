using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ClothesItemReceivedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                         Action<DetailedClothesListingItemViewModel> addItemToAvailableClothesList,
                                                         Action<Guid> addItemToEditedClothesList,
                                                         Action<Guid> removeItemFromEditedClothesList)
                                                         : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<DetailedClothesListingItemViewModel> _addItemToAvailableClothesList = addItemToAvailableClothesList;
        public readonly Action<Guid> _addItemToEditedClothesList = addItemToEditedClothesList;
        public readonly Action<Guid> _removeItemFromEditedClothesList = removeItemFromEditedClothesList; 

        public override void Execute(object parameter)
        {
            DetailedClothesListingItemViewModel newDclivm;

            DetailedClothesListingItemViewModel? existingClothes = _addEditEmployeeListingViewModel.GetClothesFrom_availableClothesSizes();

            if (existingClothes != null)
            {
                
                DetailedClothesListingItemViewModel? existingClothesSize = _addEditEmployeeListingViewModel.GetClothesSizeFrom_availableClothesSizes();

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

        private static DetailedClothesListingItemViewModel CreateNewDetailedClothesitem(AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel)
        {
            DetailedClothesListingItemViewModel newDclivm = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
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
