using DVS.Models;
using DVS.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class EmployeesListViewViewModel : ViewModelBase
    {
        // Bereitstellung einer ObservableCollection der Mitarbeiter-Kleidung "_allEmployeeClothesCollection".
        private readonly ObservableCollection<ClothesModel> _allEmployeeClothesCollection;

        // Zur encapsulation (private) von "_allEmployeeClothesCollection" wird ein IEnumerable als pointer verwendet.
        public IEnumerable<ClothesModel> AllEmployeeClothesCollection => _allEmployeeClothesCollection;

        public EmployeesListViewViewModel(SelectedClothesStore _selectedClothesStore)
        {
            _allEmployeeClothesCollection = [];

            HardCodedObjects();
        }





















        private void HardCodedObjects()
        {
            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "XL", "Sommer", 4, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Wintershirt", "M", "Winter", 2, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "54", "Sommer", 3, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Winterhose", "54", "Winter", 2, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "L", "", 1, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Fleecejacke", "S", "", 1, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "XL", "Winter", 1, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Kopfbedeckung", "Sommerkappe", "", "Sommer", 1, 666, "Jonas", "Engelen"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Kopfbedeckung", "Winterkappe", "", "Winter", 1, 666, "Jonas", "Engelen"));

            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "XL", "Sommer", 2, 123, "Markus", "Oettken"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "56", "Sommer", 2, 123, "Markus", "Oettken"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Winterhose", "56", "Winter", 2, 123, "Markus", "Oettken"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "L", "", 1, 123, "Markus", "Oettken"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "XL", "Winter", 1, 123, "Markus", "Oettken"));

            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "XL", "Sommer", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Wintershirt", "M", "Winter", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "48", "Sommer", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Winterhose", "48", "Winter", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "L", "", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Fleecejacke", "S", "", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "XL", "Winter", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Kopfbedeckung", "Sommerkappe", "", "Sommer", 1, 456, "Nadine", "Molik"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Kopfbedeckung", "Winterkappe", "", "Winter", 1, 456, "Nadine", "Molik"));

            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "XL", "Sommer", 2, 789, "Daniel", "Nickol"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Wintershirt", "M", "Winter", 2, 789, "Daniel", "Nickol"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "56", "Sommer", 2, 789, "Daniel", "Nickol"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Winterhose", "56", "Winter", 2, 789, "Daniel", "Nickol"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "L", "", 2, 789, "Daniel", "Nickol"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "XL", "Winter", 2, 789, "Daniel", "Nickol"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Kopfbedeckung", "Sommerkappe", "", "Sommer", 2, 789, "Daniel", "Nickol"));

            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Sommershirt", "XL", "Sommer", 4, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Shirt", "Wintershirt", "M", "Winter", 4, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Sommerhose", "48", "Sommer", 3, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Hose", "Winterhose", "48", "Winter", 3, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Regenjacke", "L", "", 1, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Fleecejacke", "S", "", 1, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Jacke", "Winterjacke", "XL", "Winter", 1, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Kopfbedeckung", "Sommerkappe", "", "Sommer", 1, 753, "Irfan", "Yüksel"));
            //_allEmployeeClothesCollection.Add(new ClothesModel("Kopfbedeckung", "Winterkappe", "", "Winter", 1, 753, "Irfan", "Yüksel"));
        }
    }
}
