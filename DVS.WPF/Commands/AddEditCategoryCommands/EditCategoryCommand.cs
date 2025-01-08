using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class EditCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel = addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;

            if (Confirm($"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditCategoryFormViewModel.EditSelectedCategory}\" umbenannt.\n\nUmbennen fortsetzen?", "Kategorie umbenennen"))
            {
                addEditCategoryFormViewModel.HasError = false;
                addEditCategoryFormViewModel.IsSubmitting = true;

                Category updatedCategory = new(addEditCategoryFormViewModel.SelectedCategory.GuidId, addEditCategoryFormViewModel.EditSelectedCategory);

                try
                {
                    await _categoryStore.Update(updatedCategory, addEditCategoryFormViewModel);
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
