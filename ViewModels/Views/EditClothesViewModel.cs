using DVS.Commands;
using DVS.Commands.AddEditClothesCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class EditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public ICommand CloseModalCommand { get; }


        public EditClothesViewModel(ClothesModel clothes, ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            ICommand editClothesCommand = new EditClothesCommand(this, clothesStore , modalNavigationStore, clothes.GuidID);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(
                modalNavigationStore, categoryStore, null, this);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(
                modalNavigationStore, seasonStore, null, this);

            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditClothesFormViewModel = new(clothes, editClothesCommand, openAddEditCategoriesCommand,
                openAddEditSeasonsCommand, categoryStore, seasonStore)
            {
                ID = clothes.ID, Name = clothes.Name, Comment = clothes.Comment,
                Category = clothes.Category, Season = clothes.Season,
            };
        }
    }
}
