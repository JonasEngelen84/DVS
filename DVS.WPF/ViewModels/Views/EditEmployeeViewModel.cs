using DVS.WPF.Commands.AddEditEmployeeCommands;
using DVS.WPF.Commands.EmployeeCommands;
using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel;
        public EditEmployeeFormViewModel EditEmployeeFormViewModel { get; }
        public ICommand CloseAddEditEmployee { get; }


        public EditEmployeeViewModel(
            Employee employee,
            EmployeeStore employeeStore,
            ClothesStore clothesStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore,
            ModalNavigationStore modalNavigationStore)
        {
            _addEditEmployeeListingViewModel = new(employee, clothesSizeStore);
            CloseAddEditEmployee = new CloseAddEditEmployeeCommand(clothesStore, modalNavigationStore);

            ICommand editEmployee = new EditEmployeeCommand(
                this,
                employeeStore,
                clothesStore,
                categoryStore,
                seasonStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                modalNavigationStore);

            EditEmployeeFormViewModel = new(employee, _addEditEmployeeListingViewModel, editEmployee)
            {
                Id = employee.Id,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Comment = employee.Comment
            };
        }
    }
}
