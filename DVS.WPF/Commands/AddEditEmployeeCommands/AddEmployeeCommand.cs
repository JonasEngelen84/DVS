using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows.Controls;
using System.Windows;
using System;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, EmployeeStore employeeStore,
        ModalNavigationStore modalNavigationStore) : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            addEmployeeFormViewModel.HasError = false;
            addEmployeeFormViewModel.IsSubmitting = true;

            Employee employee = new(Guid.NewGuid(),
                                    addEmployeeFormViewModel.ID,
                                    addEmployeeFormViewModel.Firstname,
                                    addEmployeeFormViewModel.Lastname,
                                    addEmployeeFormViewModel.Comment);

            foreach (DetailedClothesListingItemViewModel item in addEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection)
            {
                item.ClothesSize.EmployeeClothesSizes.Add(new EmployeeClothesSize(Guid.NewGuid(), employee, item.ClothesSize, (int)item.Quantity, null));
                employee.Clothes.Add(new EmployeeClothesSize(Guid.NewGuid(), employee, item.ClothesSize, (int)item.Quantity, null));
            }

            try
            {
                await _employeeStore.Add(employee);
            }
            catch (Exception)
            {
                string messageBoxText = "Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                string caption = " Bekleidung bearbeiten";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                addEmployeeFormViewModel.HasError = true;
            }
            finally
            {
                addEmployeeFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
