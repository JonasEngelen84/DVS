using DVS.Commands.SeasonCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class AddEditSeasonViewModel : ViewModelBase
    {
        public AddEditSeasonFormViewModel AddEditSeasonFormViewModel { get; }

        public AddEditSeasonViewModel(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            ICommand submitAddSeasonCommand = new AddSeasonCommand(this, modalNavigationStore);
            ICommand editSeasonCommand = new EditSeasonCommand(this, modalNavigationStore);
            ICommand deleteSeasonCommand = new DeleteSeasonCommand(this, modalNavigationStore);
            ICommand clearSeasonListCommand = new ClearSeasonListCommand(this, modalNavigationStore);
            ICommand closeAddSeasonCommand = new CloseAddEditSeasonCommand(modalNavigationStore, selectedCategoryStore);

            AddEditSeasonFormViewModel = new AddEditSeasonFormViewModel(submitAddSeasonCommand,
                editSeasonCommand, deleteSeasonCommand, clearSeasonListCommand, closeAddSeasonCommand);
        }
    }
}
