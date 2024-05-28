using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.SeasonCommands
{
    public class CloseAddEditSeasonCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public CloseAddEditSeasonCommand(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _selectedCategoryStore = selectedCategoryStore;
        }

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(_modalNavigationStore, _selectedCategoryStore);
            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
