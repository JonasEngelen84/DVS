using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class EditSeasonCommand(
        AddEditSeasonViewModel addEditSeasonViewModel,
        SizeStore sizeStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = addEditSeasonViewModel.AddEditSeasonFormViewModel;

            if (Confirm($"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditSeasonFormViewModel.EditSelectedSeason}\" umbenannt.\n\nUmbennen fortsetzen?", "Saison umbenennen"))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsSubmitting = true;

                Season season = new(addEditSeasonFormViewModel.SelectedSeason.GuidId, addEditSeasonFormViewModel.EditSelectedSeason);

                try
                {
                    await seasonStore.Update(season, addEditSeasonFormViewModel);
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
