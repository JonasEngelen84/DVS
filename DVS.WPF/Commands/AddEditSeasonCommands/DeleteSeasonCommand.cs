using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class DeleteSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel, SeasonStore seasonStore) : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel = addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;
            addEditSeasonFormViewModel.HasError = false;
            addEditSeasonFormViewModel.IsDeleting = true;

            string messageBoxText = $"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" und ihre Schnittstellen werden gelöscht.\n\nLöschen fortsetzen?";
            string caption = "Saison löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                try
                {
                    await _seasonStore.Delete((Guid)addEditSeasonFormViewModel.SelectedSeason.GuidID, addEditSeasonFormViewModel);
                }
                catch (Exception)
                {
                    messageBoxText = "Löschen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                    caption = "Saison löschen";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    addEditSeasonFormViewModel.HasError = true;
                }
                finally
                {
                    addEditSeasonFormViewModel.IsDeleting = false;
                }
            }
        }
    }
}
