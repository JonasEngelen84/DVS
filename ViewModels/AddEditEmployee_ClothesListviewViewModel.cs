using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class AddEditEmployee_ClothesListviewViewModel : ViewModelBase
    {

        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;

        private readonly ClothesStore _clothesStore;


        public AddEditEmployee_ClothesListviewViewModel(ClothesStore clothesStore)
        {
            _clothes = [];
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
