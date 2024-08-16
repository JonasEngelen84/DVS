using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class AddCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, CategoryStore categoryStore) : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel = addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;
            addEditCategoryFormViewModel.HasError = false;
            addEditCategoryFormViewModel.IsSubmitting = true;

            Category newCategory = new(Guid.NewGuid(), addEditCategoryFormViewModel.AddNewCategory);

            try
            {
                await _categoryStore.Add(newCategory, addEditCategoryFormViewModel);
            }
            catch (Exception)
            {
                string messageBoxText = "Erstellen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                string caption = "Kategorie erstellen";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                addEditCategoryFormViewModel.HasError = true;
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }
    }
}
