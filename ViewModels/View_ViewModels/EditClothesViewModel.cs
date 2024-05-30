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

        public EditClothesViewModel(ModalNavigationStore modalNavigationStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    SelectedCategoryStore selectedCategoryStore,
                                    SelectedSeasonStore selectedSeasonStore)
        {
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);
            ICommand submitEditClothesCommand = new SubmitEditClothesCommand(this, modalNavigationStore);
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


            AddEditClothesFormViewModel = new AddEditClothesFormViewModel(openAddEditCategoriesCommand,
                                                                          openAddEditSeasonsCommand,
                                                                          closeModalCommand,
                                                                          submitEditClothesCommand);
        }
    }
}
