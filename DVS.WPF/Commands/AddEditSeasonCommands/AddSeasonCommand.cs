using DVS.Domain.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditSeasonCommands
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

            SeasonModel newSeason = new(Guid.NewGuid(), addEditSeasonFormViewModel.AddNewSeason);

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
