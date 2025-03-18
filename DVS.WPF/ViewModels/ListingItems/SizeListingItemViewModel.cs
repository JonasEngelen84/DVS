namespace DVS.WPF.ViewModels.ListingItems
{
    public class SizeListingItemViewModel(string size) : ViewModelBase
    {
        public string Size { get; } = size;

        public bool IsChecked { get; set; } = false;

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
    }
}
