using DVS.Commands.ClothesCommands;
using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class ClothesListViewViewModel : ViewModelBase
    {
        //private readonly SelectedClothesStore _selectedClothesStore;
        //private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        //private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ClothesStore _clothesStore;

        private readonly ObservableCollection<ClothesModel> _clothes;
        public IEnumerable<ClothesModel> Clothes => _clothes;


        public ClothesListViewViewModel(ClothesStore clothesStore)
        {
            //_selectedClothesStore = selectedClothesStore;
            //_selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            //_modalNavigationStore = modalNavigationStore;
            _clothesStore = clothesStore;
            _clothes = [];

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

        private void ClothesStore_ClothesLoaded()
        {
            _clothes.Clear();

            foreach(ClothesModel clothes in _clothesStore.Clothes)
            {
                AddClothes(clothes);
            }
        }
        
        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            AddClothes(clothes);
        }
        
        private void ClotheStore_ClothesEdit(ClothesModel clothes)
        {
            
        }

        private void AddClothes(ClothesModel clothes)
        {
            _clothes.Add(clothes);
        }
    }
}
