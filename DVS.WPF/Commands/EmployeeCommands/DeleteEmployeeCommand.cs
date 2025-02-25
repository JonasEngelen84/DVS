using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;
using System.Windows;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class DeleteEmployeeCommand(
        EmployeeListingItemViewModel employeeListingItemViewModel,
        EmployeeStore employeeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = $"Der Mitarbeiter  {employeeListingItemViewModel.Lastname}, {employeeListingItemViewModel.Firstname}  " +
                $"wird gelöscht!\n\nLöschen fortsetzen?";
            string caption = "Mitarbeiter löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                employeeListingItemViewModel.HasError = false;
                employeeListingItemViewModel.IsDeleting = true;

                Employee employee = employeeListingItemViewModel.Employee;

                foreach (EmployeeClothesSize size in employee.Clothes)
                {
                    size.ClothesSize.EmployeeClothesSizes.Remove(size);
                }

                try
                {
                    await employeeStore.Delete(employee);
                }
                catch (Exception)
                {
                    messageBoxText = "Löschen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = " Mitarbeiter löschen";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    employeeListingItemViewModel.HasError = true;
                }
                finally
                {
                    employeeListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
