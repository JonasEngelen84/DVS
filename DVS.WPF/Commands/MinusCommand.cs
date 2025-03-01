using DVS.Domain.Models;
using DVS.WPF.Stores;

namespace DVS.WPF.Commands
{
    public class MinusCommand(
        SelectedClothesSizeStore selectedClothesSizeStore,
        SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (selectedClothesSizeStore.SelectedClothesSize != null) EditClothesSize();
            else if (selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
            {
                Clothes? existingClothes = clothesStore.Clothes
                    .FirstOrDefault(c => c.Id == selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.ClothesSize.ClothesId);

                if (existingClothes == null)
                {
                    if (Confirm($"Diese Bekleidung ist nicht Im Bestand.\nSoll ein neues Objekt dieser Bekleidung angelegt werden?" +
                                $"\nAndernfalls wird die zu entfernende Bekleidung gelöscht!", "Bekleidung nicht im Vorrat"))
                        EditEmployeeClothesSize();
                }
                else EditEmployeeClothesSize();
            }
        }

        private async void EditClothesSize()
        {
            ClothesSize selectedClothesSize = selectedClothesSizeStore.SelectedClothesSize;
            ClothesSize editedClothesSize = CreateMinusEditedClothesSize(selectedClothesSize);
            await UpdateClothesSize(editedClothesSize);
            selectedClothesSizeStore.SelectedClothesSize = editedClothesSize;
        }

        private async void EditEmployeeClothesSize()
        {
            EmployeeClothesSize selectedEcs = selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize;
            ClothesSize editedClothesSize = CreatePlusEditedClothesSize(selectedEcs.ClothesSize);
            await UpdateClothesSize(editedClothesSize);

            EmployeeClothesSize editedEcs = CreateEditedEmployeeClothesSize(selectedEcs, editedClothesSize);
            employeeClothesSizesStore.Update(editedEcs);

            selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize = editedEcs;
        }

        private static ClothesSize CreateMinusEditedClothesSize(ClothesSize selectedClothesSize)
        {
            int quantity = selectedClothesSize.Quantity - 1;

            return new ClothesSize(
                selectedClothesSize.GuidId,
                selectedClothesSize.Clothes,
                selectedClothesSize.Size,
                quantity,
                selectedClothesSize.Comment)
            {
                EmployeeClothesSizes = []
            };
        }

        private static ClothesSize CreatePlusEditedClothesSize(ClothesSize selectedClothesSize)
        {
            int quantity = selectedClothesSize.Quantity + 1;

            return new ClothesSize(
                selectedClothesSize.GuidId,
                selectedClothesSize.Clothes,
                selectedClothesSize.Size,
                quantity,
                selectedClothesSize.Comment)
            {
                EmployeeClothesSizes = []
            };
        }

        private static EmployeeClothesSize CreateEditedEmployeeClothesSize(
            EmployeeClothesSize selectedEcs, ClothesSize editedClothesSize)
        {
            int quantity = selectedEcs.Quantity - 1;

            return new EmployeeClothesSize(
                selectedEcs.GuidId,
                selectedEcs.Employee,
                editedClothesSize,
                quantity,
                selectedEcs.Comment);
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
