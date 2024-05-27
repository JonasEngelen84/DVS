using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.CategoryCommands
{
    public class SubmitAddCategoryCommand : CommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;

        public SubmitAddCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel, ModalNavigationStore modalNavigationStore)
        {
            _addEditCategoryViewModel = addEditCategoryViewModel;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
