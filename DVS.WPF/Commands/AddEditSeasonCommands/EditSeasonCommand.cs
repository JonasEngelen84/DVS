using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class EditSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel, SeasonStore seasonStore) : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel = addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;

            string messageBoxText = $"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditSeasonFormViewModel.EditSeason}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Saison umbenennen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                addEditSeasonFormViewModel.ErrorMessage = null;
                addEditSeasonFormViewModel.IsSubmitting = true;

                SeasonModel season = new(addEditSeasonFormViewModel.SelectedSeason.GuidID, addEditSeasonFormViewModel.EditSelectedSeason);

                try
                {
                    await _seasonStore.Update(season, addEditSeasonFormViewModel);
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
