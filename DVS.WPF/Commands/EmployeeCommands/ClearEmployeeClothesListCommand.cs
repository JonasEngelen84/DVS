using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using System.Windows;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class ClearEmployeeClothesListCommand(
        EmployeeListingItemViewModel employeeListingItemViewModel,
        EmployeeStore employeeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = $"Die gesamte Kleidungsliste des Mitarbeiters  " +
                $"{employeeListingItemViewModel.Lastname}, {employeeListingItemViewModel.Firstname}  " +
                $"wird gelöscht!\n\nLöschen fortsetzen?";
            string caption = "Alle Bekleidungen löschen";
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

                employee.Clothes.Clear();

                try
                {
                    await employeeStore.Update(employee);
                }
                catch (Exception)
                {
                    messageBoxText = $"Löschen aller Bekleidungen ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = " Alle Bekleidungen löschen";
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
