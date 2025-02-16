using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CategoryCommands
{
    public class AddCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = addEditCategoryViewModel.AddEditCategoryFormViewModel;
            addEditCategoryFormViewModel.HasError = false;
            addEditCategoryFormViewModel.IsSubmitting = true;

            Category newCategory = new(Guid.NewGuid(), addEditCategoryFormViewModel.AddNewCategory);

            try
            {
                await categoryStore.Add(newCategory, addEditCategoryFormViewModel);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Hinzufügen der Category in Datenbank ist fehlgeschlagen!", "AddCategoryCommand ExecuteAsync");
                addEditCategoryFormViewModel.HasError = true;
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }
    }
}
