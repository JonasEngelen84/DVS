using DVS.Domain.Models;
using DVS.WPF.Stores;

namespace DVS.WPF.Commands
{
    public class PlusCommand(
        SelectedClothesSizeStore selectedClothesSizeStore,
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
        public override async Task ExecuteAsync(object parameter)
        {
            if (selectedClothesSizeStore.SelectedClothesSize != null)
                EditClothesSize();
            else if (selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
                EditEmployeeClothesSize();
        }

        private async void EditClothesSize()
        {
            ClothesSize selectedClothesSize = selectedClothesSizeStore.SelectedClothesSize;
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
                await clothesSizeStore.Update(editedClothesSize);
            }
            catch
            {
                ShowErrorMessageBox("Erhöhen der Stückzahl ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Stückzahl erhöhen");
            }
        }
    }
}
