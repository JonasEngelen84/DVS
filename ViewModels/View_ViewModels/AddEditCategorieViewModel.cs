using DVS.Commands.CategorieCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class AddEditCategorieViewModel : ViewModelBase
    {
        public AddEditCategorieFormViewModel AddEditCategorieFormViewModel { get; }

        public AddEditCategorieViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand submitAddCategorieCommand = new SubmitAddCategorieCommand(this, modalNavigationStore);
            ICommand editCategorieCommand = new EditCategorieCommand(this, modalNavigationStore);
            ICommand deleteCategorieCommand = new DeleteCategorieCommand(this, modalNavigationStore);
            ICommand clearCategorieListCommand = new ClearCategorieListCommand(this, modalNavigationStore);
            ICommand closeAddCategorieCommand = new CloseAddCategorieCommand(modalNavigationStore);

            AddEditCategorieFormViewModel = new AddEditCategorieFormViewModel(submitAddCategorieCommand,
                editCategorieCommand, deleteCategorieCommand, clearCategorieListCommand, closeAddCategorieCommand);
        }
    }
}
