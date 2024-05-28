using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenAddClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public OpenAddClothesCommand(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
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
