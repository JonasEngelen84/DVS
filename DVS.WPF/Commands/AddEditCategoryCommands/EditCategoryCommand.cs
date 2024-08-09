﻿using DVS.Domain.Models;
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

            string messageBoxText = $"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditCategoryFormViewModel.EditCategory}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Kategorie umbenennen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                addEditCategoryFormViewModel.ErrorMessage = null;
                addEditCategoryFormViewModel.IsSubmitting = true;

                Category category = new(addEditCategoryFormViewModel.SelectedCategory.GuidID, addEditCategoryFormViewModel.EditSelectedCategory);

                try
                {
                    await _categoryStore.Update(category, addEditCategoryFormViewModel);
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
