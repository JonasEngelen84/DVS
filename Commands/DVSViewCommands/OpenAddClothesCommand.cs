using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenAddClothesCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly SelectedSeasonStore _selectedSeasonStore;
        private readonly ClothesListViewViewModel _clothesListViewViewModel;

        public OpenAddClothesCommand(
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            SelectedCategoryStore selectedCategoryStore,
            SelectedSeasonStore selectedSeasonStore,
            ClothesListViewViewModel clothesListViewViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _selectedCategoryStore = selectedCategoryStore;
            _selectedSeasonStore = selectedSeasonStore;
            _clothesListViewViewModel = clothesListViewViewModel;
        }

        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(_modalNavigationStore,
                                                          _categoryStore,
                                                          _seasonStore,
                                                          _selectedCategoryStore,
                                                          _selectedSeasonStore,
                                                          _clothesListViewViewModel);

            _modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
