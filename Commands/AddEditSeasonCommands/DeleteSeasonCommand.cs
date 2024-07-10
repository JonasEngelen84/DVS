using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.AddEditSeasonCommands
{
    public class DeleteSeasonCommand : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel;
        private readonly SelectedSeasonStore _selectedSeasonStore;
        private readonly SeasonStore _seasonStore;

        public DeleteSeasonCommand(
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

            SeasonModel deletedSeason = _selectedSeasonStore.SelectedSeason;

            string messageBoxText = $"Die Saison \"{_selectedSeasonStore.SelectedSeason.Name}\" und ihre Schnittstellen werden gelöscht.\n\nLöschen fortsetzen?";
            string caption = "Saison umbenennen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    await _seasonStore.Delete(deletedSeason);
                }
                catch (Exception)
                {
                    addEditSeasonFormViewModel.ErrorMessage = "Löschen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    addEditSeasonFormViewModel.IsSubmitting = false;
                }
            }
        }
    }
}
