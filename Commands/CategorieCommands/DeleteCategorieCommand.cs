using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.CategorieCommands
{
    public class DeleteCategorieCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public DeleteCategorieCommand(AddEditCategorieViewModel addEditCategorieViewModel, ModalNavigationStore modalNavigationStore)
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
