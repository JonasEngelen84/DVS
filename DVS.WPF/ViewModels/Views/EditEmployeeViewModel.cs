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
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }
        public ICommand CloseAddEditEmployee { get; }


        public EditEmployeeViewModel(Employee employee,
                                     EmployeeStore employeeStore,
                                     ClothesStore clothesStore,
                                     SizeStore sizeStore,
                                     CategoryStore categoryStore,
                                     SeasonStore seasonStore,
                                     ClothesSizeStore clothesSizeStore,
                                     EmployeeClothesSizesStore employeeClothesSizesStore,
                                     ModalNavigationStore modalNavigationStore,
                                     AddEditEmployeeListingViewModel addEditEmployeeListingViewModel)
        {
            _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;
            CloseAddEditEmployee = new CloseAddEditEmployeeCommand(clothesStore, modalNavigationStore);

            ICommand editEmployee = new EditEmployeeCommand(this,
                                                            employeeStore,
                                                            clothesStore,
                                                            sizeStore,
                                                            categoryStore,
                                                            seasonStore,
                                                            clothesSizeStore,
                                                            employeeClothesSizesStore,
                                                            modalNavigationStore);

            AddEditEmployeeFormViewModel = new(employee, _addEditEmployeeListingViewModel, editEmployee)
            {
                Id = employee.Id,
                Lastname = employee.Lastname,
                Firstname = employee.Firstname,
                Comment = employee.Comment
            };
        }
    }
}
