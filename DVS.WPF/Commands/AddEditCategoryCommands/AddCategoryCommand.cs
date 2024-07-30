using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class AddCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel = addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;
            addEditCategoryFormViewModel.ErrorMessage = null;
            addEditCategoryFormViewModel.IsSubmitting = true;

            CategoryModel newCategory = new(Guid.NewGuid(), addEditCategoryFormViewModel.AddNewCategory);

            try
            {
                await _categoryStore.Add(newCategory, addEditCategoryFormViewModel);
            }
            catch (Exception)
            {
                addEditCategoryFormViewModel.ErrorMessage = "Erstellen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }
    }
}
