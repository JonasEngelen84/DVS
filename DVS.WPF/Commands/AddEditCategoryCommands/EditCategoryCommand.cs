using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class EditCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel = addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;

            if (ConfirmEditCategory(addEditCategoryFormViewModel))
            {
                addEditCategoryFormViewModel.HasError = false;
                addEditCategoryFormViewModel.IsSubmitting = true;

                Category updatedCategory = new(addEditCategoryFormViewModel.SelectedCategory.GuidID, addEditCategoryFormViewModel.EditSelectedCategory);

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

        private bool ConfirmEditCategory(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            string messageBoxText = $"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditCategoryFormViewModel.EditSelectedCategory}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Kategorie umbenennen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);
            return dialog == MessageBoxResult.Yes;
        }

        private void ShowErrorMessageBox(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(message, title, button, icon);
        }
    }
}
