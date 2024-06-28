using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.SeasonCommands
{
    public class AddSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel,
                                  SeasonStore seasonStore)
                                  : CommandBase
    {
        public override async void Execute(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = addEditSeasonViewModel.AddEditSeasonFormViewModel;
            addEditSeasonFormViewModel.ErrorMessage = null;
            addEditSeasonFormViewModel.IsSubmitting = true;
            string newSeason = addEditSeasonFormViewModel.AddNewSeason;

            try
            {
                await seasonStore.Add(newSeason);
            }
            catch (Exception)
            {
                addEditSeasonFormViewModel.ErrorMessage =
                    "Erstellen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditSeasonFormViewModel.IsSubmitting = false;
            }
        }
    }
}
