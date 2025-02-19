using DVS.Domain.Models;
using DVS.WPF.Stores;

namespace DVS.WPF.Commands
{
    public class PlusCommand(
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
            if (selectedClothesSizeStore.SelectedClothesSize != null)
                EditClothesSize();
            else if (selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
                EditEmployeeClothesSize();
        }

        private async void EditClothesSize()
        {
            ClothesSize selectedClothesSize = selectedClothesSizeStore.SelectedClothesSize;
            ClothesSize editedClothesSize = CreatePlusEditedClothesSize(selectedClothesSize);
            await UpdateClothesSize(editedClothesSize);
            selectedClothesSizeStore.SelectedClothesSize = editedClothesSize;
        }

        private async void EditEmployeeClothesSize()
        {
            ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes
                .First(cs => cs.GuidId == selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.ClothesSizeGuidId);

            if (existingClothesSize!= null && existingClothesSize.Quantity > 0)
            {
                ClothesSize editedClothesSize = CreateMinusEditedClothesSize(existingClothesSize);
                await UpdateClothesSize(editedClothesSize);

                EmployeeClothesSize editedEcs = CreateEditedEmployeeClothesSize(
                    selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize, editedClothesSize);
                await UpdateEmployeeClothesSize(editedEcs);

                selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize = editedEcs;
            }
            else
                ShowErrorMessageBox("Erhöhen der Stückzahl ist fehlgeschlagen!" +
                    "\nGewählte Bekleidung ist nicht vorrätig.", "Bekleidung nicht vorhanden");
        }

        private static ClothesSize CreatePlusEditedClothesSize(ClothesSize selectedClothesSize)
        {
            int quantity = selectedClothesSize.Quantity + 1;

            ClothesSize editedClothesSize = new(
                selectedClothesSize.GuidId,
                selectedClothesSize.Clothes,
                selectedClothesSize.Size,
                quantity,
                selectedClothesSize.Comment)
            {
                EmployeeClothesSizes = selectedClothesSize.EmployeeClothesSizes
            };

            editedClothesSize.Clothes.Sizes.Remove(selectedClothesSize);
            editedClothesSize.Clothes.Sizes.Add(editedClothesSize);

            return editedClothesSize;
        }

        private static ClothesSize CreateMinusEditedClothesSize(ClothesSize selectedClothesSize)
        {
            int quantity = selectedClothesSize.Quantity - 1;

            ClothesSize editedClothesSize = new(
                selectedClothesSize.GuidId,
                selectedClothesSize.Clothes,
                selectedClothesSize.Size,
                quantity,
                selectedClothesSize.Comment)
            {
                EmployeeClothesSizes = selectedClothesSize.EmployeeClothesSizes
            };

            editedClothesSize.Clothes.Sizes.Remove(selectedClothesSize);
            editedClothesSize.Clothes.Sizes.Add(editedClothesSize);

            return editedClothesSize;
        }

        private static EmployeeClothesSize CreateEditedEmployeeClothesSize(
            EmployeeClothesSize selectedEcs, ClothesSize editedClothesSize)
        {
            Employee editedEmployee = new(
                    selectedEcs.EmployeeId,
                    selectedEcs.Employee.Lastname,
                    selectedEcs.Employee.Firstname,
                    selectedEcs.Employee.Comment)
            {
                Clothes = selectedEcs.Employee.Clothes
            };

            int quantity = selectedEcs.Quantity + 1;

            EmployeeClothesSize editedEcs = new(
                selectedEcs.GuidId,
                editedEmployee,
                editedClothesSize,
                quantity,
                selectedEcs.Comment);

            editedEmployee.Clothes.Remove(selectedEcs);
            editedEmployee.Clothes.Add(editedEcs);

            return editedEcs;
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

        private async Task UpdateEmployeeClothesSize(EmployeeClothesSize editedEcs)
        {
            try
            {
                await employeeClothesSizesStore.Update(editedEcs);
            }
            catch
            {
                ShowErrorMessageBox("Erhöhen der Stückzahl ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Stückzahl erhöhen");
            }
        }
    }
}
