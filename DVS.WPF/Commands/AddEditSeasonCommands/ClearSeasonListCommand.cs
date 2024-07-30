using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class ClearSeasonListCommand(AddEditSeasonViewModel addEditSeasonViewModel, SeasonStore seasonStore) : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel = addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;

            addEditSeasonFormViewModel.ErrorMessage = null;
            addEditSeasonFormViewModel.IsSubmitting = true;

            string messageBoxText = "Alle Saisons und ihre Schnittstellen werden gelöscht.\nLöschen fortsetzen?";
            string caption = "Alle Saisons löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    await _seasonStore.ClearSeasons(addEditSeasonFormViewModel);
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
}
