using DVS.Stores;

namespace DVS.ViewModels.Views
{
    public class DVSEmployeesViewModel : ViewModelBase
    {
        public DVSEmployeesListingViewModel DVSEmployeesListingViewModel { get; }
        public DVSClothesListingViewModel DVSClothesListingViewModel { get; }

        public DVSEmployeesViewModel(EmployeeStore employeeStore,
                                     ClothesStore clothesStore)
        {
            DVSEmployeesListingViewModel = new(employeeStore);
            DVSClothesListingViewModel = new(clothesStore);
        }

        
    }
}
