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

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore, SelectedCategoryStore selectedCategoryStore)
        {
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore, selectedCategoryStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore, selectedCategoryStore);
            ICommand submitAddClothesCommand = new SubmitAddClothesCommand(this, modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditClothesFormViewModel = new(openAddEditCategoriesCommand, openAddEditSeasonsCommand, submitAddClothesCommand, closeModalCommand);
        }
    }
}
