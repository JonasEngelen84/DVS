using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class DVSClothesListingViewModel : ViewModelBase
    {
        private readonly ClothesStore _clothesStore;

        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesListingItemCollection;
        public IEnumerable<ClothesListingItemViewModel> ClothesListingItemCollection => _clothesListingItemCollection;

        private readonly ObservableCollection<DetailedClothesListingItemModel> _detailedClothesListingItemCollection;
        public IEnumerable<DetailedClothesListingItemModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;

        public DVSClothesListingViewModel(ClothesStore clothesStore)
        {
            _clothesStore = clothesStore;
            _clothesListingItemCollection = [];
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
            _clothesListingItemCollection.Clear();
            _detailedClothesListingItemCollection.Clear();

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            ClothesListingItemViewModel item = new(clothes);
            _clothesListingItemCollection.Add(item);

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

        // Drag n Drop
        public void RemoveClothes(DetailedClothesListingItemModel clothes)
        {
            if (clothes != null && _detailedClothesListingItemCollection.Contains(clothes))
            {
                _detailedClothesListingItemCollection.Remove(clothes);
                OnPropertyChanged(nameof(DetailedClothesListingItemCollection));
            }
        }

        // Drag n Drop
        public void AddClothes(DetailedClothesListingItemModel clothes)
        {
            if (clothes != null)
            {
                _detailedClothesListingItemCollection.Add(clothes);
                OnPropertyChanged(nameof(DetailedClothesListingItemCollection));
            }
        }
    }
}
