using DVS.Models;
using DVS.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.ViewModels
{
    public class AddEditClothes_ClothesListViewViewModel : ViewModelBase
    {

        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;

        private readonly ClothesStore _clothesStore;

        public AddEditClothes_ClothesListViewViewModel(ClothesStore clothesStore)
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
