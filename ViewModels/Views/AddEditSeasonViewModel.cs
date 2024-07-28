using DVS.Commands.AddEditSeasonCommands;
using DVS.Domain.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditSeasonViewModel : ViewModelBase
    {
        public AddEditSeasonFormViewModel AddEditSeasonFormViewModel { get; }
        public AddEditListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddSeason { get; }

        public AddEditSeasonViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, ClothesModel clothes, AddClothesViewModel addClothesViewModel,
            EditClothesViewModel editClothesViewModel, AddEditListingViewModel addEditListingViewModel)
        {
            ICommand addSeason = new AddSeasonCommand(this, seasonStore);
            ICommand editSeason = new EditSeasonCommand(this, seasonStore);
            ICommand deleteSeason = new DeleteSeasonCommand(this, seasonStore);
            ICommand clearSeasonList = new ClearSeasonListCommand(this, seasonStore);

            AddEditListingViewModel = new(clothes, categoryStore, seasonStore);

            CloseAddSeason = new CloseAddEditSeasonCommand(
                modalNavigationStore, addClothesViewModel, editClothesViewModel);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(addSeason,
                editSeason, deleteSeason, clearSeasonList, addEditListingViewModel)
            {
                AddNewSeason = "Neue Saison",
                EditSelectedSeason = "Saison wählen",
                SelectedSeason = new(null, "Saison wählen")
            };
        }
    }
}
