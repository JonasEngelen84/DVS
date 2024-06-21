namespace DVS.ViewModels.Views
{
    public class DVSEmployeesViewModel : ViewModelBase
    {
        public DVSEmployeesListingViewModel DVSEmployeesListingViewModel { get; }
        public DVSClothesListingViewModel DVSClothesListingViewModel { get; }

        public DVSEmployeesViewModel()
        {
            DVSEmployeesListingViewModel = new DVSEmployeesListingViewModel();
            DVSClothesListingViewModel = new DVSClothesListingViewModel();
        }

        
    }
}
