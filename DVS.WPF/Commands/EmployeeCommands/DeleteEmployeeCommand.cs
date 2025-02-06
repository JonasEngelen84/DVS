using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class DeleteEmployeeCommand(EmployeeListingItemViewModel employeeListingItemViewModel,
        EmployeeStore employeeStore) : AsyncCommandBase
    {
        private readonly EmployeeListingItemViewModel _employeeListingItemViewModel = employeeListingItemViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = $"Der Mitarbeiter  {_employeeListingItemViewModel.Lastname}, {_employeeListingItemViewModel.Firstname}  " +
                $"wird gelöscht!\n\nLöschen fortsetzen?";
            string caption = "Mitarbeiter löschen";
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

                try
                {
                    await _employeeStore.Delete(employee);
                }
                catch (Exception)
                {
                    messageBoxText = "Löschen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = " Mitarbeiter löschen";
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
