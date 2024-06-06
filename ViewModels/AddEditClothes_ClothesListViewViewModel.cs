﻿using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class AddEditClothes_ClothesListviewViewModel : ViewModelBase
    {

        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;

        private readonly ClothesStore _clothesStore;

        public AddEditClothes_ClothesListviewViewModel(ClothesStore clothesStore)
        {
            _clothes = [];
            _clothesStore = clothesStore;
            LoadClothes();
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;
        }

        private void LoadClothes()
        {
            _clothes.Clear();
            
            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                _clothes.Add(clothes);
            }

        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            AddClothes(clothes);
        }
        
        private void AddClothes(ClothesModel clothes)
        {
            _clothes.Add(clothes);
            OnPropertyChanged(nameof(_clothes));
        }
    }
}
