using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    class ClothesItemReceivedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                         Action<DetailedClothesListingItemViewModel> addItemToAvailableClothesList)
                                                         : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<DetailedClothesListingItemViewModel> _addItemToAvailableClothesList = addItemToAvailableClothesList;

        public override void Execute(object parameter)
        {
            DetailedClothesListingItemViewModel newDclivm;

            DetailedClothesListingItemViewModel? existingClothes = _addEditEmployeeListingViewModel.AvailableClothesSizes
                .FirstOrDefault(dclivm => dclivm.Clothes.GuidId == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.GuidId);

            if (existingClothes != null)
            {
                DetailedClothesListingItemViewModel? existingClothesSize = _addEditEmployeeListingViewModel.AvailableClothesSizes
                    .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSizeGuidId);

                if (existingClothesSize != null)
                {
                    existingClothesSize.Quantity += 1;

                    UpdateEditedClothesList(existingClothesSize);
                }                    
                else
                {
                    newDclivm = CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel);

                    _addItemToAvailableClothesList?.Invoke(newDclivm);

                    UpdateEditedClothesList(newDclivm);
                }
            }
            else
            {
                if (Confirm($"Diese Bekleidung ist nicht Im Bestand.\nSoll ein neues Objekt dieser Bekleidung angelegt werden?" +
                $"\nAndernfalls wird die zu entfernende Bekleidung gelöscht!", "Bekleidung nicht im Vorrat"))
                {
                    newDclivm = CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel);

                    _addItemToAvailableClothesList?.Invoke(CreateNewDetailedClothesitem(_addEditEmployeeListingViewModel));

                    UpdateEditedClothesList(newDclivm);
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

        private void UpdateEditedClothesList(DetailedClothesListingItemViewModel dclivm)
        {
            DetailedClothesListingItemViewModel? existingDclivm = _addEditEmployeeListingViewModel.EditedClothesList
                .FirstOrDefault(dclivm => dclivm.ClothesSize?.GuidId == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize?.GuidId);

            if (existingDclivm != null)
                existingDclivm.Quantity += 1;
            else
            {
                DetailedClothesListingItemViewModel newDclivm = new(dclivm.Clothes, dclivm.ClothesSize)
                {
                    Quantity = dclivm.Quantity
                };

                _addEditEmployeeListingViewModel.EditedClothesList.Add(newDclivm);
            }
        }
    }
}
