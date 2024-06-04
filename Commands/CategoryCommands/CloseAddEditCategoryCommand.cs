using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.CategoryCommands
{
    public class CloseAddEditCategoryCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly SelectedSeasonStore _selectedSeasonStore;
        private readonly ClothesListViewViewModel _clothesListViewViewModel;

        public CloseAddEditCategoryCommand(
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
            if(_modalNavigationStore.PreviousViewModel is AddClothesViewModel)
            {
                AddClothesViewModel addClothesViewModel = new(_modalNavigationStore,
                                                          _categoryStore,
                                                          _seasonStore,
                                                          _selectedCategoryStore,
                                                          _selectedSeasonStore,
                                                          _clothesListViewViewModel);

                _modalNavigationStore.CurrentViewModel = addClothesViewModel;
            }
            else
            {
                EditClothesViewModel editClothesViewModel = new EditClothesViewModel(_modalNavigationStore,
                                                                                     _categoryStore,
                                                                                     _seasonStore,
                                                                                     _selectedCategoryStore,
                                                                                     _selectedSeasonStore,
                                                                                     _clothesListViewViewModel);

                _modalNavigationStore.CurrentViewModel = editClothesViewModel;
            }
        }
    }
}
