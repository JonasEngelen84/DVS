namespace DVS.Domain.Models
{
    public class AvailableClothesSizeItem : ModelBase
    {
        public ClothesSize ClothesSize { get; private set; }
        public string ClothesId => ClothesSize.Clothes.Id;
        public string ClothesName => ClothesSize.Clothes.Name;
        public string Category => ClothesSize.Clothes.Category.Name;
        public string Season => ClothesSize.Clothes.Season.Name;
        public string Size => ClothesSize.Size.Size;
        public string Comment => ClothesSize.Comment;

        private int _quantity;
        public int Quantity
        {
            get => _quantity;

            set
            {
                _quantity = value;

                OnPropertyChanged(nameof(Quantity));
            }
        }

        public AvailableClothesSizeItem(ClothesSize clothesSize)
        {
            ClothesSize = clothesSize;
            _quantity = ClothesSize.Quantity;
        }

        public void Update(ClothesSize clothesSize)
        {
            ClothesSize = clothesSize;
            _quantity = clothesSize.Quantity;

            OnPropertyChanged(nameof(ClothesId));
            OnPropertyChanged(nameof(ClothesName));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Season));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }
    }
}
