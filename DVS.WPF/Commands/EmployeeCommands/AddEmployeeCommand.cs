using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class AddEmployeeCommand(
        AddEmployeeViewModel addEmployeeViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {        
        public override async Task ExecuteAsync(object parameter)
        {
            AddEmployeeFormViewModel addEmployeeFormViewModel = addEmployeeViewModel.AddEmployeeFormViewModel;
            addEmployeeFormViewModel.HasError = false;

            if (employeeStore.Employees.Any(e => e.Id == addEmployeeFormViewModel.Id))
            {
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
                return;
            }

            addEmployeeFormViewModel.IsSubmitting = true;
            List<ClothesSize> editedClothesSizesList = [];

            UpdateClothesSizes(editedClothesSizesList, addEmployeeFormViewModel);
            UpdateClothes(editedClothesSizesList, addEmployeeFormViewModel);
            Employee newEmployee = CreateNewEmployee(addEmployeeFormViewModel);
            await AddEmployeeAsync(newEmployee, addEmployeeFormViewModel);
            AddEmployeeClothesSizeToStore(newEmployee);

            addEmployeeFormViewModel.IsSubmitting = false;
            modalNavigationStore.Close();
        }

        private void UpdateClothesSizes(List<ClothesSize> editedClothesSizesList, AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            List<AvailableClothesSizeItem> ClothesSizesToEdit = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesSizesToEdit();

            foreach (AvailableClothesSizeItem acsi in ClothesSizesToEdit)
            {
                acsi.ClothesSize.Quantity = acsi.Quantity;
                acsi.ClothesSize.IsDirty = true;
                editedClothesSizesList.Add(acsi.ClothesSize);
                clothesSizeStore.Update(acsi.ClothesSize);
            }
        }

        private void UpdateClothes(List<ClothesSize> editedClothesSizesList, AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            List<Clothes> clothesToEdit = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesToEdit();

            foreach (Clothes clothes in clothesToEdit)
            {
                foreach (ClothesSize editedClothesSize in editedClothesSizesList)
                {
                    if (editedClothesSize.ClothesId == clothes.Id)
                    {
                        ClothesSize existingClothesSize = clothes.Sizes.First(cs => cs.Id == editedClothesSize.Id);

                        if (existingClothesSize != null)
                        {
                            clothes.Sizes.Remove(existingClothesSize);
                            clothes.Sizes.Add(editedClothesSize);
                        }
                    }
                }

                clothesStore.Update(clothes);
            }
        }

        private static Employee CreateNewEmployee(AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee newEmployee = new(
                addEmployeeFormViewModel.Id,
                addEmployeeFormViewModel.Lastname,
                addEmployeeFormViewModel.Firstname,
                addEmployeeFormViewModel.Comment)
            {
                Clothes = []
            };

            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), newEmployee, ecslivm.ClothesSize, ecslivm.Quantity, ecslivm.Comment);
                newEmployee.Clothes.Add(employeeClothesSize);
            }

            return newEmployee;
        }

        private async Task AddEmployeeAsync(Employee newEmployee, AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            try
            {
                await employeeStore.Add(newEmployee);
            }
            catch
            {
                ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand, UpdateClothesSizes");
                addEmployeeFormViewModel.HasError = true;
            }
        }

        private void AddEmployeeClothesSizeToStore(Employee newEmployee)
        {
            foreach(EmployeeClothesSize ecs in newEmployee.Clothes)
            {
                employeeClothesSizesStore.AddStore(ecs);
            }
        }
    }
}
