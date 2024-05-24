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

        // Bereitstellung einer ObservableCollection der vorhandenen Kleidung "_clothesCollection".
        // Zur encapsulation (private) von "_clothesCollection" wird ein IEnumerable als pointer verwendet.
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

            FillCL();
        }

        private void FillCL()
        {
            _clothesCollection.Add(new ClothesModel(111, "Sommershirt", "Shirt", "XL", "Sommer", 12));
            _clothesCollection.Add(new ClothesModel(112, "Sommershirt", "Shirt", "L", "Sommer", 8));
            _clothesCollection.Add(new ClothesModel(113, "Sommershirt", "Shirt", "M", "Sommer", 10));
            _clothesCollection.Add(new ClothesModel(114, "Wintershirt", "Shirt", "XL", "Winter", 8));
            _clothesCollection.Add(new ClothesModel(115, "Wintershirt", "Shirt", "L", "Winter", 5));
            _clothesCollection.Add(new ClothesModel(116, "Wintershirt", "Shirt", "M", "Winter", 15));
            _clothesCollection.Add(new ClothesModel(211, "Sommerhose", "Hose", "58", "Sommer", 6));
            _clothesCollection.Add(new ClothesModel(212, "Sommerhose", "Hose", "55", "Sommer", 3));
            _clothesCollection.Add(new ClothesModel(213, "Sommerhose", "Hose", "48", "Sommer", 11));
            _clothesCollection.Add(new ClothesModel(311, "Winterhose", "Hose", "58", "Winter", 6));
            _clothesCollection.Add(new ClothesModel(312, "Winterhose", "Hose", "55", "Winter", 10));
            _clothesCollection.Add(new ClothesModel(313, "Winterhose", "Hose", "48", "Winter", 3));
            _clothesCollection.Add(new ClothesModel(411, "Regenjacke", "Jacke", "XL", "Saisonlos", 12));
            _clothesCollection.Add(new ClothesModel(412, "Regenjacke", "Jacke", "L", "Saisonlos", 7));
            _clothesCollection.Add(new ClothesModel(413, "Regenjacke", "Jacke", "M", "Saisonlos", 59));
            _clothesCollection.Add(new ClothesModel(511, "Fleecejacke", "Jacke", "L", "Saisonlos", 7));
            _clothesCollection.Add(new ClothesModel(512, "Winterjacke", "Jacke", "XL", "Winter", 2));
            _clothesCollection.Add(new ClothesModel(611, "Sommerkappe", "Kopfbedeckung", "", "Saisonlos", 8));
            _clothesCollection.Add(new ClothesModel(612, "Winterkappe", "Kopfbedeckung", "", "Saisonlos", 4));
        }
    }
}
