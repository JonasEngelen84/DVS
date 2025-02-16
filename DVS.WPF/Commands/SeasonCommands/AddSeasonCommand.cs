using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class AddSeasonCommand(
        AddEditSeasonViewModel addEditSeasonViewModel,
        SeasonStore seasonStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = addEditSeasonViewModel.AddEditSeasonFormViewModel;
            addEditSeasonFormViewModel.HasError = false;
            addEditSeasonFormViewModel.IsSubmitting = true;

            Season newSeason = new(Guid.NewGuid(), addEditSeasonFormViewModel.AddNewSeason);

            try
            {
                await seasonStore.Add(newSeason, addEditSeasonFormViewModel);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Saison erstellen");

                addEditSeasonFormViewModel.HasError = true;
            }
            finally
            {
                addEditSeasonFormViewModel.IsSubmitting = false;
            }
        }
    }
}
