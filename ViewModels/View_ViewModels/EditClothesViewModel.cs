using DVS.Commands;
using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class EditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }

        public EditClothesViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(_modalNavigationStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(_modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(_modalNavigationStore);
            ICommand SubmitEditClothesCommand = new SubmitEditClothesCommand(this, _modalNavigationStore);

            AddEditClothesFormViewModel = new AddEditClothesFormViewModel(openAddEditCategoriesCommand,
                openAddEditSeasonsCommand, closeModalCommand, SubmitEditClothesCommand);
        }
    }
}
