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

        public DVSClothesListingViewModel(ClothesStore clothesStore)
        {
            _clothesStore = clothesStore;
            _clothesListingItemCollection = [];

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

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            ClothesListingItemViewModel item = new(clothes);
            _clothesListingItemCollection.Add(item);
        }

        private void ClotheStore_ClothesEdit(ClothesModel clothes)
        {

        }
    }
}
