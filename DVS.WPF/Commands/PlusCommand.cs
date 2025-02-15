using DVS.Domain.Models;
using DVS.WPF.Commands.ClothesSizeCommands;
using DVS.WPF.Stores;

namespace DVS.WPF.Commands
{
    public class PlusCommand(SelectedClothesSizeStore selectedClothesSizeStore,
                             SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
                             EmployeeStore employeeStore,
                             ClothesStore clothesStore,
                             SizeStore sizeStore,
                             CategoryStore categoryStore,
                             SeasonStore seasonStore,
                             ClothesSizeStore clothesSizeStore,
                             EmployeeClothesSizeStore employeeClothesSizesStore,
                             ModalNavigationStore modalNavigationStore)
                             : AsyncCommandBase
    {
        private readonly SelectedClothesSizeStore _selectedClothesSizeStore = selectedClothesSizeStore;
        private readonly SelectedEmployeeClothesSizeStore _selectedEmployeeClothesSizeStore = selectedEmployeeClothesSizeStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            if (_selectedClothesSizeStore.SelectedClothesSize != null)
                EditClothesSize();
            else if (_selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
                EditEmployeeClothesSize();
        }

        private async void EditClothesSize()
        {
            ClothesSize selectedClothesSize = _selectedClothesSizeStore.SelectedClothesSize;
            await UpdateClothesSize(CreateEditedClothesSize(selectedClothesSize));
        }

        private void EditEmployeeClothesSize()
        {

        }

        private static ClothesSize CreateEditedClothesSize(ClothesSize selectedClothesSize)
        {
            int quantity = selectedClothesSize.Quantity + 1;

            return new ClothesSize(
                selectedClothesSize.GuidId,
                selectedClothesSize.Clothes,
                selectedClothesSize.Size,
                quantity,
                selectedClothesSize.Comment);
        }

        private async Task UpdateClothesSize(ClothesSize editedClothesSize)
        {
            try
            {
                await _clothesSizeStore.Update(editedClothesSize);
            }
            catch
            {
                ShowErrorMessageBox("Erhöhen der Stückzahl ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Stückzahl erhöhen");
            }
        }
    }
}
