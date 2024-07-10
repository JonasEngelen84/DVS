using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.AddEditSeasonCommands
{
    public class EditSeasonCommand : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel;
        private readonly SelectedSeasonStore _selectedSeasonStore;
        private readonly SeasonStore _seasonStore;

        public EditSeasonCommand(
            AddEditSeasonViewModel addEditSeasonViewModel,
            SelectedSeasonStore selectedSeasonStore,
            SeasonStore seasonStore)
        {
            _addEditSeasonViewModel = addEditSeasonViewModel;
            _selectedSeasonStore = selectedSeasonStore;
            _seasonStore = seasonStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;

            addEditSeasonFormViewModel.ErrorMessage = null;
            addEditSeasonFormViewModel.IsSubmitting = true;

            SeasonModel oldSeason = _selectedSeasonStore.SelectedSeason;
            string editedSeason = _selectedSeasonStore.EditedSeason;

            string messageBoxText = $"Die Saison \"{_selectedSeasonStore.SelectedSeason.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{_selectedSeasonStore.EditedSeason}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Saison umbenennen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    await _seasonStore.Edit(oldSeason, editedSeason);
                }
                catch (Exception)
                {
                    addEditSeasonFormViewModel.ErrorMessage = "Umbenennen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    addEditSeasonFormViewModel.IsSubmitting = false;
                }
            }
        }
    }
}
