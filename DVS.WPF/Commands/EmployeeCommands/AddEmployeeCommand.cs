using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(
        AddEmployeeViewModel addEmployeeViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore,
        DVSListingViewModel dVSListingViewModel)
        : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizeStore = employeeClothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEmployeeFormViewModel;

            if (CheckEmployeeId(addEmployeeFormViewModel) != null)
                ShowErrorMessageBox("Die eingegebene Id ist bereits vergeben!\nBitte eine andere Id eingeben.", "Vorhandene Id");
            else
            {
                addEmployeeFormViewModel.HasError = false;
                addEmployeeFormViewModel.IsSubmitting = true;
                Employee newEmployee = CreateNewEmployee(addEmployeeFormViewModel);

                if (addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList != null)
                    CreateEmployeeClothesSizes(newEmployee, addEmployeeFormViewModel);

                await UpdateClothesSizes(addEmployeeFormViewModel);
                await AddEmployeeToDB(newEmployee, addEmployeeFormViewModel);

                addEmployeeFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }

        private Employee CheckEmployeeId(AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            Employee? existingEmployeeId = _employeeStore.Employees
                .FirstOrDefault(e => e.Id == addEmployeeFormViewModel.Id);

            return existingEmployeeId;
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

            return newEmployee;
        }

        private static void CreateEmployeeClothesSizes(Employee newEmployee, AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in addEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList)
            {
                EmployeeClothesSize employeeClothesSize = new(Guid.NewGuid(), newEmployee, ecslivm.ClothesSize, ecslivm.Quantity, ecslivm.Comment);
                newEmployee.Clothes.Add(employeeClothesSize);
            }
        }

        private async Task AddEmployeeToDB(Employee newEmployee, AddEmployeeFormViewModel addEmployeeFormViewModel)
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

        private async Task UpdateClothesSizes(AddEmployeeFormViewModel addEmployeeFormViewModel)
        {
            List<AvailableClothesSizeItem> EditedClothesSizes = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllEditedClothesSizes();

            foreach (AvailableClothesSizeItem acsi in EditedClothesSizes)
            {
                ClothesSize? existingClothesSize = _clothesSizeStore.ClothesSizes
                    .FirstOrDefault(cs => cs.GuidId == acsi.ClothesSizeId);

                if (existingClothesSize != null)
                {
                    AvailableClothesSizeItem? existingAcsi = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.AvailableClothesSizes
                        .FirstOrDefault(acsi => acsi.ClothesSize.GuidId == existingClothesSize.GuidId);

                    ClothesSize editedClothesSize = new(existingClothesSize.GuidId,
                                                        existingClothesSize.Clothes,
                                                        existingClothesSize.Size,
                                                        existingAcsi.Quantity,
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

        //private async Task UpdateClothes(AddEmployeeFormViewModel addEmployeeFormViewModel)
        //{
        //    List<AvailableClothesSizeItem> EditedClothesSizes = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllEditedClothesSizes();
        //    List<Clothes> EditedClothes = addEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllEditedClothes();

        //    foreach (Clothes clothes in EditedClothes)
        //    {
        //        Clothes editedClothes = new(
        //            clothes.Id,
        //            clothes.Name,
        //            clothes.Category,
        //            clothes.Season,
        //            clothes.Comment)
        //        {
        //            Sizes = clothes.Sizes
        //        };

        //        foreach (AvailableClothesSizeItem acsi in EditedClothesSizes)
        //        {
        //            if (acsi.ClothesId == clothes.Id)
        //            {
        //                ClothesSize newClothesSize = new(
        //                    acsi.ClothesSizeId,
        //                    acsi.ClothesSize.Clothes,
        //                    acsi.ClothesSize.Size,
        //                    acsi.Quantity,
        //                    acsi.Comment)
        //                {
        //                    EmployeeClothesSizes = []
        //                };

        //                ClothesSize? existingClothesSize = editedClothes.Sizes
        //                .FirstOrDefault(cs => cs.GuidId == newClothesSize.GuidId);

        //                if (existingClothesSize != null)
        //                {
        //                    editedClothes.Sizes.Remove(existingClothesSize);
        //                    editedClothes.Sizes.Add(newClothesSize);
        //                }
        //            }
        //        }

        //        try
        //        {
        //            await _clothesStore.Update(editedClothes);
        //        }
        //        catch
        //        {
        //            ShowErrorMessageBox("Erstellen des Mitarbeiters ist fehlgeschlagen!", "AddEmployeeCommand, UpdateClothes");
        //            addEmployeeFormViewModel.HasError = true;
        //        }
        //    }
        //}
    }
}
