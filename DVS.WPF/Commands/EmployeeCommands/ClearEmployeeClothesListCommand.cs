using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class ClearEmployeeClothesListCommand(
        EmployeeListingItemViewModel employeeListingItemViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            employeeListingItemViewModel.HasError = false;
            employeeListingItemViewModel.IsDeleting = true;

            Employee employee = employeeListingItemViewModel.Employee;

            if (Confirm("Alle Bekleidungen des Mitarbeiters" +
                $"  {employeeListingItemViewModel.Lastname}, {employeeListingItemViewModel.Firstname}  werden entfernt!" +
                $"\n\nSollen die Bekleidungen dem Lager zugefügt werden?",
                "Alle Bekleidungen entfernen"))
            {
                foreach(EmployeeClothesSize ecs in employee.Clothes)
                {

                }
            }
            else
            {                
                employee.Clothes.Clear();
                await UpdateEmployee(employee);
            }

            employeeListingItemViewModel.IsDeleting = false;
        }

        private async Task UpdateEmployee(Employee editedEmployee)
        {
            try
            {
                await employeeStore.Update(editedEmployee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Entfernen aller Bekleidungen ist fehlgeschlagen!\nBitte versuchen Sie es erneut.",
                    "Alle Bekleidungen löschen");

                employeeListingItemViewModel.HasError = true;
            }
        }
    }
}
