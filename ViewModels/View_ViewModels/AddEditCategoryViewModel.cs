using DVS.Commands.CategoryCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class AddEditCategoryViewModel : ViewModelBase
    {
        public AddEditCategoryFormViewModel AddEditCategoryFormViewModel { get; }

        public AddEditCategoryViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand submitAddCategoryCommand = new SubmitAddCategoryCommand(this, modalNavigationStore);
            ICommand editCategoryCommand = new EditCategoryCommand(this, modalNavigationStore);
            ICommand deleteCategoryCommand = new DeleteCategoryCommand(this, modalNavigationStore);
            ICommand clearCategoryListCommand = new ClearCategoryListCommand(this, modalNavigationStore);
            ICommand closeAddCategoryCommand = new CloseAddCategoryCommand(modalNavigationStore);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(submitAddCategoryCommand,
                editCategoryCommand, deleteCategoryCommand, clearCategoryListCommand, closeAddCategoryCommand);
        }
    }
}
