using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.CategoryCommands
{
    public class ClearCategoryListCommand : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore;

        public ClearCategoryListCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore)
        {
            _addEditCategoryViewModel = addEditCategoryViewModel;
            _categoryStore = categoryStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;

            addEditCategoryFormViewModel.ErrorMessage = null;
            addEditCategoryFormViewModel.IsSubmitting = true;

            try
            {
                string messageBoxText = "Alle Kategorien und ihre Schnittstellen werden gelöscht.\nLöschen fortsetzen?";
                string caption = "Alle Kategorien löschen";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                if (dialog == MessageBoxResult.Yes)
                {
                    await _categoryStore.ClearCategories();
                }
            }
            catch (Exception)
            {
                addEditCategoryFormViewModel.ErrorMessage = "Löschen aller Kategorien ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }
    }
}
