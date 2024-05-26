using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public DeleteCategoryCommand(AddEditCategoryViewModel addEditCategorieViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
