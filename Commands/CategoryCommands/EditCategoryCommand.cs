using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.CategoryCommands
{
    public class EditCategoryCommand : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly CategoryStore _categoryStore;

        public EditCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel,
            SelectedCategoryStore selectedCategoryStore,
            CategoryStore categoryStore)
        {
            _selectedCategoryStore = selectedCategoryStore;
            _addEditCategoryViewModel = addEditCategoryViewModel;
            _categoryStore = categoryStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;

            addEditCategoryFormViewModel.ErrorMessage = null;
            addEditCategoryFormViewModel.IsSubmitting = true;

            CategoryModel oldCategory = _selectedCategoryStore.SelectedCategory;
            string editedCategory = _selectedCategoryStore.EditedCategory;

            try
            {
                await _categoryStore.Update(oldCategory, editedCategory);
            }
            catch (Exception)
            {
                addEditCategoryFormViewModel.ErrorMessage = "Umbenennen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }
    }
}
