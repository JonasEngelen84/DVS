using DVS.Commands;
using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class EditClothesViewModel : ViewModelBase
    {
        public EditClothesFormViewModel EditClothesFormViewModel { get; }

        public EditClothesViewModel(ModalNavigationStore modalNavigationStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    SelectedCategoryStore selectedCategoryStore,
                                    SelectedSeasonStore selectedSeasonStore,
                                    ClothesStore clothesStore)
        {
            ICommand cancelClothesCommand = new CloseModalCommand(modalNavigationStore);
            ICommand editClothesCommand = new EditClothesCommand(this, modalNavigationStore);
            ICommand deleteClothesCommand = new DeleteClothesCommand(modalNavigationStore);
            ICommand clearClothesListCommand = new ClearClothesListCommand(modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                               categoryStore,
                                                                               seasonStore,
                                                                               selectedCategoryStore,
                                                                               selectedSeasonStore,
                                                                               clothesStore);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore,
                                                                               categoryStore,
                                                                               seasonStore,
                                                                               selectedCategoryStore,
                                                                               selectedSeasonStore,
                                                                               clothesStore);


            EditClothesFormViewModel = new EditClothesFormViewModel(categoryStore,
                                                                    seasonStore,
                                                                    openAddEditCategoriesCommand,
                                                                    openAddEditSeasonsCommand,
                                                                    cancelClothesCommand,
                                                                    editClothesCommand,
                                                                    deleteClothesCommand,
                                                                    clearClothesListCommand);
        }
    }
}
