using DVS.Commands;
using DVS.Commands.ClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddClothesViewModel : ViewModelBase
    {
        public AddClothesFormViewModel AddClothesFormViewModel { get; }

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   SelectedCategoryStore selectedCategoryStore,
                                   SelectedSeasonStore selectedSeasonStore,
                                   ClothesStore clothesStore)
        {
            ICommand AddClothesCommand = new AddClothesCommand(this, clothesStore);
            ICommand closeModalCommand = new CloseModalCommand(modalNavigationStore);

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

            AddClothesFormViewModel = new(categoryStore,
                                          seasonStore,
                                          clothesStore,
                                          openAddEditCategoriesCommand,
                                          openAddEditSeasonsCommand,
                                          AddClothesCommand,
                                          closeModalCommand);
        }
    }
}
