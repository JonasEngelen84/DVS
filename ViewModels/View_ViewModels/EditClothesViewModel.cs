using DVS.Commands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Collections.ObjectModel;
using DVS.Commands.ClothesCommands;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class EditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }

        public EditClothesViewModel(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore, selectedCategoryStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore, selectedCategoryStore);
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);
            ICommand submitEditClothesCommand = new SubmitEditClothesCommand(this, modalNavigationStore);

            AddEditClothesFormViewModel = new AddEditClothesFormViewModel(openAddEditCategoriesCommand,
                openAddEditSeasonsCommand, closeModalCommand, submitEditClothesCommand);
        }
    }
}
