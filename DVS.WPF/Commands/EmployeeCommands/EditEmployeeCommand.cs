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
        EmployeeClothesSizeStore employeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        private readonly List<ClothesSize> editedClothesSizesList = [];

        public override async Task ExecuteAsync(object parameter)
        {
            EditEmployeeFormViewModel editEmployeeFormViewModel = editEmployeeViewModel.EditEmployeeFormViewModel;
            editEmployeeFormViewModel.HasError = false;

            if (!Confirm($"Soll der/die Mitarbeiter/in  \"{editEmployeeFormViewModel.Lastname}\", \"{editEmployeeFormViewModel.Firstname}\"" +
                "  bearbeiten werden?", "Mitarbeiter bearbeiten"))
            {                
                return;
            }

            editEmployeeFormViewModel.IsSubmitting = true;

            List<EmployeeClothesSize> oldClothes = new(editEmployeeFormViewModel.Employee.Clothes);
            List<EmployeeClothesSizeListingItemViewModel> newClothes = new(
                editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList);

            if (oldClothes.Count > 0 && newClothes.Count > 0)
            {
                await DeleteOrUpdateEcs(oldClothes, newClothes, editEmployeeFormViewModel);
            }

            if (newClothes.Count > 0)
                await AddNewEcsAsync(newClothes, editEmployeeFormViewModel);

            UpdateClothesSizes(editEmployeeFormViewModel);
            UpdateClothes(editedClothesSizesList, editEmployeeFormViewModel);
            Employee editedEmployee = EditEmployee(editEmployeeFormViewModel);
            employeeStore.Update(editedEmployee);

            editEmployeeFormViewModel.IsSubmitting = false;
            modalNavigationStore.Close();
        }

        private async Task DeleteOrUpdateEcs(
            List<EmployeeClothesSize> oldClothes,
            List<EmployeeClothesSizeListingItemViewModel> newClothes,
            EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSize oldEcs in oldClothes)
            {
                EmployeeClothesSizeListingItemViewModel? newEcs = newClothes
                    .FirstOrDefault(ecslivm => ecslivm.EmployeeClothesSizeGuidId == oldEcs.Id);

                if (newEcs == null)
                {
                    editEmployeeFormViewModel.Employee.Clothes.Remove(oldEcs);

                    try
                    {
                        await employeeClothesSizeStore.Delete(oldEcs);
                    }
                    catch
                    {
                        ShowErrorMessageBox("Bearbeiten des/der Mitarbeiter/in ist fehlgeschlagen!", " Mitarbeiter/in bearbeiten");
                        editEmployeeFormViewModel.HasError = true;
                    }
                }
                else
                {
                    UpdateEmployeeClothesSize(oldEcs, newEcs, editEmployeeFormViewModel);
                    newClothes.Remove(newEcs);
                }
            }
        }

        private void UpdateEmployeeClothesSize(
            EmployeeClothesSize oldEcs,
            EmployeeClothesSizeListingItemViewModel newEcs,
            EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            bool ecsPropertyChanged = false;

            if (oldEcs.Quantity != newEcs.Quantity)
            {
                oldEcs.Quantity = newEcs.Quantity;
                ecsPropertyChanged = true;
            }

            if (!string.Equals(oldEcs.Comment, newEcs.Comment))
            {
                oldEcs.Comment = newEcs.Comment;
                ecsPropertyChanged = true;
            }

            if (ecsPropertyChanged)
            {
                EmployeeClothesSize existingEcs = editEmployeeFormViewModel.Employee.Clothes
                    .First(ecs => ecs.Id == oldEcs.Id);

                editEmployeeFormViewModel.Employee.Clothes.Remove(existingEcs);
                editEmployeeFormViewModel.Employee.Clothes.Add(oldEcs);
                employeeClothesSizeStore.Update(oldEcs);
            }
        }

        private async Task AddNewEcsAsync(
            List<EmployeeClothesSizeListingItemViewModel> newClothes,
            EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in newClothes)
            {
                EmployeeClothesSize newEcs = new(
                    Guid.NewGuid(),
                    editEmployeeFormViewModel.Employee,
                    ecslivm.ClothesSize,
                    ecslivm.Quantity,
                    ecslivm.Comment);

                editEmployeeFormViewModel.Employee.Clothes.Add(newEcs);

                try
                {
                    await employeeClothesSizeStore.AddDataBase(newEcs);
                }
                catch
                {
                    ShowErrorMessageBox("Bearbeiten des/der Mitarbeiter/in ist fehlgeschlagen!", " Mitarbeiter/in bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private void UpdateClothesSizes(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            List<AvailableClothesSizeItem> ClothesSizesToEdit = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesSizesToEdit();

            foreach (AvailableClothesSizeItem acsi in ClothesSizesToEdit)
            {
                ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes.First(cs => cs.Id == acsi.ClothesSizeId);

                if (existingClothesSize != null)
                {
                    AvailableClothesSizeItem existingAcsi = ClothesSizesToEdit.First(acsi => acsi.ClothesSize.Id == existingClothesSize.Id);

                    ClothesSize editedClothesSize = new(existingClothesSize.Id,
                                                        existingClothesSize.Clothes,
                                                        existingClothesSize.Size,
                                                        existingAcsi.Quantity,
                                                        existingClothesSize.Comment)
                    {
                        EmployeeClothesSizes = []
                    };

                    editedClothesSizesList.Add(editedClothesSize);

                    clothesSizeStore.Update(editedClothesSize);
                }
            }
        }

        private void UpdateClothes(List<ClothesSize> editedClothesSizesList, EditEmployeeFormViewModel editEmployeeFormViewModel)
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
                        ClothesSize existingClothesSize = editedClothes.Sizes.First(cs => cs.Id == editedClothesSize.Id);

                        if (existingClothesSize != null)
                        {
                            editedClothes.Sizes.Remove(existingClothesSize);
                            editedClothes.Sizes.Add(editedClothesSize);
                        }
                    }
                }

                clothesStore.Update(editedClothes);
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
    }
}
