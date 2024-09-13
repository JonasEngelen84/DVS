using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class EditEmployeeCommand(EditEmployeeViewModel editEmployeeViewModel,
                                     EmployeeStore employeeStore,
                                     ClothesStore clothesStore,
                                     SizeStore sizeStore,
                                     CategoryStore categoryStore,
                                     SeasonStore seasonStore,
                                     ClothesSizeStore clothesSizeStore,
                                     EmployeeClothesSizesStore employeeClothesSizesStore,
                                     ModalNavigationStore modalNavigationStore)
                                     : AsyncCommandBase
    {
        private readonly EditEmployeeViewModel _editEmployeeViewModel = editEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel editEmployeeFormViewModel = _editEmployeeViewModel.AddEditEmployeeFormViewModel;
            
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
                await UpdateSizeModelAsync(editEmployeeFormViewModel);
                await UpdateCategoryAsync(editEmployeeFormViewModel);
                await UpdateSeasonAsync(editEmployeeFormViewModel);

                editEmployeeFormViewModel.IsSubmitting = false;

                _modalNavigationStore.Close();
            }
        }

        private static Employee CreateUpdatedEmployee(AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            return new Employee(editEmployeeFormViewModel.Employee.GuidID,
                                editEmployeeFormViewModel.ID,
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
                DetailedClothesListingItemViewModel existingItem = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList
                    .FirstOrDefault(dclivm => dclivm.ClothesSizeGuidID == ecs.ClothesSizeGuidID);

                if (existingItem == null)
                {
                    updatedEmployee.Clothes.Remove(ecs);
                    ecs.ClothesSize.EmployeeClothesSizes.Remove(ecs);

                    try
                    {
                        await _employeeClothesSizesStore.Delete(ecs.GuidID);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                        editEmployeeFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task CreateAndAddNewEmployeeClothesSizesAsync(Employee updatedEmployee, AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (DetailedClothesListingItemViewModel dclivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize existingItem = updatedEmployee.Clothes.FirstOrDefault(ecs => ecs.ClothesSizeGuidID == dclivm.ClothesSizeGuidID);

                if (existingItem == null)
                {
                    EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), updatedEmployee, dclivm.ClothesSize, (int)dclivm.Quantity, null);

                    updatedEmployee.Clothes.Add(employeeClothesSize);
                    dclivm.ClothesSize.EmployeeClothesSizes.Add(employeeClothesSize);

                    try
                    {
                        await _employeeClothesSizesStore.Add(employeeClothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                        editEmployeeFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task UpdateEmployeeClothesSizesAsync(Employee updatedEmployee, AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (DetailedClothesListingItemViewModel dclivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize existingItem = updatedEmployee.Clothes.FirstOrDefault(ecs => ecs.ClothesSizeGuidID == dclivm.ClothesSizeGuidID);

                if (existingItem != null)
                {
                    EmployeeClothesSize UpdatedEmployeeClothesSize = new(existingItem.GuidID, updatedEmployee, dclivm.ClothesSize, (int)dclivm.Quantity, null);

                    EmployeeClothesSize itemToRemove = updatedEmployee.Clothes.FirstOrDefault(ecs => ecs.GuidID == UpdatedEmployeeClothesSize.GuidID);

                    if (itemToRemove != null)
                    {
                        updatedEmployee.Clothes.Remove(itemToRemove);
                        updatedEmployee.Clothes.Add(UpdatedEmployeeClothesSize);
                    }


                    itemToRemove = UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.FirstOrDefault(ecs => ecs.GuidID == UpdatedEmployeeClothesSize.GuidID);

                    if (itemToRemove != null)
                    {
                        UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Remove(itemToRemove);
                        UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Add(UpdatedEmployeeClothesSize);
                    }


                    try
                    {
                        await _employeeClothesSizesStore.Update(UpdatedEmployeeClothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                        editEmployeeFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task UpdateEmployeeAsync(Employee updatedEmployee, AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            try
            {
                await _employeeStore.Update(updatedEmployee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                editEmployeeFormViewModel.HasError = true;
            }
        }

        private async Task UpdateClothesSizeAsync(AddEditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (DetailedClothesListingItemViewModel dclivm in editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                ClothesSize itemToRemove = dclivm.Clothes.Sizes.FirstOrDefault(cs => cs.GuidID == dclivm.ClothesSizeGuidID);

                if (itemToRemove != null)
                {
                    dclivm.Clothes.Sizes.Remove(itemToRemove);
                    dclivm.Clothes.Sizes.Add(dclivm.ClothesSize);
                }

                itemToRemove = dclivm.ClothesSize.Size.ClothesSizes.FirstOrDefault(cs => cs.GuidID == dclivm.ClothesSizeGuidID);

                if (itemToRemove != null)
                {
                    dclivm.ClothesSize.Size.ClothesSizes.Remove(itemToRemove);
                    dclivm.ClothesSize.Size.ClothesSizes.Add(dclivm.ClothesSize);
                }

                try
                {
                    await _clothesSizeStore.Update(dclivm.ClothesSize);
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
            foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                var itemToRemove = dclivm.Clothes.Category.Clothes.FirstOrDefault(c => c.GuidID == dclivm.Clothes.GuidID);

                if (itemToRemove != null)
                {
                    dclivm.Clothes.Category.Clothes.Remove(itemToRemove);
                    dclivm.Clothes.Category.Clothes.Add(dclivm.Clothes);
                }

                itemToRemove = dclivm.Clothes.Season.Clothes.FirstOrDefault(c => c.GuidID == dclivm.Clothes.GuidID);

                if (itemToRemove != null)
                {
                    dclivm.Clothes.Season.Clothes.Remove(itemToRemove);
                    dclivm.Clothes.Season.Clothes.Add(dclivm.Clothes);
                }

                try
                {
                    await _clothesStore.Update(dclivm.Clothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter bearbeiten");
                    addEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateSizeModelAsync(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                try
                {
                    await _sizeStore.Update(dclivm.ClothesSize.Size);
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
            foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                try
                {
                    await _categoryStore.Update(dclivm.Clothes.Category, null);
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
            foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                try
                {
                    await _seasonStore.Update(dclivm.Clothes.Season, null);
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
