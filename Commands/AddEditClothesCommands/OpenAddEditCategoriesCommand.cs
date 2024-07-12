using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditClothesCommands
{
    public class OpenAddEditCategoriesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesStore _clothesStore;

        public OpenAddEditCategoriesCommand(ModalNavigationStore modalNavigationStore,
                                            CategoryStore categoryStore,
                                            SeasonStore seasonStore,
                                            ClothesStore clothesStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _clothesStore = clothesStore;
        }

        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(_modalNavigationStore,
                                                                     _categoryStore,
                                                                     _seasonStore,
                                                                     _clothesStore);

            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
