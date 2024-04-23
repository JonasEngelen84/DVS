using DVS.Models;
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

        public ClothesListViewViewModel()
        {
            _clothesCollection = [];

            HardCodedObjects();
        }


        private void HardCodedObjects()
        {
            _clothesCollection.Add(new ClothesModel(111, "Sommershirt", Categorie.Shirt, "XL", Season.Sommer, 12));
            _clothesCollection.Add(new ClothesModel(222, "Sommershirt", Categorie.Shirt, "L", Season.Sommer, 8));
            _clothesCollection.Add(new ClothesModel(333, "Sommershirt", Categorie.Shirt, "M", Season.Sommer, 10));
            _clothesCollection.Add(new ClothesModel(444, "Wintershirt", Categorie.Shirt, "XL", Season.Winter, 8));
            _clothesCollection.Add(new ClothesModel(555, "Wintershirt", Categorie.Shirt, "L", Season.Winter, 5));
            _clothesCollection.Add(new ClothesModel(666, "Wintershirt", Categorie.Shirt, "M", Season.Winter, 15));
            _clothesCollection.Add(new ClothesModel(777, "Sommerhose", Categorie.Hose, "58", Season.Sommer, 6));
            _clothesCollection.Add(new ClothesModel(888, "Sommerhose", Categorie.Hose, "55", Season.Sommer, 3));
            _clothesCollection.Add(new ClothesModel(999, "Sommerhose", Categorie.Hose, "48", Season.Sommer, 11));
            _clothesCollection.Add(new ClothesModel(123, "Winterhose", Categorie.Hose, "58", Season.Winter, 6));
            _clothesCollection.Add(new ClothesModel(456, "Winterhose", Categorie.Hose, "55", Season.Winter, 10));
            _clothesCollection.Add(new ClothesModel(789, "Winterhose", Categorie.Hose, "48", Season.Winter, 3));
            _clothesCollection.Add(new ClothesModel(147, "Regenjacke", Categorie.Jacke, "XL", Season.Saisonlos, 12));
            _clothesCollection.Add(new ClothesModel(159, "Regenjacke", Categorie.Jacke, "L", Season.Saisonlos, 7));
            _clothesCollection.Add(new ClothesModel(167, "Regenjacke", Categorie.Jacke, "M", Season.Saisonlos, 59));
            _clothesCollection.Add(new ClothesModel(654, "Fleecejacke", Categorie.Jacke, "L", Season.Saisonlos, 7));
            _clothesCollection.Add(new ClothesModel(321, "Winterjacke", Categorie.Jacke, "XL", Season.Winter, 2));
            _clothesCollection.Add(new ClothesModel(852, "Sommerkappe", Categorie.Kopfbedeckung, "", Season.Saisonlos, 8));
            _clothesCollection.Add(new ClothesModel(753, "Winterkappe", Categorie.Kopfbedeckung, "", Season.Saisonlos, 4));
        }
    }
}
