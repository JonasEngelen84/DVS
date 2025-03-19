namespace DVS.Domain.Models
{
    public class EmployeeClothesSize : ObservableEntity
    {
        public Guid Id { get; set; }
        public Guid ClothesSizeGuidId { get; set; }
        public string EmployeeId { get; set; }

        private Employee _employee;
        public Employee Employee
        {
            get => _employee;

            set
            {
                if (_employee != value)
                {
                    _employee = value;
                }
            } 
        }

        private ClothesSize _clothesSize;
        public ClothesSize ClothesSize
        {
            get => _clothesSize;

            set
            {
                if (_clothesSize != value)
                {
                    _clothesSize = value;
                }
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
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
                }
            }
        }

        public EmployeeClothesSize(
            Guid id,
            Employee employee,
            ClothesSize clothesSize,
            int quantity,
            string comment)
        {
            Id = id;
            Employee = employee;
            EmployeeId = employee.Id;
            _clothesSize = clothesSize;
            ClothesSizeGuidId = clothesSize.Id;
            Quantity = quantity;
            Comment = comment;
        }

        public EmployeeClothesSize() { }
    }
}
