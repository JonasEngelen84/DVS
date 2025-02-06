using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel,
                                    EmployeeStore employeeStore,
                                    ClothesSizeStore clothesSizeStore,
                                    ModalNavigationStore modalNavigationStore,
                                    DVSListingViewModel dVSListingViewModel)
                                    : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            if (ExistingEmployeeId(addEmployeeFormViewModel) != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addEmployeeFormViewModel.HasError = false;
                addEmployeeFormViewModel.IsSubmitting = true;

                Employee newEmployee = CreateNewEmployee(addEmployeeFormViewModel);

                await UpdateClothesSizes(newEmployee, addEmployeeFormViewModel);

                //await UpdateClothes(addEmployeeFormViewModel);
                
                addEmployeeFormViewModel.IsSubmitting = false;

                _modalNavigationStore.Close();
            }
        }

        private Employee ExistingEmployeeId(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee? existingEmployeeId = _employeeStore.Employees
                .FirstOrDefault(e => e.Id == addEmployeeFormViewModel.Id);

            return existingEmployeeId;
        }

        private static Employee CreateNewEmployee(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee newEmployee = new(Guid.NewGuid(),
                                       addEmployeeFormViewModel.Id,
                                       addEmployeeFormViewModel.Lastname,
                                       addEmployeeFormViewModel.Firstname,
                                       addEmployeeFormViewModel.Comment);

            if (addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList != null)
            {
                foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
                {
                    EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), newEmployee, dclivm.ClothesSize, (int)dclivm.Quantity, "");
                    newEmployee.Clothes.Add(employeeClothesSize);
                }
            }

            return newEmployee;
        }

        private async Task UpdateClothesSizes(Employee newEmployee, AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            List<Guid> EditedClothes = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllEditedClothes();

            try
            {
                foreach (Guid clothesSizeGuidId in EditedClothes)
                {
                    ClothesSize? existingClothesSize = _clothesSizeStore.ClothesSizes
                        .FirstOrDefault(cs => cs.GuidId == clothesSizeGuidId);

                    if (existingClothesSize != null)
                    {
                        DetailedClothesListingItemViewModel? targetItem = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.AvailableClothesSizes
                            .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == clothesSizeGuidId);

                        ClothesSize editedClothesSize = new(existingClothesSize.GuidId,
                                                            existingClothesSize.Clothes,
                                                            existingClothesSize.Size,
                                                            targetItem.Quantity,
                                                            existingClothesSize.Comment)
                        {
                            EmployeeClothesSizes = []
                        };

                        await _clothesSizeStore.Update(editedClothesSize);

                        DetailedClothesListingItemViewModel? dclivm = _dVSListingViewModel.DetailedClothesListingItemCollection
                            .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidId == clothesSizeGuidId);

                        dclivm?.Update(dclivm.Clothes, editedClothesSize);
                    }
                }

                await AddNewEmployee(newEmployee, addEmployeeFormViewModel);
            }
            catch
            {
                ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand, UpdateClothesSizes");

                addEmployeeFormViewModel.HasError = true;
            }
        }

        private async Task AddNewEmployee(Employee newEmployee, AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            try
            {
                await _employeeStore.Add(newEmployee);
            }
            catch
            {
                ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand");

                addEmployeeFormViewModel.HasError = true;
            }
        }
        
        //private async Task UpdateClothes(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        //{
        //    foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
        //    {

        //    }
        //}
    }
}
