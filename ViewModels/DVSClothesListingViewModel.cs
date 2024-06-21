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

        public DVSClothesListingViewModel()
        {
            _clothesListingItemCollection = [];

            LoadClothesListingItemCollection();
            
        }










        public void LoadClothesListingItemCollection()
        {
            var clothes1 = new ClothesModel("1111", "Sommerhose", "Hose", "Sommer", "Auslaufmodell lbabla" +
                "lblb albl ablablbal balabla blabl abalba lbalablab lablab alba lbalab lablabla blablabla blalbabla!!!");
            clothes1.Sizes.Add(new ClothesSizeModel("S", 22, "Auslaufmodell"));
            clothes1.Sizes.Add(new ClothesSizeModel("M", 18, "Auslaufmodell"));
            clothes1.Sizes.Add(new ClothesSizeModel("L", 10, "Auslaufmodell"));
            clothes1.Sizes.Add(new ClothesSizeModel("XL", 15, "Auslaufmodell"));

            var clothes2 = new ClothesModel("2222", "Winterhose", "Hose", "Winter", "Testweise im Sortiment");
            clothes2.Sizes.Add(new ClothesSizeModel("S", 22, null));
            clothes2.Sizes.Add(new ClothesSizeModel("M", 18, null));
            clothes2.Sizes.Add(new ClothesSizeModel("L", 10, null));
            clothes2.Sizes.Add(new ClothesSizeModel("XL", 11, null));
            clothes2.Sizes.Add(new ClothesSizeModel("XLL", 3, "Auslaufmodell"));

            var clothes3 = new ClothesModel("3333", "Regenjacke", "Jacke", "Saisonlos", null);
            clothes3.Sizes.Add(new ClothesSizeModel("S", 22, "Auslaufmodell"));
            clothes3.Sizes.Add(new ClothesSizeModel("M", 18, null));
            clothes3.Sizes.Add(new ClothesSizeModel("L", 10, null));
            clothes3.Sizes.Add(new ClothesSizeModel("XL", 15, null));
            clothes3.Sizes.Add(new ClothesSizeModel("XLL", 8, null));
            clothes3.Sizes.Add(new ClothesSizeModel("3XL", 14, null));

            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(clothes1));
            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(clothes2));
            _clothesListingItemCollection.Add(new ClothesListingItemViewModel(clothes3));
        }
    }
}
