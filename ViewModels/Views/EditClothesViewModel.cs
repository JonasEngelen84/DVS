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
        public AddEditListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddEditClothes { get; }


        public EditClothesViewModel(ClothesModel clothes, ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            AddEditListingViewModel = new(clothes, categoryStore, seasonStore);
            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand editClothesCommand = new EditClothesCommand(this, clothesStore , modalNavigationStore, clothes.GuidID);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(
                modalNavigationStore, categoryStore, seasonStore, clothes, null, this, AddEditListingViewModel);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(
                modalNavigationStore, categoryStore, seasonStore, clothes, null, this, AddEditListingViewModel);

            AddEditClothesFormViewModel = new(clothes, editClothesCommand,
                openAddEditCategoriesCommand, openAddEditSeasonsCommand, AddEditListingViewModel)
            {
                ID = clothes.ID,
                Name = clothes.Name,
                Category = clothes.Category,
                Season = clothes.Season,
                Comment = clothes.Comment
            };
        }
    }
}
