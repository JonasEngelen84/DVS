using DVS.Commands;
using DVS.Commands.AddEditClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public ICommand CloseModalCommand { get; }

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            ICommand addClothesCommand = new AddClothesCommand(this, clothesStore , modalNavigationStore);
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore, categoryStore, seasonStore, clothesStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore, categoryStore, seasonStore, clothesStore);
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditClothesFormViewModel = new(categoryStore, seasonStore, clothesStore, openAddEditCategoriesCommand, openAddEditSeasonsCommand, addClothesCommand);
        }
    }
}
