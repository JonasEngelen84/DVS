using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ClothesItemRemovedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel) : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public override void Execute(object parameter)
        {
            //CheckQuantity();

            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {
                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity -= 1;

                UpdateEditedClothesList(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem);
            }
            else
                ShowErrorMessageBox("Diese Bekleidung ist zur Zeit nicht vorrätig!", "Bekleidung nicht vorhanden");
        }

        private void CheckQuantity()
        {
            switch (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity)
            {
                case 1:
                    ShowErrorMessageBox("Nach der Transaktion ist diese Bekleidung nicht mehr vorrätig!", "Letztes Bekleidungsstück");
                    break;

                case 2:
                    ShowErrorMessageBox("Nach der Transaktion ist diese Bekleidung noch  1  mal vorrätig!", "Sehr geringer Bestand");
                    break;

                case 3:
                    ShowErrorMessageBox("Nach der Transaktion ist diese Bekleidung noch  2  mal vorrätig!", "geringer Bestand");
                    break;

                default:
                    break;
            }
        }

        private void UpdateEditedClothesList(DetailedClothesListingItemViewModel dclivm)
        {
            DetailedClothesListingItemViewModel? existingDclivm = _addEditEmployeeListingViewModel.EditedClothesList
                .FirstOrDefault(dclivm => dclivm.ClothesSize?.GuidId == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize?.GuidId);

            if (existingDclivm != null)
                existingDclivm.Quantity -= 1;
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
