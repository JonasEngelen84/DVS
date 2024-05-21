using DVS.Commands;
using DVS.Commands.AddClothesViewCommands;
using DVS.Stores;
using System.Windows.Input;
using DVS.ViewModels.AddViewModels.Forms;

namespace DVS.ViewModels.AddEditViewModels
{
    public class AddClothesViewModel : ViewModelBase
    {
        public DVSAddClothesFormViewModel DVSAddClothesFormViewModel { get; }

        public AddClothesViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(_modalNavigationStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(_modalNavigationStore);
            ICommand submitAddClothesCommand = new SubmitAddClothesCommand(this, _modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(_modalNavigationStore);

            DVSAddClothesFormViewModel = new(openAddEditCategoriesCommand, openAddEditSeasonsCommand, submitAddClothesCommand, closeModalCommand);
        }
    }
}
