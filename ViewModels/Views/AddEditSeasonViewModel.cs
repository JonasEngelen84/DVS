using DVS.Commands.AddEditSeasonCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditSeasonViewModel : ViewModelBase
    {
        public AddEditSeasonFormViewModel AddEditSeasonFormViewModel { get; }
        public ICommand CloseAddSeasonCommand { get; }

        public AddEditSeasonViewModel(
            ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
            SeasonStore seasonStore, SelectedSeasonStore selectedSeasonStore,
            SelectedCategoryStore selectedCategoryStore, ClothesStore clothesStore)
        {
            ICommand addSeasonCommand = new AddSeasonCommand(this, seasonStore);
            ICommand editSeasonCommand = new EditSeasonCommand(this, selectedSeasonStore, seasonStore);
            ICommand deleteSeasonCommand = new DeleteSeasonCommand(this, selectedSeasonStore, seasonStore);
            ICommand clearSeasonListCommand = new ClearSeasonListCommand(this, seasonStore);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(
                seasonStore, selectedSeasonStore, addSeasonCommand, 
               editSeasonCommand, deleteSeasonCommand, clearSeasonListCommand);

            CloseAddSeasonCommand = new CloseAddEditSeasonCommand(
                modalNavigationStore, categoryStore, seasonStore,
                selectedCategoryStore, selectedSeasonStore, clothesStore);
        }
    }
}
