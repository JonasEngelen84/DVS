using DVS.Commands;
using DVS.Commands.AddEditClothesCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class EditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public ICommand CloseModalCommand { get; }


        public EditClothesViewModel(ModalNavigationStore modalNavigationStore, CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            ICommand editClothesCommand = new EditClothesCommand(this, clothesStore , modalNavigationStore);
            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(modalNavigationStore, categoryStore, seasonStore, clothesStore);
            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(modalNavigationStore, categoryStore, seasonStore, clothesStore);
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditClothesFormViewModel = new(categoryStore, seasonStore, clothesStore, openAddEditCategoriesCommand, openAddEditSeasonsCommand, editClothesCommand);
        }
    }
}
