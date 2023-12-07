using DVS.Models;
using DVS.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    internal class ClothesListViewViewModel
    {
        // Bereitstellung einer ObservableCollection der vorhandenen Kleidung "_clothesCollection".
        private readonly ObservableCollection<ClothesModel> _clothesCollection;

        // Zur encapsulation (private) von "_clothesCollection" wird ein IEnumerable als pointer verwendet.
        public IEnumerable<ClothesModel> ClothesCollection => _clothesCollection;

        public ClothesListViewViewModel(SelectedClothesStore _selectedClothesStore)
        {
            _clothesCollection = new ObservableCollection<ClothesModel>();

            HardCodedObjects();
        }
























        private void HardCodedObjects()
        {
            _clothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "XL", "Sommer", 12, 19.99));
            _clothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "L", "Sommer", 8, 19.99));
            _clothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "M", "Sommer", 10, 19.99));
            _clothesCollection.Add(new ClothesModel("Shirt", "Wintershirt", "XL", "Winter", 8, 19.99));
            _clothesCollection.Add(new ClothesModel("Shirt", "Wintershirt", "L", "Winter", 5, 19.99));
            _clothesCollection.Add(new ClothesModel("Shirt", "Wintershirt", "M", "Winter", 15, 19.99));
            _clothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "58", "Sommer", 6, 50.29));
            _clothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "55", "Sommer", 3, 50.29));
            _clothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "48", "Sommer", 11, 50.29));
            _clothesCollection.Add(new ClothesModel("Hose", "Winterhose", "58", "Winter", 6, 19.99));
            _clothesCollection.Add(new ClothesModel("Hose", "Winterhose", "55", "Winter", 10, 19.99));
            _clothesCollection.Add(new ClothesModel("Hose", "Winterhose", "48", "Winter", 3, 19.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "XL", "", 12, 25.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "L", "", 7, 25.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "M", "", 5, 25.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Fleecejacke", "L", "", 7, 25.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Fleecejacke", "M", "", 8, 25.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Fleecejacke", "S", "", 9, 25.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "XL", "Winter", 2, 45.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "L", "Winter", 5, 45.99));
            _clothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "M", "Winter", 7, 45.99));
            _clothesCollection.Add(new ClothesModel("Kopfbedeckung", "Sommerkappe", "", "Sommer", 8, 10.99));
            _clothesCollection.Add(new ClothesModel("Kopfbedeckung", "Winterkappe", "", "Winter", 4, 15.99));
        }
    }
}
