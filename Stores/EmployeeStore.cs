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
            _employees = [new EmployeeModel("1324", "Engelen", "Jonas", null)
                          {
                              Clothes = [new DetailedClothesListingItemModel("666", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                                         new DetailedClothesListingItemModel("999", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null),]
                          },
                          new EmployeeModel("1212", "Molik", "Nadine", null)
                          {
                              Clothes = [new DetailedClothesListingItemModel("666", "Regenjacke", "Jacke", "Saisonlos", "XL", 1, null),
                                         new DetailedClothesListingItemModel("999", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null),
                                         new DetailedClothesListingItemModel("999", "Schuhe", "Schuhwerk", "Saisonlos", "45", 1, null),]
                          },
                          new EmployeeModel("1112", "Yüksel", "Kemal", null),
                          new EmployeeModel("1213", "Oetken", "Markus", null),
                          new EmployeeModel("1231", "Nickol", "Daniel", null),
                          new EmployeeModel("1132", "Yüksel", "Irfan", null),
                          new EmployeeModel("1221", "Killen", "Stefan", null)];
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
