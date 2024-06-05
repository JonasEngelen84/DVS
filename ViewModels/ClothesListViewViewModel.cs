using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class ClothesListViewViewModel
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ObservableCollection<ClothesModel> _clothesCollection;
        public IEnumerable<ClothesModel> ClothesCollection => _clothesCollection;

        public ClothesListViewViewModel(
            SelectedClothesStore selectedClothesStore,
            SelectedEmployeeClothesStore selectedEmployeeClothesStore,
            ModalNavigationStore modalNavigationStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _modalNavigationStore = modalNavigationStore;
            _clothesCollection = [];
        }
    }
}
