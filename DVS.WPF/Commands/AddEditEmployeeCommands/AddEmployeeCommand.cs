using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel,
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
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
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
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            addEmployeeFormViewModel.HasError = false;
            addEmployeeFormViewModel.IsSubmitting = true;

            Employee employee = CreateEmployee(addEmployeeFormViewModel);

            await CreateAndAddEmployeeClothesSizesAsync(employee, addEmployeeFormViewModel);

            await AddEmployeeAsync(employee, addEmployeeFormViewModel);

            await UpdateClothesSizeAsync(addEmployeeFormViewModel);

            await UpdateClothesAsync(addEmployeeFormViewModel);

            await UpdateSizeModelAsync(addEmployeeFormViewModel);

            await UpdateCategoryAsync(addEmployeeFormViewModel);

            await UpdateSeasonAsync(addEmployeeFormViewModel);

            addEmployeeFormViewModel.IsSubmitting = false;

            _modalNavigationStore.Close();
        }


        private static Employee CreateEmployee(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            return new Employee(Guid.NewGuid(),
                                addEmployeeFormViewModel.ID,
                                addEmployeeFormViewModel.Firstname,
                                addEmployeeFormViewModel.Lastname,
                                addEmployeeFormViewModel.Comment);
        }

        private async Task CreateAndAddEmployeeClothesSizesAsync(Employee employee, AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), employee, dclivm.ClothesSize, (int)dclivm.Quantity, null);
                employee.Clothes.Add(employeeClothesSize);
                dclivm.ClothesSize.EmployeeClothesSizes.Add(employeeClothesSize);

                try
                {
                    await _employeeClothesSizesStore.Add(employeeClothesSize);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
                    addEmployeeFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task AddEmployeeAsync(Employee employee, AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            try
            {
                await _employeeStore.Add(employee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
                addEmployeeFormViewModel.HasError = true;
            }
        }

        private async Task UpdateClothesSizeAsync(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (DetailedClothesListingItemViewModel dclivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                var itemToRemove = dclivm.Clothes.Sizes.FirstOrDefault(cs => cs.GuidID == dclivm.ClothesSizeGuidID);

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
                    ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
                    addEmployeeFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task UpdateClothesAsync(AddEditEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (Clothes clothes in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EditedClothesList)
            {
                var itemToRemove = clothes.Category.Clothes.FirstOrDefault(c => c.GuidID == clothes.GuidID);

                if (itemToRemove != null)
                {
                    clothes.Category.Clothes.Remove(itemToRemove);
                    clothes.Category.Clothes.Add(clothes);
                }

                itemToRemove = clothes.Season.Clothes.FirstOrDefault(c => c.GuidID == clothes.GuidID);

                if (itemToRemove != null)
                {
                    clothes.Season.Clothes.Remove(itemToRemove);
                    clothes.Season.Clothes.Add(clothes);
                }

                try
                {
                    await _clothesStore.Update(clothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
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
                    ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
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
                    ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
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
                    ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Mitarbeiter erstellen");
                    addEmployeeFormViewModel.HasError = true;
                }
            }
        }
    }
}
