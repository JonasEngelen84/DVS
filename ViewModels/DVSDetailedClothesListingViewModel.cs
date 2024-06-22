using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class DVSDetailedClothesListingViewModel : ViewModelBase
    {
        //private readonly SelectedClothesStore _selectedClothesStore;
        //private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        //private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ClothesStore _clothesStore;

        private readonly ObservableCollection<DetailedClothesListingItemModel> _detailedClothesListingItemCollection;
        public IEnumerable<DetailedClothesListingItemModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;


        public DVSDetailedClothesListingViewModel(ClothesStore clothesStore)
        {
            //_selectedClothesStore = selectedClothesStore;
            //_selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            //_modalNavigationStore = modalNavigationStore;
            _clothesStore = clothesStore;
            _detailedClothesListingItemCollection = [];

            ClothesStore_ClothesLoaded();
            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
        }


        protected override void Dispose()
        {
            _clothesStore.ClothesLoaded -= ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;

            base.Dispose();
        }

        public void ClothesStore_ClothesLoaded()
        {
            _detailedClothesListingItemCollection.Clear();

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            foreach (ClothesSizeModel size in clothes.Sizes)
            {
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemModel(clothes.ID,
                                                                                              clothes.Name,
                                                                                              clothes.Categorie,
                                                                                              clothes.Season,
                                                                                              size.Size,
                                                                                              size.Quantity,
                                                                                              clothes.Comment));
            }
        }
        
        private void ClotheStore_ClothesEdit(ClothesModel clothes)
        {
            
        }
    }
}
