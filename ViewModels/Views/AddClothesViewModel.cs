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
        public AddEditListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseModalCommand { get; }

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            AddEditListingViewModel = new(null, categoryStore, seasonStore);
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            ICommand addClothesCommand = new AddClothesCommand(this, clothesStore , modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(
                modalNavigationStore, categoryStore, seasonStore, null, this, null, AddEditListingViewModel);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(
                modalNavigationStore, categoryStore, seasonStore, null, this, null, AddEditListingViewModel);

            AddEditClothesFormViewModel = new(null, addClothesCommand,
                openAddEditCategoriesCommand, openAddEditSeasonsCommand, AddEditListingViewModel)
            {
                ID = "ID",
                Name = "Name",
                Comment = "Kommentar"
            };
        }
    }
}
