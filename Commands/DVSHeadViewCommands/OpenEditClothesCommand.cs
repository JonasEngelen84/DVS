using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSHeadViewCommands
{
    public class OpenEditClothesCommand(ClothesModel clothes, ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore) : CommandBase
    {
        private readonly ClothesModel _clothes = clothes;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override void Execute(object parameter)
        {
            EditClothesViewModel EditClothesViewModel = new(
                _clothes, _modalNavigationStore, _categoryStore, _seasonStore, _clothesStore);

            _modalNavigationStore.CurrentViewModel = EditClothesViewModel;
        }
    }
}
