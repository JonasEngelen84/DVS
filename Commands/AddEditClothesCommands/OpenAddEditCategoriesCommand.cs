using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditClothesCommands
{
    public class OpenAddEditCategoriesCommand(ClothesModel clothes, ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore) : CommandBase
    {
        private readonly ClothesModel _clothes = clothes;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(
                _clothes, _modalNavigationStore, _categoryStore, _seasonStore, _clothesStore);

            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
