using DVS.Models;
using DVS.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class ClothesListViewViewModel
    {
        // Bereitstellung einer ObservableCollection der vorhandenen Kleidung "_clothesCollection".
        private readonly ObservableCollection<ClothesModel> _clothesCollection;

        // Zur encapsulation (private) von "_clothesCollection" wird ein IEnumerable als pointer verwendet.
        public IEnumerable<ClothesModel> ClothesCollection => _clothesCollection;

        public ClothesListViewViewModel(SelectedClothesStore _selectedClothesStore)
        {
            _clothesCollection = [];

            HardCodedObjects();
        }


        private void HardCodedObjects()
        {
            _clothesCollection.Add(new ClothesModel(Categorie.Shirt, "Sommershirt", "XL", Season.Sommer, 12));
            _clothesCollection.Add(new ClothesModel(Categorie.Shirt, "Sommershirt", "L", Season.Sommer, 8));
            _clothesCollection.Add(new ClothesModel(Categorie.Shirt, "Sommershirt", "M", Season.Sommer, 10));
            _clothesCollection.Add(new ClothesModel(Categorie.Shirt, "Wintershirt", "XL", Season.Winter, 8));
            _clothesCollection.Add(new ClothesModel(Categorie.Shirt, "Wintershirt", "L", Season.Winter, 5));
            _clothesCollection.Add(new ClothesModel(Categorie.Shirt, "Wintershirt", "M", Season.Winter, 15));
            _clothesCollection.Add(new ClothesModel(Categorie.Hose, "Sommerhose", "58", Season.Sommer, 6));
            _clothesCollection.Add(new ClothesModel(Categorie.Hose, "Sommerhose", "55", Season.Sommer, 3));
            _clothesCollection.Add(new ClothesModel(Categorie.Hose, "Sommerhose", "48", Season.Sommer, 11));
            _clothesCollection.Add(new ClothesModel(Categorie.Hose, "Winterhose", "58", Season.Winter, 6));
            _clothesCollection.Add(new ClothesModel(Categorie.Hose, "Winterhose", "55", Season.Winter, 10));
            _clothesCollection.Add(new ClothesModel(Categorie.Hose, "Winterhose", "48", Season.Winter, 3));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Regenjacke", "XL", Season.Saisonlos, 12));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Regenjacke", "L", Season.Saisonlos, 7));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Regenjacke", "M", Season.Saisonlos, 59));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Fleecejacke", "L", Season.Saisonlos, 7));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Fleecejacke", "M", Season.Saisonlos, 8));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Fleecejacke", "S", Season.Saisonlos, 9));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Winterjacke", "XL", Season.Winter, 2));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Winterjacke", "L", Season.Winter, 5));
            _clothesCollection.Add(new ClothesModel(Categorie.Jacke, "Winterjacke", "M", Season.Winter, 7));
            _clothesCollection.Add(new ClothesModel(Categorie.Kopfbedeckung, "Sommerkappe", "", Season.Saisonlos, 8));
            _clothesCollection.Add(new ClothesModel(Categorie.Kopfbedeckung, "Winterkappe", "", Season.Saisonlos, 4));
        }
    }
}
