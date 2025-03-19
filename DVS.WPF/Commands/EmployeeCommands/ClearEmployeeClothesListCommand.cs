using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class ClearEmployeeClothesListCommand(
        EmployeeListingItemViewModel employeeListingItemViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            employeeListingItemViewModel.HasError = false;

            Employee employee = employeeListingItemViewModel.Employee;

            if (employee.Clothes.Count == 0)
            {
                ShowErrorMessageBox("Keine Bekleidung zum entfernen vorhanden!", "Bekleidungen entfernen");
                return;
            }

            if (!Confirm($"Sollen alle Bekleidungen des/der Mitarbeiters/in  {employeeListingItemViewModel.Lastname}, {employeeListingItemViewModel.Firstname}  " +
                    "wirklich entfernt werden?", "Alle Bekleidungen löschen"))
            {
                return;
            }

            employeeListingItemViewModel.IsDeleting = true;

            if (Confirm("Bekleidungen dem Lager zufügen?", "Alle Bekleidungen löschen"))
            {
                foreach (EmployeeClothesSize ecs in employee.Clothes)
                {
                    ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes.First(cs => cs.Id == ecs.ClothesSizeGuidId);
                    ClothesSize editedClothesSize = CreateEditedClothesSize(existingClothesSize, ecs.Quantity);
                    clothesSizeStore.Update(editedClothesSize);
                    await DeleteEmployeeClothesSizes(ecs);
                    UpdateClothes(editedClothesSize);
                }

                UpdateEmployee(employee);
            }
            else
            {
                foreach (EmployeeClothesSize ecs in employee.Clothes)
                {
                    await DeleteEmployeeClothesSizes(ecs);
                }

                UpdateEmployee(employee);
            }

            employeeListingItemViewModel.IsDeleting = false;
        }

        private static ClothesSize CreateEditedClothesSize(ClothesSize existingClothesSize, int ecsQuantity)
        {
            int newQuantity = existingClothesSize.Quantity + ecsQuantity;

            ClothesSize editedClothesSize = new(existingClothesSize.Id,
                                                existingClothesSize.Clothes,
                                                existingClothesSize.Size,
                                                newQuantity,
                                                existingClothesSize.Comment)
            {
                EmployeeClothesSizes = []
            };

            return editedClothesSize;
        }

        private void UpdateClothes(ClothesSize editedClothesSize)
        {
            Clothes editedClothes = new(
                    editedClothesSize.Clothes.Id,
                    editedClothesSize.Clothes.Name,
                    editedClothesSize.Clothes.Category,
                    editedClothesSize.Clothes.Season,
                    editedClothesSize.Clothes.Comment)
            {
                Sizes = editedClothesSize.Clothes.Sizes
            };

            ClothesSize existingClothesSize = editedClothes.Sizes.First(cs => cs.Id == editedClothesSize.Id);

            editedClothes.Sizes.Remove(existingClothesSize);
            editedClothes.Sizes.Add(editedClothesSize);

            clothesStore.Update(editedClothes);
        }

        private async Task DeleteEmployeeClothesSizes(EmployeeClothesSize ecsToDelete)
        {
            try
            {
                await employeeClothesSizeStore.Delete(ecsToDelete);
            }
            catch
            {
                ShowErrorMessageBox("Entfernen der Bekleidungen ist fehlgeschlagen!", "Bekleidungen entfernen");
            }
        }

        private void UpdateEmployee(Employee employee)
        {
            employee.Clothes.Clear();

            employeeStore.Update(employee);
        }
    }
}
