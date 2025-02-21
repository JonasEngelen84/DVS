namespace DVS.Domain.Models
{
    public class EmployeeClothesSizeItem(ClothesSize clothesSize) : ModelBase
    {
        public ClothesSize ClothesSize { get; } = clothesSize;
        public string ClothesId => ClothesSize.Clothes.Id;
        public string ClothesName => ClothesSize.Clothes.Name;
        public string Size => ClothesSize.Size;

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }

            set
            {
                if (_quantity != value)
                    _quantity = value;
                
                OnPropertyChanged(nameof(Quantity));
            }
        }
    }
}
