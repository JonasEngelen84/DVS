using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class ClearEmployeeClothesListCommand(EmployeeListingItemViewModel employeeListingItemViewModel,
        EmployeeStore employeeStore) : AsyncCommandBase
    {
        private readonly EmployeeListingItemViewModel _employeeListingItemViewModel = employeeListingItemViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = $"Die gesamte Kleidungsliste des Mitarbeiters  " +
                $"{_employeeListingItemViewModel.Lastname}, {_employeeListingItemViewModel.Firstname}  " +
                $"wird gelöscht!\n\nLöschen fortsetzen?";
            string caption = "Alle Bekleidungen löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                _employeeListingItemViewModel.HasError = false;
                _employeeListingItemViewModel.IsDeleting = true;

                Employee employee = _employeeListingItemViewModel.Employee;

                foreach (EmployeeClothesSize size in employee.Clothes)
                {
                    size.ClothesSize.EmployeeClothesSizes.Remove(size);
                }

                employee.Clothes.Clear();

                try
                {
                    await _employeeStore.Update(employee);
                }
                catch (Exception)
                {
                    messageBoxText = $"Löschen aller Bekleidungen ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = " Alle Bekleidungen löschen";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    _employeeListingItemViewModel.HasError = true;
                }
                finally
                {
                    _employeeListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
