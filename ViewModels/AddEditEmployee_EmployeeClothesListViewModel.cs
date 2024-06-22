using DVS.Models;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class AddEditEmployee_EmployeeClothesListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<DetailedClothesListingItemModel> _employeeClothes = [];
        public IEnumerable<DetailedClothesListingItemModel> EmployeeClothes => _employeeClothes;


        public AddEditEmployee_EmployeeClothesListViewModel()
        {
            _employeeClothes = [new DetailedClothesListingItemModel("951",
                                                                    "Test",
                                                                    "Schuhe",
                                                                    "Winter",
                                                                    "46",
                                                                    1,
                                                                    "Testweise")];
        }


        public void AddClothes(DetailedClothesListingItemModel clothes)
        {
            if (clothes != null && !_employeeClothes.Contains(clothes))
            {
                _employeeClothes.Add(clothes);
                OnPropertyChanged(nameof(EmployeeClothes));
            }
        }

        public void RemoveClothes(DetailedClothesListingItemModel clothes)
        {
            if (clothes != null && _employeeClothes.Contains(clothes))
            {
                _employeeClothes.Remove(clothes);
                OnPropertyChanged(nameof(EmployeeClothes));
            }
        }
    }
}
