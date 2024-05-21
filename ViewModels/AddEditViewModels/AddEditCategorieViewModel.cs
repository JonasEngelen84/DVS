using DVS.Commands;
using DVS.Commands.AddCategorieViewCommands;
using DVS.Components;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.AddEditViewModels
{
    public class AddEditCategorieViewModel : ViewModelBase
    {
        public DVSAddEditCategorieFormViewModel DVSAddEditCategorieFormViewModel { get; }

        public AddEditCategorieViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand submitAddCategorieCommand = new SubmitAddCategorieCommand(this, _modalNavigationStore);
            ICommand editCategorieCommand = new EditCategorieCommand(this, _modalNavigationStore);
            ICommand deleteCategorieCommand = new DeleteCategorieCommand(this, _modalNavigationStore);
            ICommand clearCategorieListCommand = new ClearCategorieListCommand(this, _modalNavigationStore);
            ICommand closeAddCategorieCommand = new CloseAddCategorieCommand(_modalNavigationStore);

            DVSAddEditCategorieFormViewModel = new DVSAddEditCategorieFormViewModel(submitAddCategorieCommand,
                editCategorieCommand, deleteCategorieCommand, clearCategorieListCommand, closeAddCategorieCommand);
        }
    }
}
