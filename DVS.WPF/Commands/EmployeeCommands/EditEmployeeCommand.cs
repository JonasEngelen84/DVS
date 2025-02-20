using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
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
            AddEditEmployeeFormViewModel editEmployeeFormViewModel = editEmployeeViewModel.AddEditEmployeeFormViewModel;
            
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

        private static Employee CreateUpdatedEmployee(AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            return new Employee(editEmployeeFormViewModel.Id,
                                editEmployeeFormViewModel.Lastname,
                                editEmployeeFormViewModel.Firstname,
                                editEmployeeFormViewModel.Comment)
            {
                Clothes = new ObservableCollection<EmployeeClothesSize>(editEmployeeFormViewModel.Employee.Clothes)
            };
        }

        private async Task DeleteRemovedEmployeeClothesSizesAsync(Employee updatedEmployee, AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSize ecs in updatedEmployee.Clothes)
            {
                EmployeeClothesSizeItem existingItem = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList
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

        private async Task CreateAndAddNewEmployeeClothesSizesAsync(Employee updatedEmployee, AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeItem ecsi in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize existingEcs = updatedEmployee.Clothes.First(ecs => ecs.ClothesSizeGuidId == ecsi.ClothesSize.GuidId);

                EmployeeClothesSize newEcs = new(Guid.NewGuid(), updatedEmployee, ecsi.ClothesSize, (int)ecsi.Quantity, null);

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

        private async Task UpdateEmployeeClothesSizesAsync(Employee updatedEmployee, AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeItem ecsi in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize existingEcsi = updatedEmployee.Clothes.First(ecs => ecs.ClothesSizeGuidId == ecsi.ClothesSize.GuidId);
                EmployeeClothesSize UpdatedEmployeeClothesSize = new(existingEcsi.GuidId, updatedEmployee, ecsi.ClothesSize, (int)ecsi.Quantity, null);
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

        private async Task UpdateEmployeeAsync(Employee updatedEmployee, AddEditEmployeeFormViewModel editEmployeeFormViewModel)
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

        private async Task UpdateClothesSizeAsync(AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeItem ecsi in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                ClothesSize csToRemove = ecsi.ClothesSize.Clothes.Sizes.First(cs => cs.GuidId == ecsi.ClothesSize.GuidId);

                ecsi.ClothesSize.Clothes.Sizes.Remove(csToRemove);
                ecsi.ClothesSize.Clothes.Sizes.Add(ecsi.ClothesSize);

                try
                {
                    await clothesSizeStore.Update(ecsi.ClothesSize);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateClothesAsync(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeItem ecsi in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                Clothes ClothesToRemove = ecsi.ClothesSize.Clothes.Category.Clothes.First(c => c.Id == ecsi.ClothesSize.Clothes.Id);

                if (ClothesToRemove != null)
                {
                    ecsi.ClothesSize.Clothes.Category.Clothes.Remove(ClothesToRemove);
                    ecsi.ClothesSize.Clothes.Category.Clothes.Add(ecsi.ClothesSize.Clothes);
                }

                ClothesToRemove = ecsi.ClothesSize.Clothes.Season.Clothes.FirstOrDefault(c => c.Id == ecsi.ClothesSize.Clothes.Id);

                if (ClothesToRemove != null)
                {
                    ecsi.ClothesSize.Clothes.Season.Clothes.Remove(ClothesToRemove);
                    ecsi.ClothesSize.Clothes.Season.Clothes.Add(ecsi.ClothesSize.Clothes);
                }

                try
                {
                    await clothesStore.Update(ecsi.ClothesSize.Clothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    addEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateCategoryAsync(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeItem acsi in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                try
                {
                    await categoryStore.Update(acsi.ClothesSize.Clothes.Category, null);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    addEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateSeasonAsync(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeItem ecsi in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                try
                {
                    await seasonStore.Update(ecsi.ClothesSize.Clothes.Season, null);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    addEmployeeFormViewModel.HasError = true;
                }
                finally
                {
                    addEmployeeFormViewModel.IsSubmitting = false;
                }
            }
        }
    }
}
