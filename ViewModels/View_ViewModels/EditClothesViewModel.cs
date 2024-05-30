using DVS.Commands;
using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.View_ViewModels
{
    public class EditClothesViewModel : ViewModelBase
    {
        public EditClothesFormViewModel EditClothesFormViewModel { get; }

        public EditClothesViewModel(ModalNavigationStore modalNavigationStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    SelectedCategoryStore selectedCategoryStore,
                                    SelectedSeasonStore selectedSeasonStore)
        {
            ICommand cancelClothesCommand = new CloseModalCommand(modalNavigationStore);
            ICommand editClothesCommand = new EditClothesCommand(this, modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                               categoryStore,
                                                                               seasonStore,
                                                                               selectedCategoryStore,
                                                                               selectedSeasonStore);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore,
                                                                               categoryStore,
                                                                               seasonStore,
                                                                               selectedCategoryStore,
                                                                               selectedSeasonStore);


            EditClothesFormViewModel = new EditClothesFormViewModel(openAddEditCategoriesCommand,
                                                                    openAddEditSeasonsCommand,
                                                                    cancelClothesCommand,
                                                                    editClothesCommand);
        }
    }
}
