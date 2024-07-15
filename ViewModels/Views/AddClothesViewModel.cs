using DVS.Commands;
using DVS.Commands.AddEditClothesCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public ICommand CloseModalCommand { get; }

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            ICommand addClothesCommand = new AddClothesCommand(this, clothesStore , modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(
                modalNavigationStore, categoryStore, this, null);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(
                modalNavigationStore, seasonStore, this, null);

            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditClothesFormViewModel = new(null, addClothesCommand, openAddEditCategoriesCommand,
                openAddEditSeasonsCommand,  categoryStore, seasonStore)
            {
                ID = "ID",
                Name = "Name",
                Comment = "Kommentar"
            };
        }
    }
}
