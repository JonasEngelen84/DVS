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
                new EmployeeModel(Guid.NewGuid(), "1324", "Engelen", "Jonas", "Vertrag läuft aus")
                {
                    Clothes = [new DetailedClothesListingItemModel(new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null), "50"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null), "44"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null), "45")]
                },

                new EmployeeModel(Guid.NewGuid(), "1212", "Molik", "Nadine", null)
                {
                    Clothes = [new DetailedClothesListingItemModel(new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null), "50"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null), "44"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null), "45")]
                },

                new EmployeeModel(Guid.NewGuid(), "1112", "Yüksel", "Kemal", null)
                {
                    Clothes = [new DetailedClothesListingItemModel(new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null), "50"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null), "44"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null), "45")]
                },

                new EmployeeModel(Guid.NewGuid(), "1213", "Oetken", "Markus", null)
                {
                    Clothes = [new DetailedClothesListingItemModel(new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null), "50"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null), "44"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null), "45")]
                },

                new EmployeeModel(Guid.NewGuid(), "1231", "Nickol", "Daniel", null)
                {
                    Clothes = [new DetailedClothesListingItemModel(new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null), "50"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null), "44"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null), "45")]
                },

                new EmployeeModel(Guid.NewGuid(), "1132", "Yüksel", "Irfan", null)
                {
                    Clothes = [new DetailedClothesListingItemModel(new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null), "50"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null), "44"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null), "45")]
                },

                new EmployeeModel(Guid.NewGuid(), "1221", "Killen", "Stefan", null)
                {
                    Clothes = [new DetailedClothesListingItemModel(new(Guid.NewGuid(), "115", "Schuhe", new(Guid.NewGuid(), "Schuhwerk"), new(Guid.NewGuid(), "Saisonlos"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "113", "Regenjacke", new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Saisonlos"), null), "50"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "112", "Sommershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Sommer"), null), "52"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "114", "Wintershirt", new(Guid.NewGuid(), "Shirt"), new(Guid.NewGuid(), "Winter"), null), "44"),
                        new DetailedClothesListingItemModel(new(Guid.NewGuid(), "111", "Winterhose", new(Guid.NewGuid(), "Hose"), new(Guid.NewGuid(), "Winter"), null), "45")]
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
