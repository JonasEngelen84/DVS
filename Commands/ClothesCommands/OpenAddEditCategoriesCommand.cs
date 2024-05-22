using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.ClothesCommands
{
    public class OpenAddEditCategoriesCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddEditCategorieViewModel addEditCategorieViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
