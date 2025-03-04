namespace DVS.Domain.Models
{
    public class AvailableClothesSizeItem(ClothesSize clothesSize) : ModelBase
    {
        public ClothesSize ClothesSize { get; private set; } = clothesSize;
        public Guid ClothesSizeId => ClothesSize.GuidId;
        public string ClothesId => ClothesSize.Clothes.Id;
        public string ClothesName => ClothesSize.Clothes.Name;
        public string Category => ClothesSize.Clothes.Category.Name;
        public string Season => ClothesSize.Clothes.Season.Name;
        public string Size => ClothesSize.Size;
        public string Comment => ClothesSize.Comment;

        private int _quantity;
        public int Quantity
        {
            get => _quantity;

            set
            {
                if (_quantity != value)
                    _quantity = value;

                OnPropertyChanged(nameof(Quantity));
            }
        }
    }
}
