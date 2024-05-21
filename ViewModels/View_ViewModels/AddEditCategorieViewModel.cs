using DVS.Commands.AddCategorieViewCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class AddEditCategorieViewModel : ViewModelBase
    {
        public AddEditCategorieFormViewModel AddEditCategorieFormViewModel { get; }

        public AddEditCategorieViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand submitAddCategorieCommand = new SubmitAddCategorieCommand(this, _modalNavigationStore);
            ICommand editCategorieCommand = new EditCategorieCommand(this, _modalNavigationStore);
            ICommand deleteCategorieCommand = new DeleteCategorieCommand(this, _modalNavigationStore);
            ICommand clearCategorieListCommand = new ClearCategorieListCommand(this, _modalNavigationStore);
            ICommand closeAddCategorieCommand = new CloseAddCategorieCommand(_modalNavigationStore);

            AddEditCategorieFormViewModel = new AddEditCategorieFormViewModel(submitAddCategorieCommand,
                editCategorieCommand, deleteCategorieCommand, clearCategorieListCommand, closeAddCategorieCommand);
        }
    }
}
