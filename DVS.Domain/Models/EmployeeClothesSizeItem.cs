namespace DVS.Domain.Models
{
    public class EmployeeClothesSizeItem : ModelBase
    {
        public EmployeeClothesSize EmployeeClothesSize { get; private set; }
        public string ClothesId => EmployeeClothesSize.ClothesSize.Clothes.Id;
        public string ClothesName => EmployeeClothesSize.ClothesSize.Clothes.Name;
        public string Size => EmployeeClothesSize.ClothesSize.Size.Size;
        public string Comment => EmployeeClothesSize.Comment;

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }

            set
            {
                if (EmployeeClothesSize != null)
                    _quantity = EmployeeClothesSize.Quantity;
                
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public EmployeeClothesSizeItem(EmployeeClothesSize employeeClothesSize)
        {
            EmployeeClothesSize = employeeClothesSize;
            _quantity = EmployeeClothesSize.Quantity;
        }

        public void Update(EmployeeClothesSize employeeClothesSize)
        {
            EmployeeClothesSize = employeeClothesSize;
            _quantity = employeeClothesSize.Quantity;
        }
    }
}
