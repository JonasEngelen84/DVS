using DVS.Models;

namespace DVS.ViewModels.ListViewItems
{
    public class EmployeeClothesListViewItemViewModel : ViewModelBase
    {
        public EmployeeModel EmployeeModel { get; private set; }

        public EmployeeClothesListViewItemViewModel(EmployeeModel employeeModel)
        {
            EmployeeModel = employeeModel;
        }
        
        public int EmployeeId => EmployeeModel.Id;
        public string Lastname => EmployeeModel.Lastname;
        public string Firstname => EmployeeModel.Firstname;
        public int ClothesId;
        public string ClothesName;
        public string Size;
        public string Quantity;
        public string Comment;

    }
}
