using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class EditSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel,
                                   SizeStore sizeStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesStore clothesStore,
                                   ClothesSizeStore clothesSizeStore,
                                   EmployeeClothesSizesStore employeeClothesSizesStore,
                                   EmployeeStore employeeStore)
                                   : AsyncCommandBase
    {
        private readonly AddEditSeasonViewModel _addEditSeasonViewModel = addEditSeasonViewModel;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = _addEditSeasonViewModel.AddEditSeasonFormViewModel;

            if (Confirm($"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditSeasonFormViewModel.EditSelectedSeason}\" umbenannt.\n\nUmbennen fortsetzen?", "Saison umbenennen"))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsSubmitting = true;

                Season season = new(addEditSeasonFormViewModel.SelectedSeason.GuidId, addEditSeasonFormViewModel.EditSelectedSeason);

                try
                {
                    await _seasonStore.Update(season, addEditSeasonFormViewModel);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Saison bearbeiten");

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
