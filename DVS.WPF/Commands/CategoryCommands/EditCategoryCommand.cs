using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CategoryCommands
{
    public class EditCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = addEditCategoryViewModel.AddEditCategoryFormViewModel;

            if (Confirm($"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditCategoryFormViewModel.EditSelectedCategory}\" umbenannt.\n\nUmbennen fortsetzen?", "Kategorie umbenennen"))
            {
                addEditCategoryFormViewModel.HasError = false;
                addEditCategoryFormViewModel.IsSubmitting = true;

                Category updatedCategory = new(addEditCategoryFormViewModel.SelectedCategory.GuidId, addEditCategoryFormViewModel.EditSelectedCategory);

                try
                {
                    await categoryStore.Update(updatedCategory, addEditCategoryFormViewModel);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Umbenennen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Kategorie umbenennen");

                    addEditCategoryFormViewModel.HasError = true;
                }
                finally
                {
                    addEditCategoryFormViewModel.IsSubmitting = false;
                }
            }
        }
    }
}
