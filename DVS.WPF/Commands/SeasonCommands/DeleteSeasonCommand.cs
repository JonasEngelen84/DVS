using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class DeleteSeasonCommand( AddEditSeasonViewModel addEditSeasonViewModel, SeasonStore seasonStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = addEditSeasonViewModel.AddEditSeasonFormViewModel;

            if (Confirm($"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\"" +
                $"und ihre Schnittstellen werden gelöscht.\n\nLöschen fortsetzen?", "Saison löschen"))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsDeleting = true;

                try
                {
                    await seasonStore.Delete(addEditSeasonFormViewModel.SelectedSeason, addEditSeasonFormViewModel);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Saison löschen");

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
