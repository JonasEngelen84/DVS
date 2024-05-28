using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.SeasonCommands
{
    public class EditSeasonCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public EditSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
