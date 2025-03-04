using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class EditEmployeeCommand(
        EditEmployeeViewModel editEmployeeViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        private readonly List<ClothesSize> editedClothesSizesList = [];

        public override async Task ExecuteAsync(object parameter)
        {
            EditEmployeeFormViewModel editEmployeeFormViewModel = editEmployeeViewModel.EditEmployeeFormViewModel;

            if (CheckEmployeeId(editEmployeeFormViewModel) != null)
            {
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
                return;
            }

            if (Confirm($"Soll der/die Mitarbeiter/in  \"{editEmployeeFormViewModel.Employee.Lastname}\", \"{editEmployeeFormViewModel.Employee.Firstname}\"" +
                    "  bearbeiten werden?", "Mitarbeiter bearbeiten"))
            {
                editEmployeeFormViewModel.HasError = false;
                editEmployeeFormViewModel.IsSubmitting = true;


                await UpdateClothesSizes(editEmployeeFormViewModel);
                await UpdateClothes(editedClothesSizesList, editEmployeeFormViewModel);
                Employee editedEmployee = EditEmployee(editEmployeeFormViewModel);
                await UpdateEmployee(editedEmployee, editEmployeeFormViewModel);
                UpdateEmployeeClothesSizes(editedEmployee);

                editEmployeeFormViewModel.IsSubmitting = false;

                modalNavigationStore.Close();
            }
        }

        private Employee CheckEmployeeId(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            Employee? existingEmployeeId = employeeStore.Employees
                .FirstOrDefault(e => e.Id == editEmployeeFormViewModel.Id);

            return existingEmployeeId;
        }

        private async Task UpdateClothesSizes(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            List<AvailableClothesSizeItem> ClothesSizesToEdit = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesSizesToEdit();

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
                        ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!", "Mitarbeiter bearbeiten");
                        editEmployeeFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task UpdateClothes(List<ClothesSize> editedClothesSizesList, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            List<Clothes> clothesToEdited = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesToEdit();

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
                        ClothesSize existingClothesSize = editedClothes.Sizes.First(cs => cs.GuidId == editedClothesSize.GuidId);

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
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private static Employee EditEmployee(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            Employee editedEmployee = new(editEmployeeFormViewModel.Id,
                                          editEmployeeFormViewModel.Lastname,
                                          editEmployeeFormViewModel.Firstname,
                                          editEmployeeFormViewModel.Comment)
            {
                Clothes = []
            };

            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize employeeClothesSize = new(ecslivm.EmployeeClothesSizeGuidId ?? Guid.NewGuid(), editedEmployee, ecslivm.ClothesSize, ecslivm.Quantity, ecslivm.Comment);
                editedEmployee.Clothes.Add(employeeClothesSize);
            }

            return editedEmployee;
        }

        private async Task UpdateEmployee(Employee editedEmployee, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            try
            {
                await employeeStore.Delete(editEmployeeFormViewModel.Employee.Id);
                await employeeStore.Add(editedEmployee);
            }
            catch
            {
                ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!", "Mitarbeiter bearbeiten");
                editEmployeeFormViewModel.HasError = true;
            }
        }

        private void UpdateEmployeeClothesSizes(Employee editedEmployee)
        {
            foreach (EmployeeClothesSize ecs in editedEmployee.Clothes)
            {
                employeeClothesSizesStore.Update(ecs);
            }
        }
    }
}
