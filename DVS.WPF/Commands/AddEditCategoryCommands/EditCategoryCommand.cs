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
            addEditCategoryFormViewModel.HasError = false;
            addEditCategoryFormViewModel.IsSubmitting = true;

            string messageBoxText = $"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditCategoryFormViewModel.EditSelectedCategory}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Kategorie bearbeiten";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                Category updatedCategory = new(addEditCategoryFormViewModel.SelectedCategory.GuidID, addEditCategoryFormViewModel.EditSelectedCategory);

                try
                {
                    await _categoryStore.Update(updatedCategory, addEditCategoryFormViewModel);
                }
                catch (Exception)
                {
                    messageBoxText = "Bearbeiten der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = "Kategorie bearbeiten";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

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
