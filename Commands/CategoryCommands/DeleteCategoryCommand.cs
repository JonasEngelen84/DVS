using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly CategoryStore _categoryStore;

        public DeleteCategoryCommand(
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

            CategoryModel deletedCategorie = _selectedCategoryStore.SelectedCategory;

            try
            {
                string messageBoxText = $"Die Kategorie \"{_selectedCategoryStore.SelectedCategory.Name}\" und ihre Schnittstellen werden gelöscht.\n\nLöschen fortsetzen?";
                string caption = "Kategorie löschen";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                if (dialog == MessageBoxResult.Yes)
                {
                    await _categoryStore.Delete(deletedCategorie);
                }
            }
            catch (Exception)
            {
                addEditCategoryFormViewModel.ErrorMessage = "Löschen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }
    }
}
