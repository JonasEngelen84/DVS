namespace DVS.Models
{
    public class DetailedEmployeeListingItemModel : ModelBase
    {
        public EmployeeModel Employee { get; private set; }
        public string ID => Employee.ID;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;
        public Guid? ClothesGuidID { get; }
        public string ClothesID => Employee.Clothes.FirstOrDefault(s => s.GuidID == ClothesGuidID)?.ID ?? null;
        public string ClothesName => Employee.Clothes.FirstOrDefault(s => s.GuidID == ClothesGuidID)?.Name ?? null;
        public string Size { get; }

        public int? Quantity => Employee.Clothes.FirstOrDefault(c => c.GuidID == ClothesGuidID)?.Sizes
            .FirstOrDefault(s => s.Size == Size)?.Quantity ?? null;

        public string Comment => Employee.Clothes.FirstOrDefault(s => s.GuidID == ClothesGuidID).Sizes
            .FirstOrDefault(s => s.Size == Size)?.Comment ?? null;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                if (value != _isDeleting)
                {
                    _isDeleting = value;
                    OnPropertyChanged(nameof(IsDeleting));
                }
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value != _errorMessage)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(HasErrorMessage));
                }
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);


        public DetailedEmployeeListingItemModel(EmployeeModel employee, Guid? clothesGuidID, string size)
        {
            Employee = employee;
            ClothesGuidID = clothesGuidID;
            Size = size;
        }
    }
}
