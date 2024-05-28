using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.SeasonCommands
{
    public class AddSeasonCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public AddSeasonCommand(AddEditSeasonViewModel addEditSeasonViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
