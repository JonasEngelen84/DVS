using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace DVS.ViewModels
{
    public class DVSDetailedClothesListingViewModel : ViewModelBase
    {
        //private readonly SelectedClothesStore _selectedClothesStore;
        //private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        //private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ClothesStore _clothesStore;

        private readonly ObservableCollection<DetailedClothesListingItem> _detailedClothesListingItemCollection;
        public IEnumerable<DetailedClothesListingItem> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;


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
                DetailedClothesListingItem item = new()
                {
                    ID = clothes.ID,
                    Categorie = clothes.Categorie,
                    Name = clothes.Name,
                    Season = clothes.Season,
                    Size = size.Size,
                    Quantity = size.Quantity,
                    Comment = clothes.Comment
                };

                AddClothesItem(item);
            }
        }
        
        private void ClotheStore_ClothesEdit(ClothesModel clothes)
        {
            
        }

        private void AddClothesItem(DetailedClothesListingItem item)
        {
            _detailedClothesListingItemCollection.Add(item);
        }
    }
}
