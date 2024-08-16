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
            addEditSeasonFormViewModel.HasError = false;
            addEditSeasonFormViewModel.IsSubmitting = true;

            string messageBoxText = $"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditSeasonFormViewModel.EditSelectedSeason}\" umbenannt.\n\nUmbennen fortsetzen?";
            string caption = "Saison bearbeiten";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                Season season = new(addEditSeasonFormViewModel.SelectedSeason.GuidID, addEditSeasonFormViewModel.EditSelectedSeason);

                try
                {
                    await _seasonStore.Update(season, addEditSeasonFormViewModel);
                }
                catch (Exception)
                {
                    messageBoxText = "Bearbeiten der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = "Saison bearbeiten";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    addEditSeasonFormViewModel.HasError = true;
                }
                finally
                {
                    addEditSeasonFormViewModel.IsSubmitting = false;
                }
            }
        }
    }
}
