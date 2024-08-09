using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class OpenAddClothesCommand(ModalNavigationStore modalNavigationStore, SizeStore sizeStore,
        CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(
                _modalNavigationStore, _sizeStore, _categoryStore, _seasonStore, _clothesStore);

            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
