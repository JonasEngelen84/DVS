using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
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
                _employeeListingItemViewModel.ErrorMessage = null;
                _employeeListingItemViewModel.IsDeleting = true;

                EmployeeModel employee = _employeeListingItemViewModel.Employee;

                try
                {
                    await _employeeStore.Delete(employee.GuidID);
                }
                catch (Exception)
                {
                    _employeeListingItemViewModel.ErrorMessage = "Löschen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    _employeeListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
