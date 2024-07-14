using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenAddClothesCommand(ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(
                _modalNavigationStore, _categoryStore, _seasonStore, _clothesStore);

            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
