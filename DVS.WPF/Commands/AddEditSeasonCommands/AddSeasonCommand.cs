using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class AddSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel, SeasonStore seasonStore) : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel = addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;
            addEditSeasonFormViewModel.HasError = false;
            addEditSeasonFormViewModel.IsSubmitting = true;

            Season newSeason = new(Guid.NewGuid(), addEditSeasonFormViewModel.AddNewSeason);

            try
            {
                await _seasonStore.Add(newSeason, addEditSeasonFormViewModel);
            }
            catch (Exception)
            {
                string messageBoxText = "Erstellen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                string caption = "Saison erstellen";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                addEditSeasonFormViewModel.HasError = true;
            }
            finally
            {
                addEditSeasonFormViewModel.IsSubmitting = false;
            }
        }
    }
}
