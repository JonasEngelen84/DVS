using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.CategoryCommands
{
    public class CloseAddEditCategoryCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;

        public CloseAddEditCategoryCommand(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
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
