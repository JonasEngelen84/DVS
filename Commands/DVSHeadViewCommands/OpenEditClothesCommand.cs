using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSHeadViewCommands
{
    public class OpenEditClothesCommand : CommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ClothesStore _clothesStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;


        public OpenEditClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
                                      ModalNavigationStore modalNavigationStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      ClothesStore clothesStore)
        {
            _clothesListingItemViewModel = clothesListingItemViewModel;
            _modalNavigationStore = modalNavigationStore;
            _clothesStore = clothesStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
        }
        
        public override void Execute(object parameter)
        {
            AddEditClothesViewModel addEditClothesViewModel = new(_modalNavigationStore,
                                                                  _categoryStore,
                                                                  _seasonStore,
                                                                  _clothesStore);

            addEditClothesViewModel.AddEditClothesFormViewModel.ID = _clothesListingItemViewModel.ID;
            addEditClothesViewModel.AddEditClothesFormViewModel.Name = _clothesListingItemViewModel.Name;
            addEditClothesViewModel.AddEditClothesFormViewModel.Comment = _clothesListingItemViewModel.Comment;
            addEditClothesViewModel.AddEditClothesFormViewModel.Category = _clothesListingItemViewModel.Category;
            addEditClothesViewModel.AddEditClothesFormViewModel.Season = _clothesListingItemViewModel.Season;
            addEditClothesViewModel.AddEditClothesFormViewModel.Season = _clothesListingItemViewModel.Season;
            addEditClothesViewModel.AddEditClothesFormViewModel.Season = _clothesListingItemViewModel.Season;
            addEditClothesViewModel.AddEditClothesFormViewModel.LoadSizes(_clothesListingItemViewModel.Clothes);

            _modalNavigationStore.CurrentViewModel = addEditClothesViewModel;
        }
    }
}
