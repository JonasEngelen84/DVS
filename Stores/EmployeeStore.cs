using DVS.Models;
using DVS.ViewModels;
using System.Collections.ObjectModel;

namespace DVS.Stores
{
    public class EmployeeStore
    {
        private readonly ObservableCollection<EmployeeModel> _employees;
        public IEnumerable<EmployeeModel> Employees => _employees;

        public event Action EmployeesLoaded;
        public event Action<EmployeeModel> EmployeeAdded;
        public event Action<EmployeeModel> EmployeeUpdated;


        public EmployeeStore()
        {
            _employees =
            [
                //new EmployeeModel("1324", "Engelen", "Jonas", "Vertrag läuft aus")
                //{
                //    Clothes = [new DetailedClothesListingItemModel("111", "Winterhose", new("Hose"), new("Winter"), "XL", 3),
                //               new DetailedClothesListingItemModel("112", "Sommershirt", new("Shirt"), new("Sommer"), "L", 4),
                //               new DetailedClothesListingItemModel("114", "Wintershirt", new("Shirt"), new("Winter"), "L", 4),
                //               new DetailedClothesListingItemModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), "XL", 1),
                //               new DetailedClothesListingItemModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), "45", 1)]
                //},

                //new EmployeeModel("1212", "Molik", "Nadine", null)
                //{
                //    Clothes = [new DetailedClothesListingItemModel("111", "Winterhose", new("Hose"), new("Winter"), "XL", 3),
                //               new DetailedClothesListingItemModel("112", "Sommershirt", new("Shirt"), new("Sommer"), "L", 4),
                //               new DetailedClothesListingItemModel("114", "Wintershirt", new("Shirt"), new("Winter"), "L", 4),
                //               new DetailedClothesListingItemModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), "XL", 1),
                //               new DetailedClothesListingItemModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), "45", 1)]
                //},

                //new EmployeeModel("1112", "Yüksel", "Kemal", null)
                //{
                //    Clothes = [new DetailedClothesListingItemModel("111", "Winterhose", new("Hose"), new("Winter"), "XL", 3),
                //               new DetailedClothesListingItemModel("112", "Sommershirt", new("Shirt"), new("Sommer"), "L", 4),
                //               new DetailedClothesListingItemModel("114", "Wintershirt", new("Shirt"), new("Winter"), "L", 4),
                //               new DetailedClothesListingItemModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), "XL", 1),
                //               new DetailedClothesListingItemModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), "45", 1)]
                //},

                //new EmployeeModel("1213", "Oetken", "Markus", null)
                //{
                //    Clothes = [new DetailedClothesListingItemModel("111", "Winterhose", new("Hose"), new("Winter"), "XL", 3),
                //               new DetailedClothesListingItemModel("112", "Sommershirt", new("Shirt"), new("Sommer"), "L", 4),
                //               new DetailedClothesListingItemModel("114", "Wintershirt", new("Shirt"), new("Winter"), "L", 4),
                //               new DetailedClothesListingItemModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), "XL", 1),
                //               new DetailedClothesListingItemModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), "45", 1)]
                //},

                //new EmployeeModel("1231", "Nickol", "Daniel", null)
                //{
                //    Clothes = [new DetailedClothesListingItemModel("111", "Winterhose", new("Hose"), new("Winter"), "XL", 3),
                //               new DetailedClothesListingItemModel("112", "Sommershirt", new("Shirt"), new("Sommer"), "L", 4),
                //               new DetailedClothesListingItemModel("114", "Wintershirt", new("Shirt"), new("Winter"), "L", 4),
                //               new DetailedClothesListingItemModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), "XL", 1),
                //               new DetailedClothesListingItemModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), "45", 1)]
                //},

                //new EmployeeModel("1132", "Yüksel", "Irfan", null)
                //{
                //    Clothes = [new DetailedClothesListingItemModel("111", "Winterhose", new("Hose"), new("Winter"), "XL", 3),
                //               new DetailedClothesListingItemModel("112", "Sommershirt", new("Shirt"), new("Sommer"), "L", 4),
                //               new DetailedClothesListingItemModel("114", "Wintershirt", new("Shirt"), new("Winter"), "L", 4),
                //               new DetailedClothesListingItemModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), "XL", 1),
                //               new DetailedClothesListingItemModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), "45", 1)]
                //},

                //new EmployeeModel("1221", "Killen", "Stefan", null)
                //{
                //    Clothes = [new DetailedClothesListingItemModel("111", "Winterhose", new("Hose"), new("Winter"), "XL", 3),
                //               new DetailedClothesListingItemModel("112", "Sommershirt", new("Shirt"), new("Sommer"), "L", 4),
                //               new DetailedClothesListingItemModel("114", "Wintershirt", new("Shirt"), new("Winter"), "L", 4),
                //               new DetailedClothesListingItemModel("113", "Regenjacke", new("Jacke"), new("Saisonlos"), "XL", 1),
                //               new DetailedClothesListingItemModel("115", "Schuhe", new("Schuhwerk"), new("Saisonlos"), "45", 1)]
                //}
            ];
        }


        public async Task Load()
        {
            EmployeesLoaded?.Invoke();
        }

        public async Task Add(EmployeeModel employee)
        {
            _employees.Add(employee);
            EmployeeAdded?.Invoke(employee);
        }
    }
}
