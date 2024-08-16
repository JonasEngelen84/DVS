using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListViewItems;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class EditEmployeeCommand(EditEmployeeViewModel updateEmployeeViewModel, EmployeeStore employeeStore,
        ModalNavigationStore modalNavigationStore, Guid guiID) : AsyncCommandBase
    {
        private readonly EditEmployeeViewModel _updateEmployeeViewModel = updateEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly Guid _guidID = guiID;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = "Mitarbeiter bearbeiten?";
            string caption = "Mitarbeiter bearbeiten";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                AddEditEmployeeFormViewModel updateEmployeeFormViewModel = _updateEmployeeViewModel.AddEditEmployeeFormViewModel;

                updateEmployeeFormViewModel.HasError = false;
                updateEmployeeFormViewModel.IsSubmitting = true;

                Employee updatedEmployee = new(_guidID,
                                              updateEmployeeFormViewModel.ID,
                                              updateEmployeeFormViewModel.Lastname,
                                              updateEmployeeFormViewModel.Firstname,
                                              updateEmployeeFormViewModel.Comment);

                foreach (EmployeeClothesSize size in updatedEmployee.Clothes)
                {
                    size.ClothesSize.EmployeeClothesSizes.Remove(size);
                }

                updatedEmployee.Clothes.Clear();

                //TODO: Kommentare von DetailedItems werden entfernt bei update
                foreach (DetailedClothesListingItemViewModel item in updateEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection)
                {
                    var existingClothes = item.Clothes.Sizes.FirstOrDefault(s => s.Size.Equals(item.Clothes.Sizes));
                    existingClothes.EmployeeClothesSizes.Add(new EmployeeClothesSize(Guid.NewGuid(), updatedEmployee, existingClothes, (int)item.Quantity, item.Comment));
                    updatedEmployee.Clothes.Add(new EmployeeClothesSize(Guid.NewGuid(), updatedEmployee, existingClothes, (int)item.Quantity, item.Comment));
                }

                try
                {
                    await _employeeStore.Update(updatedEmployee);
                }
                catch (Exception)
                {
                    messageBoxText = "Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = " Mitarbeiter bearbeiten";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    updateEmployeeFormViewModel.HasError = true;
                }
                finally
                {
                    updateEmployeeFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }

    }
}
