using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class OpenAddEditCategoriesCommand : CommandBase
    {
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEditCategoriesCommand(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            _selectedCategoryStore = selectedCategoryStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(_modalNavigationStore, _selectedCategoryStore);
            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
