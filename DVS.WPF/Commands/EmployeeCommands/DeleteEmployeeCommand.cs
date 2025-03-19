using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class DeleteEmployeeCommand(
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

            if (!Confirm($"Soll der/die Mitarbeiter/in  {employeeListingItemViewModel.Lastname}, {employeeListingItemViewModel.Firstname}  " +
                    "wirklich gelöscht werden?", "Mitarbeiter/in löschen"))
            {
                return;
            }

            employeeListingItemViewModel.IsDeleting = true;

            Employee employee = employeeListingItemViewModel.Employee;

            if (employee.Clothes.Count < 1)
                await DeleteEmployee(employee);
            else
            {
                if (Confirm("Bekleidungen dem Lager zufügen?", "Mitarbeiter/in löschen"))
                {
                    foreach (EmployeeClothesSize ecs in employee.Clothes)
                    {
                        ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes.First(cs => cs.Id == ecs.ClothesSizeGuidId);
                        ClothesSize editedClothesSize = CreateEditedClothesSize(existingClothesSize, ecs.Quantity);
                        clothesSizeStore.Update(editedClothesSize);
                        await DeleteEmployeeClothesSizes(ecs);
                        UpdateClothes(editedClothesSize);
                    }

                    await DeleteEmployee(employee);
                }
                else
                {
                    foreach (EmployeeClothesSize ecs in employee.Clothes)
                    {
                        await DeleteEmployeeClothesSizes(ecs);
                    }

                    await DeleteEmployee(employee);
                }
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

        private async Task DeleteEmployee(Employee employee)
        {
            try
            {
                employee.Clothes.Clear();
                await employeeStore.Delete(employee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Entfernen der Bekleidungen ist fehlgeschlagen!", "Bekleidungen entfernen");
            }
        }
    }
}
