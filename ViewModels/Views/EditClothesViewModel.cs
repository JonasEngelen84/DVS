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
        private readonly ClothesModel _clothes;
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public ICommand CloseModalCommand { get; }


        public EditClothesViewModel(ClothesModel clothes, ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            _clothes = clothes;

            ICommand editClothesCommand = new EditClothesCommand(this, clothesStore , modalNavigationStore);

            ICommand openAddEditCategoriesCommand = new OpenAddEditCategoriesCommand(
                clothes, modalNavigationStore, categoryStore, seasonStore, clothesStore);

            ICommand openAddEditSeasonsCommand = new OpenAddEditSeasonsCommand(
                clothes, modalNavigationStore, categoryStore, seasonStore, clothesStore);

            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditClothesFormViewModel = new(categoryStore, seasonStore,
                clothesStore, openAddEditCategoriesCommand, openAddEditSeasonsCommand, editClothesCommand)
            {
                ID = _clothes.ID, Name = _clothes.Name, Comment = _clothes.Comment,
                Category = _clothes.Category, Season = _clothes.Season
            };
            AddEditClothesFormViewModel.LoadSizes(_clothes);
        }
    }
}
