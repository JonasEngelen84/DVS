namespace DVS.ViewModels.Views
{
    public class DVSHeadViewModel(DVSListingViewModel dVSListingViewModel) : ViewModelBase
    {
        public DVSListingViewModel DVSClothesListingViewModel { get; } = dVSListingViewModel;
    }
}
