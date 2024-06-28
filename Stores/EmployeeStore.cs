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
                new EmployeeModel("1324", "Engelen", "Jonas", "Vertrag läuft aus")
                {
                    Clothes = [new DetailedClothesListingItemViewModel("111", "Winterhose", "Hose", "Winter", "XL", 3, "dfgdfhdhsfdghfgfsffgdsfgfdg"),
                               new DetailedClothesListingItemViewModel("112", "Sommershirt", "Shirt", "Sommer", "L", 4, " "),
                               new DetailedClothesListingItemViewModel("114", "Wintershirt", "Shirt", "Winter", "L", 4, " "),
                               new DetailedClothesListingItemViewModel("113", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, " "),
                               new DetailedClothesListingItemViewModel("115", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, " ")]
                },

                new EmployeeModel("1212", "Molik", "Nadine", null)
                {
                    Clothes = [new DetailedClothesListingItemViewModel("111", "Winterhose", "Hose", "Winter", "XL", 3, null),
                               new DetailedClothesListingItemViewModel("112", "Sommershirt", "Shirt", "Sommer", "L", 4, null),
                               new DetailedClothesListingItemViewModel("114", "Wintershirt", "Shirt", "Winter", "L", 4, null),
                               new DetailedClothesListingItemViewModel("113", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                               new DetailedClothesListingItemViewModel("115", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null)]
                },

                new EmployeeModel("1112", "Yüksel", "Kemal", null)
                {
                    Clothes = [new DetailedClothesListingItemViewModel("111", "Winterhose", "Hose", "Winter", "XL", 3, null),
                               new DetailedClothesListingItemViewModel("112", "Sommershirt", "Shirt", "Sommer", "L", 4, null),
                               new DetailedClothesListingItemViewModel("114", "Wintershirt", "Shirt", "Winter", "L", 4, null),
                               new DetailedClothesListingItemViewModel("113", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                               new DetailedClothesListingItemViewModel("115", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null)]
                },

                new EmployeeModel("1213", "Oetken", "Markus", null)
                {
                    Clothes = [new DetailedClothesListingItemViewModel("111", "Winterhose", "Hose", "Winter", "XL", 3, null),
                               new DetailedClothesListingItemViewModel("112", "Sommershirt", "Shirt", "Sommer", "L", 4, null),
                               new DetailedClothesListingItemViewModel("114", "Wintershirt", "Shirt", "Winter", "L", 4, null),
                               new DetailedClothesListingItemViewModel("113", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                               new DetailedClothesListingItemViewModel("115", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null)]
                },

                new EmployeeModel("1231", "Nickol", "Daniel", null)
                {
                    Clothes = [new DetailedClothesListingItemViewModel("111", "Winterhose", "Hose", "Winter", "XL", 3, null),
                               new DetailedClothesListingItemViewModel("112", "Sommershirt", "Shirt", "Sommer", "L", 4, null),
                               new DetailedClothesListingItemViewModel("114", "Wintershirt", "Shirt", "Winter", "L", 4, null),
                               new DetailedClothesListingItemViewModel("113", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                               new DetailedClothesListingItemViewModel("115", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null)]
                },

                new EmployeeModel("1132", "Yüksel", "Irfan", null)
                {
                    Clothes = [new DetailedClothesListingItemViewModel("111", "Winterhose", "Hose", "Winter", "XL", 3, null),
                               new DetailedClothesListingItemViewModel("112", "Sommershirt", "Shirt", "Sommer", "L", 4, null),
                               new DetailedClothesListingItemViewModel("114", "Wintershirt", "Shirt", "Winter", "L", 4, null),
                               new DetailedClothesListingItemViewModel("113", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                               new DetailedClothesListingItemViewModel("115", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null)]
                },

                new EmployeeModel("1221", "Killen", "Stefan", null)
                {
                    Clothes = [new DetailedClothesListingItemViewModel("111", "Winterhose", "Hose", "Winter", "XL", 3, null),
                               new DetailedClothesListingItemViewModel("112", "Sommershirt", "Shirt", "Sommer", "L", 4, null),
                               new DetailedClothesListingItemViewModel("114", "Wintershirt", "Shirt", "Winter", "L", 4, null),
                               new DetailedClothesListingItemViewModel("113", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                               new DetailedClothesListingItemViewModel("115", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null)]
                }

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
