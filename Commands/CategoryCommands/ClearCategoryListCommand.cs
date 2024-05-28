using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.CategoryCommands
{
    public class ClearCategoryListCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public ClearCategoryListCommand(AddEditCategoryViewModel addEditCategorieViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
