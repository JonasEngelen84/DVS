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
        EmployeeClothesSizeStore employeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddEmployeeFormViewModel addEmployeeFormViewModel = addEmployeeViewModel.AddEmployeeFormViewModel;

            if (CheckEmployeeId(addEmployeeFormViewModel) != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addEmployeeFormViewModel.HasError = false;
                addEmployeeFormViewModel.IsSubmitting = true;

                List<ClothesSize> editedClothesSizesList = [];

                await UpdateClothesSizes(editedClothesSizesList, addEmployeeFormViewModel);
                await UpdateClothes(editedClothesSizesList, addEmployeeFormViewModel);
                Employee newEmployee = CreateNewEmployee(addEmployeeFormViewModel);
                await AddEmployee(newEmployee, addEmployeeFormViewModel);
                AddEmployeeClothesSizeToStore(newEmployee, employeeClothesSizeStore);

                addEmployeeFormViewModel.IsSubmitting = false;
                modalNavigationStore.Close();
            }
        }

        private Employee CheckEmployeeId(AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee? existingEmployeeId = employeeStore.Employees
                .FirstOrDefault(e => e.Id == addEmployeeFormViewModel.Id);

            return existingEmployeeId;
        }

        private async Task UpdateClothesSizes(List<ClothesSize> editedClothesSizesList, AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            List<AvailableClothesSizeItem> ClothesSizesToEdit = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesSizesToEdit();

            foreach (AvailableClothesSizeItem acsi in ClothesSizesToEdit)
            {
                ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes.First(cs => cs.GuidId == acsi.ClothesSizeId);

                if (existingClothesSize != null)
                {
                    AvailableClothesSizeItem existingAcsi = ClothesSizesToEdit.First(acsi => acsi.ClothesSize.GuidId == existingClothesSize.GuidId);

                    ClothesSize editedClothesSize = new(existingClothesSize.GuidId,
                                                        existingClothesSize.Clothes,
                                                        existingClothesSize.Size,
                                                        existingAcsi.Quantity,
                                                        existingClothesSize.Comment)
                    {
                        EmployeeClothesSizes = []
                    };

                    editedClothesSizesList.Add(editedClothesSize);

                    try
                    {
                        await clothesSizeStore.Update(editedClothesSize);
                    }
                    catch
                    {
                        ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand, UpdateClothesSizes");
                        addEmployeeFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task UpdateClothes(List<ClothesSize> editedClothesSizesList, AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            List<Clothes> clothesToEdited = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesToEdit();

            foreach (Clothes clothes in clothesToEdited)
            {
                Clothes editedClothes = new(
                    clothes.Id,
                    clothes.Name,
                    clothes.Category,
                    clothes.Season,
                    clothes.Comment)
                {
                    Sizes = clothes.Sizes
                };

                foreach (ClothesSize editedClothesSize in editedClothesSizesList)
                {
                    if (editedClothesSize.ClothesId == clothes.Id)
                    {                        
                        ClothesSize existingClothesSize = editedClothes.Sizes .First(cs => cs.GuidId == editedClothesSize.GuidId);

                        if (existingClothesSize != null)
                        {
                            editedClothes.Sizes.Remove(existingClothesSize);
                            editedClothes.Sizes.Add(editedClothesSize);
                        }
                    }
                }
                
                try
                {
                    await clothesStore.Update(editedClothes);
                }
                catch
                {
                    ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand, UpdateClothes");
                    addEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private static Employee CreateNewEmployee(AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee newEmployee = new(addEmployeeFormViewModel.Id,
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

        private async Task AddEmployee(Employee newEmployee, AddEmployeeFormViewModel addEmployeeFormViewModel)
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

        private static void AddEmployeeClothesSizeToStore(Employee newEmployee, EmployeeClothesSizeStore employeeClothesSizeStore)
        {
            foreach(EmployeeClothesSize ecs in newEmployee.Clothes)
            {
                employeeClothesSizeStore.AddToStore(ecs);
            }
        }
    }
}
