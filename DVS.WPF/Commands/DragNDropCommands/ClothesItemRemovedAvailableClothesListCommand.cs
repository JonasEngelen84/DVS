using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class ClothesItemRemovedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel) : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public override void Execute(object parameter)
        {
            CheckQuantity();

            if (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity > 0)
            {
                DetailedClothesListingItemViewModel editedDclivm = CreateNewDetailedClothesitem();
                RemoveOrUpdateDclivm(editedDclivm);
            }
        }

        private void CheckQuantity()
        {
            switch (_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity)
            {
                case 0:
                    ShowErrorMessageBox("Diese Bekleidung ist zur Zeit nicht vorrätig!", "Bekleidung nicht vorhanden");
                    break;

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

        private DetailedClothesListingItemViewModel CreateNewDetailedClothesitem()
        {
            ClothesSize editedClothesSize = new(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.GuidID,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Size,
                                                (int)_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Quantity - 1,
                                                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSize.Comment);

            ClothesSize existingClothesSize = _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes
                .FirstOrDefault(cs => cs.GuidID == editedClothesSize.GuidID);

            if (existingClothesSize != null)
            {
                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes.Remove(existingClothesSize);
                _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes.Sizes.Add(editedClothesSize);
                existingClothesSize.Clothes.Sizes.Remove(existingClothesSize);
                existingClothesSize.Clothes.Sizes.Add(editedClothesSize);
            }

            return new DetailedClothesListingItemViewModel(_addEditEmployeeListingViewModel.SelectedDetailedClothesItem.Clothes, editedClothesSize);
        }

        private void RemoveOrUpdateDclivm(DetailedClothesListingItemViewModel editedDclivm)
        {
            foreach (DetailedClothesListingItemViewModel dclivm in _addEditEmployeeListingViewModel.AvailableClothesSizes)
            {
                if (dclivm.Clothes.GuidID == editedDclivm.Clothes.GuidID)
                {
                    if (dclivm.ClothesSizeGuidID == _addEditEmployeeListingViewModel.SelectedDetailedClothesItem.ClothesSizeGuidID)
                        _addEditEmployeeListingViewModel.SelectedDetailedClothesItem?.Update(editedDclivm.Clothes, editedDclivm.ClothesSize);
                    else
                        dclivm.Update(editedDclivm.Clothes, null);
                }
            }

            var existingClothesItem = _addEditEmployeeListingViewModel.EditedClothesList
                    .FirstOrDefault(c => c.GuidID == editedDclivm.Clothes.GuidID)?
                    .Sizes.FirstOrDefault(cs => cs.GuidID == editedDclivm.ClothesSizeGuidID);

            int index = _addEditEmployeeListingViewModel.EditedClothesList.FindIndex(y => y.GuidID == existingClothesItem?.GuidID);

            if (index != -1)
                _addEditEmployeeListingViewModel.EditedClothesList[index] = editedDclivm.Clothes;
            else
                _addEditEmployeeListingViewModel.EditedClothesList.Add(editedDclivm.Clothes);
        }
    }
}
