using DVS.Commands;
using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public ICommand CloseModalCommand { get; }

        public AddEditClothesViewModel(ModalNavigationStore modalNavigationStore,
                                       CategoryStore categoryStore,
                                       SeasonStore seasonStore,
                                       SelectedCategoryStore selectedCategoryStore,
                                       SelectedSeasonStore selectedSeasonStore,
                                       ClothesStore clothesStore)
        {
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);
            ICommand addClothesCommand = new AddClothesCommand(this, clothesStore, modalNavigationStore);
            ICommand editClothesCommand = new EditClothesCommand(this, modalNavigationStore);
            ICommand deleteClothesCommand = new DeleteClothesCommand(modalNavigationStore);
            ICommand clearClothesListCommand = new ClearClothesListCommand(modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(
                modalNavigationStore, categoryStore, seasonStore,
                selectedCategoryStore, selectedSeasonStore, clothesStore);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(
                modalNavigationStore, categoryStore, seasonStore,
                selectedCategoryStore, selectedSeasonStore, clothesStore);

            AddEditClothesFormViewModel = new(
                categoryStore, seasonStore, clothesStore, openAddEditCategoriesCommand, openAddEditSeasonsCommand,
                addClothesCommand, editClothesCommand, deleteClothesCommand, clearClothesListCommand);
        }
    }
}
