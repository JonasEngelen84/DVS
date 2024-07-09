using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.SeasonCommands
{
    public class ClearSeasonListCommand : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore;

        public ClearSeasonListCommand(AddEditSeasonViewModel addEditSeasonViewModel, SeasonStore seasonStore)
        {
            _addEditSeasonViewModel = addEditSeasonViewModel;
            _seasonStore = seasonStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;

            addEditSeasonFormViewModel.ErrorMessage = null;
            addEditSeasonFormViewModel.IsSubmitting = true;

            try
            {
                string messageBoxText = "Alle Saisons und ihre Schnittstellen werden gelöscht.\nLöschen fortsetzen?";
                string caption = "Alle Saisons löschen";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                if (dialog == MessageBoxResult.Yes)
                {
                    await _seasonStore.ClearSeasons();
                }
            }
            catch (Exception)
            {
                addEditSeasonFormViewModel.ErrorMessage = "Löschen aller Saisons ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditSeasonFormViewModel.IsSubmitting = false;
            }
        }
    }
}
