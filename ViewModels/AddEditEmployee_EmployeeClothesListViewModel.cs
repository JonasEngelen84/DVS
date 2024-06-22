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
    }
}
