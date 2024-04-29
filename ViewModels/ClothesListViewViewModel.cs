using DVS.Models;
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

        private ClothesListViewViewModel _selectedClothesItem;
        public ClothesListViewViewModel SelectedClothesItem
        {
            get
            {
                return _selectedClothesItem;
            }
            set
            {
                _selectedClothesItem = value;
            }
        }

        private void HardCodedObjects()
        {
            _clothesCollection.Add(new ClothesModel(111, "Sommershirt", Categorie.Shirt, "XL", Season.Sommer, 12));
            _clothesCollection.Add(new ClothesModel(112, "Sommershirt", Categorie.Shirt, "L", Season.Sommer, 8));
            _clothesCollection.Add(new ClothesModel(113, "Sommershirt", Categorie.Shirt, "M", Season.Sommer, 10));
            _clothesCollection.Add(new ClothesModel(114, "Wintershirt", Categorie.Shirt, "XL", Season.Winter, 8));
            _clothesCollection.Add(new ClothesModel(115, "Wintershirt", Categorie.Shirt, "L", Season.Winter, 5));
            _clothesCollection.Add(new ClothesModel(116, "Wintershirt", Categorie.Shirt, "M", Season.Winter, 15));
            _clothesCollection.Add(new ClothesModel(211, "Sommerhose", Categorie.Hose, "58", Season.Sommer, 6));
            _clothesCollection.Add(new ClothesModel(212, "Sommerhose", Categorie.Hose, "55", Season.Sommer, 3));
            _clothesCollection.Add(new ClothesModel(213, "Sommerhose", Categorie.Hose, "48", Season.Sommer, 11));
            _clothesCollection.Add(new ClothesModel(311, "Winterhose", Categorie.Hose, "58", Season.Winter, 6));
            _clothesCollection.Add(new ClothesModel(312, "Winterhose", Categorie.Hose, "55", Season.Winter, 10));
            _clothesCollection.Add(new ClothesModel(313, "Winterhose", Categorie.Hose, "48", Season.Winter, 3));
            _clothesCollection.Add(new ClothesModel(411, "Regenjacke", Categorie.Jacke, "XL", Season.Saisonlos, 12));
            _clothesCollection.Add(new ClothesModel(412, "Regenjacke", Categorie.Jacke, "L", Season.Saisonlos, 7));
            _clothesCollection.Add(new ClothesModel(413, "Regenjacke", Categorie.Jacke, "M", Season.Saisonlos, 59));
            _clothesCollection.Add(new ClothesModel(511, "Fleecejacke", Categorie.Jacke, "L", Season.Saisonlos, 7));
            _clothesCollection.Add(new ClothesModel(512, "Winterjacke", Categorie.Jacke, "XL", Season.Winter, 2));
            _clothesCollection.Add(new ClothesModel(611, "Sommerkappe", Categorie.Kopfbedeckung, "", Season.Saisonlos, 8));
            _clothesCollection.Add(new ClothesModel(612, "Winterkappe", Categorie.Kopfbedeckung, "", Season.Saisonlos, 4));
        }
    }
}
