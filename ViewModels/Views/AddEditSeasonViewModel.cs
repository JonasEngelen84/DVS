using DVS.Commands.SeasonCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditSeasonViewModel : ViewModelBase
    {
        public AddEditSeasonFormViewModel AddEditSeasonFormViewModel { get; }

        public AddEditSeasonViewModel(ModalNavigationStore modalNavigationStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      SelectedCategoryStore selectedCategoryStore,
                                      SelectedSeasonStore selectedSeasonStore,
                                      ClothesStore clothesStore)
        {
            ICommand addSeasonCommand = new AddSeasonCommand(this, seasonStore);
            ICommand editSeasonCommand = new EditSeasonCommand(this, modalNavigationStore);
            ICommand deleteSeasonCommand = new DeleteSeasonCommand(this, modalNavigationStore);
            ICommand clearSeasonListCommand = new ClearSeasonListCommand(this, modalNavigationStore);
            ICommand closeAddSeasonCommand = new CloseAddEditSeasonCommand(modalNavigationStore,
                                                                           categoryStore,
                                                                           seasonStore,
                                                                           selectedCategoryStore,
                                                                           selectedSeasonStore,
                                                                           clothesStore);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(seasonStore,
                                                                        selectedSeasonStore,
                                                                        addSeasonCommand,
                                                                        editSeasonCommand,
                                                                        deleteSeasonCommand,
                                                                        clearSeasonListCommand,
                                                                        closeAddSeasonCommand);
        }
    }
}
