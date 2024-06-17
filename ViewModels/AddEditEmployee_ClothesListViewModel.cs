using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class AddEditEmployee_ClothesListViewModel : ViewModelBase
    {
        private DVSDetailedClothesListingViewModel _dVSDetailedClothesListingViewModel;
        public DVSDetailedClothesListingViewModel DVSDetailedClothesListingViewModel
        {
            get => _dVSDetailedClothesListingViewModel;
            set
            {
                if (_dVSDetailedClothesListingViewModel != value)
                {
                    _dVSDetailedClothesListingViewModel = value;
                    OnPropertyChanged(nameof(DVSDetailedClothesListingViewModel));
                }
            }
        }

        private readonly ClothesStore _clothesStore;

        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;


        public AddEditEmployee_ClothesListViewModel(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel,
                                                    ClothesStore clothesStore)
        {
            _clothes = [];
            _dVSDetailedClothesListingViewModel = new(clothesStore);
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
