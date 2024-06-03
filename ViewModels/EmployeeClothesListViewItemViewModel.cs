using DVS.Models;

namespace DVS.ViewModels.ListViewItems
{
    public class EmployeeClothesListViewItemViewModel : ViewModelBase
    {
        public EmployeeModel Employee { get; private set; }
        public ClothesModel Clothes { get; private set; }

        public int EmployeeId => Employee.Id;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;
        public int ClothesId => Clothes.Id;
        public string ClothesName => Clothes.Name;
        public string Size => Clothes.Size;

        private string _quantity;
        public string Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(_quantity));
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public EmployeeClothesListViewItemViewModel(EmployeeModel employee, ClothesModel clothes)
        {
            Employee = employee;
            Clothes = clothes;
        }
    }
}
