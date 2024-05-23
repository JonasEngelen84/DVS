using DVS.Commands;
using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class AddClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }

        public AddClothesViewModel(ModalNavigationStore _modalNavigationStore)
        {
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(_modalNavigationStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(_modalNavigationStore);
            ICommand submitAddClothesCommand = new SubmitAddClothesCommand(this, _modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(_modalNavigationStore);

            AddEditClothesFormViewModel = new(openAddEditCategoriesCommand, openAddEditSeasonsCommand, submitAddClothesCommand, closeModalCommand);
        }
    }
}
