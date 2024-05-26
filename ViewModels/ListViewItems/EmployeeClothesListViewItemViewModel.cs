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

        private string quantity;
        public string Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(quantity));
            }
        }

        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
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
