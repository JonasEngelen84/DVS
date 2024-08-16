using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class DeleteCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel = addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;
            addEditCategoryFormViewModel.HasError = false;
            addEditCategoryFormViewModel.IsDeleting = true;

            string messageBoxText = $"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}" +
                $"\" und ihre Schnittstellen werden gelöscht.\n\nLöschen fortsetzen?";
            string caption = "Kategorie löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    await _categoryStore.Delete((Guid)addEditCategoryFormViewModel.SelectedCategory.GuidID, addEditCategoryFormViewModel);
                }
                catch (Exception)
                {
                    messageBoxText = "Löschen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = "Kategorie löschen";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    addEditCategoryFormViewModel.HasError = true;
                }
                finally
                {
                    addEditCategoryFormViewModel.IsDeleting = false;
                }
            }
        }
    }
}
