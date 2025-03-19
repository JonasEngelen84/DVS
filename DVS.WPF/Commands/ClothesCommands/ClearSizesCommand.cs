using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class ClearSizesCommand(
        ClothesListingItemViewModel clothesListingItemViewModel,
        ClothesStore clothesStore,
        EmployeeStore employeeStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : AsyncCommandBase
    {
        private readonly List<ClothesSize> ClothesSizesToDelete = new(clothesListingItemViewModel.Sizes);
        private readonly List<ClothesSize> ClothesSizesToEdit = [];
        private List<EmployeeClothesSize> assignedClothesSizes = [];
        private readonly List<EmployeeClothesSize> EditedEcs = [];

        public override async Task ExecuteAsync(object parameter)
        {
            if (clothesListingItemViewModel.Sizes.Count == 0)
            {
                ShowErrorMessageBox($"Die Bekleidung  {clothesListingItemViewModel.Id}, {clothesListingItemViewModel.Name}  " +
                    $"enthält bereits keine Größen!", "Bekleidungs-Größen löschen");

                return;
            }

            if (Confirm($"Alle Größen der Bekleidung  \"{clothesListingItemViewModel.Id}, {clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                "\n\nLöschen fortsetzen?", "Alle Bekleidungs-Größen löschen"))
            {
                clothesListingItemViewModel.IsDeleting = true;
                clothesListingItemViewModel.HasError = false;

                Clothes editedClothes = CreateEditedClothes();
                await DeleteClothesSizes();

                if (ClothesSizesToEdit.Count > 0)
                {
                    UpdateClothesSizes(editedClothes);
                    UpdateEmployeeClothesSizes(editedClothes);
                    UpdateEmployee();
                }

                clothesStore.Update(editedClothes);

                clothesListingItemViewModel.IsDeleting = false;
            }
        }

        private Clothes CreateEditedClothes()
        {
            Clothes editedClothes = new(clothesListingItemViewModel.Id,
                                        clothesListingItemViewModel.Name,
                                        clothesListingItemViewModel.Category,
                                        clothesListingItemViewModel.Season,
                                        clothesListingItemViewModel.Comment)
            {
                Sizes = []
            };

            return editedClothes;
        }

        private async Task DeleteClothesSizes()
        {
            foreach (ClothesSize clothesSize in ClothesSizesToDelete)
            {
                EmployeeClothesSize? assignedClothesSize = employeeClothesSizeStore.EmployeeClothesSizes
                    .FirstOrDefault(ecs => ecs.ClothesSizeGuidId == clothesSize.Id);

                if (assignedClothesSize != null)
                {
                    ClothesSizesToEdit.Add(clothesSize);
                    ShowErrorMessageBox($"Löschen der Größe  {clothesSize.Size}  ist nicht möglich, da diese Größe in Besitz ist!", "Bekleidungs-Größen löschen");
                }
                else
                {
                    try
                    {
                        await clothesSizeStore.Delete(clothesSize);
                    }
                    catch
                    {
                        ShowErrorMessageBox($"Löschen der Größe  {clothesSize.Size}  ist fehlgeschlagen!", "Bekleidungs-Größen löschen");
                    }
                }
            }
        }

        private void UpdateClothesSizes(Clothes editedClothes)
        {            
            foreach (ClothesSize clothesSize in ClothesSizesToEdit)
            {
                ClothesSize editedClothesSize = new(
                    clothesSize.Id,
                    editedClothes,
                    clothesSize.Size,
                    clothesSize.Quantity,
                    clothesSize.Comment)
                {
                    EmployeeClothesSizes = clothesSize.EmployeeClothesSizes
                };

                clothesSizeStore.Update(editedClothesSize);

                editedClothes.Sizes.Add(editedClothesSize);
            }
        }

        private void UpdateEmployeeClothesSizes(Clothes editedClothes)
        {
            foreach (ClothesSize clothesSize in editedClothes.Sizes)
            {
                assignedClothesSizes = employeeClothesSizeStore.EmployeeClothesSizes
                .Where(ecs => ecs.ClothesSizeGuidId == clothesSize.Id)
                .ToList();

                foreach (EmployeeClothesSize ecs in assignedClothesSizes)
                {
                    EmployeeClothesSize editedEcs = new(
                        ecs.Id,
                        ecs.Employee,
                        clothesSize,
                        ecs.Quantity,
                        ecs.Comment);

                    EditedEcs.Add(editedEcs);

                    employeeClothesSizeStore.Update(editedEcs);
                }
            }
        }
        
        private void UpdateEmployee()
        {
            List<EmployeeClothesSize> ecsOwner = [];

            foreach (EmployeeClothesSize employeeClothesSize in EditedEcs)
            {
                ecsOwner = EditedEcs.Where(ecs => ecs.EmployeeId == employeeClothesSize.EmployeeId).ToList();

                Employee editedEmployee = new(
                    employeeClothesSize.EmployeeId,
                    employeeClothesSize.Employee.Lastname,
                    employeeClothesSize.Employee.Firstname,
                    employeeClothesSize.Employee.Comment)
                {
                    Clothes = employeeClothesSize.Employee.Clothes
                };

                foreach (EmployeeClothesSize employeeCS in ecsOwner)
                {
                    EmployeeClothesSize existingEcs = editedEmployee.Clothes
                        .First(ecs => ecs.Id == employeeCS.Id);

                    editedEmployee.Clothes.Remove(existingEcs);
                    editedEmployee.Clothes.Add(employeeCS);
                }

                employeeStore.Update(editedEmployee);
            }
        }
    }
}
