using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.AddEditCategoryCommands
{
    public class EditCategoryCommand : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly CategoryStore _categoryStore;

        public EditCategoryCommand(
            AddEditCategoryViewModel addEditCategoryViewModel,
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

            string messageBoxText = $"Die Kategorie \"{_selectedCategoryStore.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{_selectedCategoryStore.EditedCategory}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Kategorie umbenennen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    await _categoryStore.Edit(oldCategory, editedCategory);
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
}
