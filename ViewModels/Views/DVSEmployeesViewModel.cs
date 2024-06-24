using DVS.Stores;

namespace DVS.ViewModels.Views
{
    public class DVSEmployeesViewModel : ViewModelBase
    {
        public DVSListingViewModel DVSClothesListing { get; }
        public DVSListingViewModel DVSEmployeesListing { get; }

        public DVSEmployeesViewModel(EmployeeStore employeeStore,
                                     ClothesStore clothesStore)
        {
            DVSClothesListing = new(clothesStore, employeeStore);
            DVSEmployeesListing = new(clothesStore, employeeStore);
        }

        
    }
}
