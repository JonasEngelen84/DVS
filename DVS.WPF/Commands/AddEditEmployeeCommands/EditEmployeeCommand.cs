using DVS.Domain.Models;
using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.AddEditEmployeeCommands
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

                EmployeeModel employeeToEdit = new(_guidID,
                                                   editEmployeeFormViewModel.ID,
                                                   editEmployeeFormViewModel.Lastname,
                                                   editEmployeeFormViewModel.Firstname,
                                                   editEmployeeFormViewModel.Comment);
                employeeToEdit.Clothes.Clear();

                //TODO: Kommentare von DetailedItems werden entfernt bei einem update
                foreach (DetailedClothesListingItemViewModel item in
                _editEmployeeViewModel.AddEditEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection)
                {
                    ClothesModel existingClothes = employeeToEdit.Clothes.FirstOrDefault(clothes => clothes.GuidID == item.Clothes.GuidID);

                    if (existingClothes != null)
                    {
                        existingClothes.Sizes.Add(new ClothesSizeModel(item.Size) { Quantity = item.Quantity, IsSelected = true });
                    }
                    else
                    {
                        ClothesModel newClothes = new(item.Clothes.GuidID, item.ID, item.Name, item.Clothes.Category, item.Clothes.Season, null);
                        newClothes.Sizes.Add(new ClothesSizeModel(item.Size) { Quantity = item.Quantity, IsSelected = true });
                        employeeToEdit.Clothes.Add(newClothes);
                    }
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
