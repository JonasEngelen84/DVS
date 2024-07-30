using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.AddEditCategoryCommands
{
    public class ClearCategoryListCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel = addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;

            addEditCategoryFormViewModel.ErrorMessage = null;
            addEditCategoryFormViewModel.IsSubmitting = true;

            string messageBoxText = "Alle Kategorien und ihre Schnittstellen werden gelöscht.\nLöschen fortsetzen?";
            string caption = "Alle Kategorien löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    await _categoryStore.ClearCategories(addEditCategoryFormViewModel);
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
}
