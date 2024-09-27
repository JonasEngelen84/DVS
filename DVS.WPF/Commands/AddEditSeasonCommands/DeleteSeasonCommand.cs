using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditSeasonCommands
{
    public class DeleteSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel,
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

            if (Confirm($"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\"" +
                $"und ihre Schnittstellen werden gelöscht.\n\nLöschen fortsetzen?", "Saison löschen"))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsDeleting = true;

                try
                {
                    await _seasonStore.Delete(addEditSeasonFormViewModel.SelectedSeason, addEditSeasonFormViewModel);
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
