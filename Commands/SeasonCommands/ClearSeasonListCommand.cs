using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.SeasonCommands
{
    public class ClearSeasonListCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public ClearSeasonListCommand(
            AddEditSeasonViewModel AddEditSeasonViewModel,ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
    }
}
