using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class OpenEditClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
        ModalNavigationStore modalNavigationStore, CategoryStore categoryStore,
        SeasonStore seasonStore, ClothesStore clothesStore) : CommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override void Execute(object parameter)
        {
            ClothesModel _clothes = _clothesListingItemViewModel.Clothes;

            EditClothesViewModel EditClothesViewModel = new(
                _clothes, _modalNavigationStore, _categoryStore, _seasonStore, _clothesStore);

            _modalNavigationStore.CurrentViewModel = EditClothesViewModel;
        }
    }
}
