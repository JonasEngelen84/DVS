using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class EditEmployeeCommand(
        EditEmployeeViewModel editEmployeeViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            EditEmployeeFormViewModel editEmployeeFormViewModel = editEmployeeViewModel.EditEmployeeFormViewModel;
            
            if (Confirm($"Soll der/die Mitarbeiter/in  \"{editEmployeeFormViewModel.Lastname}\", \"{editEmployeeFormViewModel.Firstname}\"  bearbeiten werden?", "Mitarbeiter bearbeiten"))
            {
                editEmployeeFormViewModel.HasError = false;
                editEmployeeFormViewModel.IsSubmitting = true;

                Employee updatedEmployee = CreateUpdatedEmployee(editEmployeeFormViewModel);

                await DeleteRemovedEmployeeClothesSizesAsync(updatedEmployee, editEmployeeFormViewModel);
                await CreateAndAddNewEmployeeClothesSizesAsync(updatedEmployee, editEmployeeFormViewModel);
                await UpdateEmployeeClothesSizesAsync(updatedEmployee, editEmployeeFormViewModel);
                await UpdateEmployeeAsync(updatedEmployee, editEmployeeFormViewModel);
                await UpdateClothesSizeAsync(editEmployeeFormViewModel);
                await UpdateClothesAsync(editEmployeeFormViewModel);
                await UpdateCategoryAsync(editEmployeeFormViewModel);
                await UpdateSeasonAsync(editEmployeeFormViewModel);

                editEmployeeFormViewModel.IsSubmitting = false;

                modalNavigationStore.Close();
            }
        }

        private static Employee CreateUpdatedEmployee(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            return new Employee(editEmployeeFormViewModel.Id,
                                editEmployeeFormViewModel.Lastname,
                                editEmployeeFormViewModel.Firstname,
                                editEmployeeFormViewModel.Comment)
            {
                Clothes = new ObservableCollection<EmployeeClothesSize>(editEmployeeFormViewModel.Employee.Clothes)
            };
        }

        private async Task DeleteRemovedEmployeeClothesSizesAsync(Employee updatedEmployee, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSize ecs in updatedEmployee.Clothes)
            {
                EmployeeClothesSizeListingItemViewModel existingEcslivm = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList
                    .First(acsi => acsi.ClothesSize.GuidId == ecs.ClothesSizeGuidId);

                updatedEmployee.Clothes.Remove(ecs);
                ecs.ClothesSize.EmployeeClothesSizes.Remove(ecs);

                try
                {
                    await employeeClothesSizesStore.Delete(ecs);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task CreateAndAddNewEmployeeClothesSizesAsync(Employee updatedEmployee, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize existingEcs = updatedEmployee.Clothes.First(ecs => ecs.ClothesSizeGuidId == ecslivm.ClothesSize.GuidId);

                EmployeeClothesSize newEcs = new(Guid.NewGuid(), updatedEmployee, ecslivm.ClothesSize, (int)ecslivm.Quantity, null);

                updatedEmployee.Clothes.Add(newEcs);
                //dclivm.ClothesSize.EmployeeClothesSizes.Add(employeeClothesSize);

                try
                {
                    await employeeClothesSizesStore.Add(newEcs);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateEmployeeClothesSizesAsync(Employee updatedEmployee, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize existingEcsi = updatedEmployee.Clothes.First(ecs => ecs.ClothesSizeGuidId == ecslivm.ClothesSize.GuidId);
                EmployeeClothesSize UpdatedEmployeeClothesSize = new(existingEcsi.GuidId, updatedEmployee, ecslivm.ClothesSize, (int)ecslivm.Quantity, null);
                EmployeeClothesSize itemToRemove = updatedEmployee.Clothes.First(ecs => ecs.GuidId == UpdatedEmployeeClothesSize.GuidId);

                updatedEmployee.Clothes.Remove(itemToRemove);
                updatedEmployee.Clothes.Add(UpdatedEmployeeClothesSize);

                itemToRemove = UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.First(ecs => ecs.GuidId == UpdatedEmployeeClothesSize.GuidId);

                UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Remove(itemToRemove);
                UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Add(UpdatedEmployeeClothesSize);

                try
                {
                    await employeeClothesSizesStore.Update(UpdatedEmployeeClothesSize);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateEmployeeAsync(Employee updatedEmployee, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            try
            {
                await employeeStore.Update(updatedEmployee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                editEmployeeFormViewModel.HasError = true;
            }
        }

        private async Task UpdateClothesSizeAsync(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                ClothesSize csToRemove = ecslivm.ClothesSize.Clothes.Sizes.First(cs => cs.GuidId == ecslivm.ClothesSize.GuidId);

                ecslivm.ClothesSize.Clothes.Sizes.Remove(csToRemove);
                ecslivm.ClothesSize.Clothes.Sizes.Add(ecslivm.ClothesSize);

                try
                {
                    await clothesSizeStore.Update(ecslivm.ClothesSize);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateClothesAsync(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                Clothes ClothesToRemove = ecslivm.ClothesSize.Clothes.Category.Clothes.First(c => c.Id == ecslivm.ClothesSize.Clothes.Id);

                if (ClothesToRemove != null)
                {
                    ecslivm.ClothesSize.Clothes.Category.Clothes.Remove(ClothesToRemove);
                    ecslivm.ClothesSize.Clothes.Category.Clothes.Add(ecslivm.ClothesSize.Clothes);
                }

                ClothesToRemove = ecslivm.ClothesSize.Clothes.Season.Clothes.FirstOrDefault(c => c.Id == ecslivm.ClothesSize.Clothes.Id);

                if (ClothesToRemove != null)
                {
                    ecslivm.ClothesSize.Clothes.Season.Clothes.Remove(ClothesToRemove);
                    ecslivm.ClothesSize.Clothes.Season.Clothes.Add(ecslivm.ClothesSize.Clothes);
                }

                try
                {
                    await clothesStore.Update(ecslivm.ClothesSize.Clothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateCategoryAsync(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                try
                {
                    await categoryStore.Update(ecslivm.ClothesSize.Clothes.Category, null);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateSeasonAsync(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                try
                {
                    await seasonStore.Update(ecslivm.ClothesSize.Clothes.Season, null);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
                finally
                {
                    editEmployeeFormViewModel.IsSubmitting = false;
                }
            }
        }
    }
}
