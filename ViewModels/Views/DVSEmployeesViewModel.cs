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
            DVSClothesListingViewModel.Load();
            DVSEmployeesListingViewModel = new(clothesStore, employeeStore);
            DVSEmployeesListingViewModel.Load();
        }

        
    }
}
