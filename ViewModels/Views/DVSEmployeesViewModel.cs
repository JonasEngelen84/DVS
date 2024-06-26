using DVS.Stores;

namespace DVS.ViewModels.Views
{
    public class DVSEmployeesViewModel : ViewModelBase
    {
        public DVSListingViewModel DVSClothesListingViewModel { get; }
        public DVSListingViewModel DVSEmployeesListingViewModel { get; }

        public DVSEmployeesViewModel(EmployeeStore employeeStore,
                                     ClothesStore clothesStore)
        {
            DVSClothesListingViewModel = new(clothesStore, employeeStore);
            DVSEmployeesListingViewModel = new(clothesStore, employeeStore);
        }

        
    }
}
