using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class AddEditEmployee_ClothesListViewModel : ViewModelBase
    {
        public DVSDetailedClothesListingViewModel _dVSDetailedClothesListingViewModel;
        private readonly ClothesStore _clothesStore;

        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;


        public AddEditEmployee_ClothesListViewModel(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel,
                                                    ClothesStore clothesStore)
        {
            _clothes = [];
            _dVSDetailedClothesListingViewModel = dVSDetailedClothesListingViewModel;
            _clothesStore = clothesStore;
            LoadClothes();
        }


        private void LoadClothes()
        {
            _clothes.Clear();
            
            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                _clothes.Add(clothes);
            }
        }
    }
}
