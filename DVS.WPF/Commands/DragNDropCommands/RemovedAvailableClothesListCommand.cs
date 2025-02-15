using DVS.Domain.Models;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.DragNDropCommands
{
    public class RemovedAvailableClothesListCommand(AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
                                                    Action<AvailableClothesSizeItem> addItemToEditedClothesSizesList,
                                                    Action<AvailableClothesSizeItem> removeItemFromEditedClothesSizesList,
                                                    Action<Clothes> addItemToEditedClothesList,
                                                    Action<Clothes> removeItemFromEditedClothesList)
                                                    : CommandBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public readonly Action<AvailableClothesSizeItem> _addItemToEditedClothesSizesList = addItemToEditedClothesSizesList;
        public readonly Action<AvailableClothesSizeItem> _removeItemFromEditedClothesSizesList = removeItemFromEditedClothesSizesList;
        public readonly Action<Clothes> _addItemToEditedClothesList = addItemToEditedClothesList;
        public readonly Action<Clothes> _removeItemFromEditedClothesList = removeItemFromEditedClothesList;

        public override void Execute(object parameter)
        {
            CheckQuantity();

            if (_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity > 0)
            {
                _addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity -= 1;

                UpdateEditedList();
            }
            else
                ShowErrorMessageBox("Diese Bekleidung ist zur Zeit nicht vorrätig!", "Bekleidung nicht vorhanden");
        }

        private void CheckQuantity()
        {
            switch (_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.Quantity)
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

        private void UpdateEditedList()
        {
            AvailableClothesSizeItem? existingAcsi = _addEditEmployeeListingViewModel.GetClothesSizeFrom_editedClothesSizesList();
            if (existingAcsi == null)
                addItemToEditedClothesSizesList.Invoke(_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem);

            Clothes? existingClothes = _addEditEmployeeListingViewModel.GetClothesFrom_editedClothesList();
            if (existingClothes == null)
                addItemToEditedClothesList.Invoke(_addEditEmployeeListingViewModel.SelectedAvailableClothesSizeItem.ClothesSize.Clothes);
        }
    }
}
