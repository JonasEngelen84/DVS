using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class ClearSizesCommand(
        ClothesListingItemViewModel clothesListingItemViewModel,
        ClothesStore clothesStore,
        EmployeeStore employeeStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (clothesListingItemViewModel.Sizes.Count == 0)
            {
                ShowErrorMessageBox($"Die Bekleidung  {clothesListingItemViewModel.Id}, {clothesListingItemViewModel.Name}  " +
                    $"enthält bereits keine Größen!", "Bekleidungs-Größen löschen");

                return;
            }

            if (!Confirm($"Alle Größen der Bekleidung  \"{clothesListingItemViewModel.Id}, {clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                "\n\nLöschen fortsetzen?", "Alle Bekleidungs-Größen löschen"))
            {
                return;
            }

            clothesListingItemViewModel.IsDeleting = true;
            clothesListingItemViewModel.HasError = false;

            HashSet<EmployeeClothesSize> editedEmployeeClothesSizes = [];

            await CompareCothesSizes();
            UpdateEmployeeClothesSizes(editedEmployeeClothesSizes);
            UpdateEmployee(editedEmployeeClothesSizes);
            clothesStore.Update(clothesListingItemViewModel.Clothes);

            clothesListingItemViewModel.IsDeleting = false;
        }

        private async Task CompareCothesSizes()
        {
            HashSet<ClothesSize> Sizes = new(clothesListingItemViewModel.Clothes.Sizes);

            foreach (ClothesSize clothesSize in Sizes)
            {
                EmployeeClothesSize? assignedClothesSize = employeeClothesSizeStore.EmployeeClothesSizes
                    .FirstOrDefault(ecs => ecs.ClothesSizeGuidId == clothesSize.Id);

                if (assignedClothesSize != null)
                {
                    if (clothesSize.Quantity > 0)
                    {
                        ShowErrorMessageBox($"Größe  \"{clothesSize.Size}\"  kann nicht enfernt werden, " +
                            "da diese noch vergeben ist!\nDer Bestand wird auf \"0\" gesetzt.", " Bekleidung bearbeiten");

                        UpdateClothesSize(clothesSize);
                    }
                }
                else
                {
                    await DeleteClothesSize(clothesSize);
                }
            }
        }

        private void UpdateClothesSize(ClothesSize clothesSizeToEdit)
        {
            clothesSizeToEdit.Quantity = 0;

            ClothesSize existingClothesSize = clothesListingItemViewModel.Clothes.Sizes
                    .First(cs => cs.Id == clothesSizeToEdit.Id);

            clothesListingItemViewModel.Clothes.Sizes.Remove(existingClothesSize);
            clothesListingItemViewModel.Clothes.Sizes.Add(clothesSizeToEdit);

            clothesSizeStore.Update(clothesSizeToEdit);
        }

        private async Task DeleteClothesSize(ClothesSize clothesSizeToDelete)
        {
            clothesListingItemViewModel.Clothes.Sizes.Remove(clothesSizeToDelete);

            try
            {
                await clothesSizeStore.Delete(clothesSizeToDelete);
            }
            catch
            {
                ShowErrorMessageBox($"Löschen der Größe  {clothesSizeToDelete.Size}  ist fehlgeschlagen!", "Bekleidungs-Größen löschen");
            }
        }

        private void UpdateEmployeeClothesSizes(HashSet<EmployeeClothesSize> editedEmployeeClothesSizes)
        {
            foreach (ClothesSize clothesSize in clothesListingItemViewModel.Clothes.Sizes)
            {
                List<EmployeeClothesSize> assignedClothesSizes = employeeClothesSizeStore.EmployeeClothesSizes
                    .Where(ecs => ecs.ClothesSizeGuidId == clothesSize.Id)
                    .ToList();

                foreach (EmployeeClothesSize ecs in assignedClothesSizes)
                {
                    ecs.ClothesSize = clothesSize;
                    editedEmployeeClothesSizes.Add(ecs);
                    employeeClothesSizeStore.Update(ecs);
                }
            }
        }
        
        private void UpdateEmployee(HashSet<EmployeeClothesSize> editedEmployeeClothesSizes)
        {
            foreach (EmployeeClothesSize employeeClothesSize in editedEmployeeClothesSizes)
            {
                EmployeeClothesSize existingEcs = employeeClothesSize.Employee.Clothes
                    .First(ecs => ecs.Id == employeeClothesSize.Id);

                employeeClothesSize.Employee.Clothes.Remove(existingEcs);
                employeeClothesSize.Employee.Clothes.Add(employeeClothesSize);
                employeeStore.Update(employeeClothesSize.Employee);
            }
        }
    }
}
