namespace DVS.ViewModels
{
    public class ClothesSizeViewModel(string size) : ViewModelBase
    {
        public string Size { get; } = size;

        private int? _quantity = 0;
        public int? Quantity
        {
            get => _quantity;
            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
    }
}
