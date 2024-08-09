using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class EditEmployeeCommand(EditEmployeeViewModel editEmployeeViewModel, EmployeeStore employeeStore,
        ModalNavigationStore modalNavigationStore, Guid guiID) : AsyncCommandBase
    {
        private readonly EditEmployeeViewModel _editEmployeeViewModel = editEmployeeViewModel;
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
                AddEditEmployeeFormViewModel editEmployeeFormViewModel = _editEmployeeViewModel.AddEditEmployeeFormViewModel;

                editEmployeeFormViewModel.ErrorMessage = null;
                editEmployeeFormViewModel.IsSubmitting = true;

                Employee employeeToEdit = new(_guidID,
                                              editEmployeeFormViewModel.ID,
                                              editEmployeeFormViewModel.Lastname,
                                              editEmployeeFormViewModel.Firstname,
                                              editEmployeeFormViewModel.Comment);

                foreach (EmployeeClothesSize size in employeeToEdit.EmployeeClothes)
                {
                    size.ClothesSize.EmployeeClothesSizes.Remove(size);
                }

                employeeToEdit.EmployeeClothes.Clear();

                //TODO: Kommentare von DetailedItems werden entfernt bei update
                foreach (DetailedClothesListingItemViewModel item in editEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection)
                {
                    var existingClothes = item.Clothes.Sizes.FirstOrDefault(s => s.Size.Equals(item.Clothes.Sizes));
                    existingClothes.EmployeeClothesSizes.Add(new EmployeeClothesSize(employeeToEdit, existingClothes, (int)item.Quantity));
                    employeeToEdit.EmployeeClothes.Add(new EmployeeClothesSize(employeeToEdit, existingClothes, (int)item.Quantity));
                }

                try
                {
                    await _employeeStore.Update(employeeToEdit);
                }
                catch (Exception)
                {
                    editEmployeeFormViewModel.ErrorMessage = "Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    editEmployeeFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }

    }
}
