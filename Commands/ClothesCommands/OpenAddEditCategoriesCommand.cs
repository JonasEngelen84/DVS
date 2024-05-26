using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class OpenAddEditCategoriesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddEditCategoriesCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
