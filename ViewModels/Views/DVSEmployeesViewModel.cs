namespace DVS.ViewModels.Views
{
    public class DVSEmployeesViewModel : ViewModelBase
    {
        public DVSEmployeesListingViewModel DVSEmployeesListingViewModel { get; }

        public DVSEmployeesViewModel()
        {
            DVSEmployeesListingViewModel = new DVSEmployeesListingViewModel();
        }

        
    }
}
