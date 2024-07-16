using DVS.Commands.AddEditSeasonCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditSeasonViewModel : ViewModelBase
    {
        public AddEditSeasonFormViewModel AddEditSeasonFormViewModel { get; }
        public AddEditListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddSeasonCommand { get; }

        public AddEditSeasonViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, ClothesModel clothes, AddClothesViewModel addClothesViewModel,
            EditClothesViewModel editClothesViewModel)
        {
            ICommand addSeasonCommand = new AddSeasonCommand(this, seasonStore);
            ICommand editSeasonCommand = new EditSeasonCommand(this, seasonStore);
            ICommand deleteSeasonCommand = new DeleteSeasonCommand(this, seasonStore);
            ICommand clearSeasonListCommand = new ClearSeasonListCommand(this, seasonStore);

            AddEditListingViewModel = new(clothes, categoryStore, seasonStore);

            CloseAddSeasonCommand = new CloseAddEditSeasonCommand(
                modalNavigationStore, addClothesViewModel, editClothesViewModel);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(addSeasonCommand,
                editSeasonCommand, deleteSeasonCommand, clearSeasonListCommand, AddEditListingViewModel)
            {
                AddNewSeason = "Neue Saison",
                EditSeason = "Saison wählen",
                SelectedSeason = new(null, "Saison wählen")
            };
        }
    }
}
