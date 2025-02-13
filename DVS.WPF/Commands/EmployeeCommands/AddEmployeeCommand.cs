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
                                    EmployeeClothesSizeStore employeeClothesSizeStore,
                                    ModalNavigationStore modalNavigationStore,
                                    DVSListingViewModel dVSListingViewModel)
                                    : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizeStore = employeeClothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            if (CheckEmployeeId(addEmployeeFormViewModel) != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addEmployeeFormViewModel.HasError = false;
                addEmployeeFormViewModel.IsSubmitting = true;

                await UpdateClothesSizes(addEmployeeFormViewModel);
                Employee newEmployee = CreateNewEmployee(addEmployeeFormViewModel);

                if (addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList != null)
                    CreateEmployeeClothesSizes(newEmployee, addEmployeeFormViewModel);

                await AddEmployeeToDB(newEmployee, addEmployeeFormViewModel);

                addEmployeeFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }

        private Employee CheckEmployeeId(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee? existingEmployeeId = _employeeStore.Employees
                .FirstOrDefault(e => e.Id == addEmployeeFormViewModel.Id);

            return existingEmployeeId;
        }
        
        private async Task UpdateClothesSizes(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            List<Guid> EditedClothes = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllEditedClothes();

            foreach (Guid clothesSizeGuidId in EditedClothes)
            {
                ClothesSize? existingClothesSize = _clothesSizeStore.ClothesSizes
                    .FirstOrDefault(cs => cs.GuidId == clothesSizeGuidId);

                if (existingClothesSize != null)
                {
                    AvailableClothesSizeItem? targetItem = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.AvailableClothesSizes
                        .FirstOrDefault(acsi => acsi.ClothesSize.GuidId == clothesSizeGuidId);

                    ClothesSize editedClothesSize = new(existingClothesSize.GuidId,
                                                        existingClothesSize.Clothes,
                                                        existingClothesSize.Size,
                                                        targetItem.Quantity,
                                                        existingClothesSize.Comment)
                    {
                        EmployeeClothesSizes = []
                    };

                    try
                    {
                        await _clothesSizeStore.Update(editedClothesSize);
                    }
                    catch
                    {
                        ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand, UpdateClothesSizes");
                        addEmployeeFormViewModel.HasError = true;
                    }
                }
            }
        }

        private static Employee CreateNewEmployee(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee newEmployee = new(addEmployeeFormViewModel.Id,
                                       addEmployeeFormViewModel.Lastname,
                                       addEmployeeFormViewModel.Firstname,
                                       addEmployeeFormViewModel.Comment)
            {
                Clothes = []
            };

            return newEmployee;
        }

        private static void CreateEmployeeClothesSizes(Employee newEmployee, AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (AvailableClothesSizeItem acsi in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), newEmployee, acsi.ClothesSize, (int)acsi.Quantity, "");
                newEmployee.Clothes.Add(employeeClothesSize);
            }
        }

        private async Task AddEmployeeToDB(Employee newEmployee, AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            try
            {
                await _employeeStore.Add(newEmployee);
            }
            catch
            {
                ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand, UpdateClothesSizes");
                addEmployeeFormViewModel.HasError = true;
            }
        }

    }
}
