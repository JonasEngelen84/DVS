using DVS.Domain.Services.Interfaces;
using DVS.WPF.Commands.SeasonCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class AddEditSeasonViewModel : ViewModelBase
    {
        public AddEditSeasonFormViewModel AddEditSeasonFormViewModel { get; }
        public ICommand CloseAddSeason { get; }

        public AddEditSeasonViewModel(
            ModalNavigationStore modalNavigationStore,
            SeasonStore seasonStore,
            ClothesStore clothesStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore,
            EmployeeStore employeeStore,
            AddClothesViewModel addClothesViewModel,
            EditClothesViewModel editClothesViewModel,
            SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel,
            IDirtyEntitySaver dirtyEntitySaver)
        {
            ICommand addSeason = new AddSeasonCommand(this, seasonStore);

            ICommand updateSeason = new EditSeasonCommand(
                this,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeStore,
                employeeClothesSizesStore);

            ICommand deleteSeason = new DeleteSeasonCommand(
                this,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeStore,
                employeeClothesSizesStore,
                dirtyEntitySaver);

            CloseAddSeason = new CloseAddEditSeasonCommand(
                modalNavigationStore,
                addClothesViewModel,
                editClothesViewModel);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(
                addSeason,
                updateSeason,
                deleteSeason,
                SizesCategoriesSeasonsListingViewModel)
            {
                NewSeason = "Neue Saison",
                SelectedSeason = new(Guid.NewGuid(), "Saison wählen")
            };
        }
    }
}
