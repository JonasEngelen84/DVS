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

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore, SeasonStore seasonStore,
            SelectedCategoryStore selectedCategoryStore, SelectedSeasonStore selectedSeasonStore)
        {
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore, categoryStore , seasonStore, selectedCategoryStore, selectedSeasonStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore, categoryStore, seasonStore, selectedCategoryStore, selectedSeasonStore);
            ICommand submitAddClothesCommand = new SubmitAddClothesCommand(this, modalNavigationStore);
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditClothesFormViewModel = new(openAddEditCategoriesCommand, openAddEditSeasonsCommand, submitAddClothesCommand, closeModalCommand);
        }
    }
}
