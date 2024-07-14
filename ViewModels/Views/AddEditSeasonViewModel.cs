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
        public ICommand CloseAddSeasonCommand { get; }

        public AddEditSeasonViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, ClothesStore clothesStore, ClothesModel clothes)
        {
            ICommand addSeasonCommand = new AddSeasonCommand(this, seasonStore);
            ICommand editSeasonCommand = new EditSeasonCommand(this, seasonStore);
            ICommand deleteSeasonCommand = new DeleteSeasonCommand(this, seasonStore);
            ICommand clearSeasonListCommand = new ClearSeasonListCommand(this, seasonStore);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(seasonStore,
                addSeasonCommand, editSeasonCommand, deleteSeasonCommand, clearSeasonListCommand);

            CloseAddSeasonCommand = new CloseAddEditSeasonCommand(
                clothes, modalNavigationStore, categoryStore, seasonStore, clothesStore);
        }
    }
}
