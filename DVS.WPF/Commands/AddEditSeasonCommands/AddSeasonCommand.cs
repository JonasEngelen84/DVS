using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class AddSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel, SeasonStore seasonStore) : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel = addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;
            addEditSeasonFormViewModel.ErrorMessage = null;
            addEditSeasonFormViewModel.IsSubmitting = true;

            Season newSeason = new(Guid.NewGuid(), addEditSeasonFormViewModel.AddNewSeason);

            try
            {
                await _seasonStore.Add(newSeason, addEditSeasonFormViewModel);
            }
            catch (Exception)
            {
                addEditSeasonFormViewModel.ErrorMessage = "Erstellen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditSeasonFormViewModel.IsSubmitting = false;
            }
        }
    }
}
