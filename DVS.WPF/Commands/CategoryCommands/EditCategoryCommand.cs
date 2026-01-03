using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Diagnostics;

namespace DVS.WPF.Commands.CategoryCommands
{
    public class EditCategoryCommand(
        AddEditCategoryViewModel addEditCategoryViewModel,
        CategoryStore categoryStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeStore employeeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = addEditCategoryViewModel.AddEditCategoryFormViewModel;

            if (!Confirm($"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditCategoryFormViewModel.EditSelectedCategory}\" umbenannt.\n\nUmbennen fortsetzen?", "Kategorie umbenennen"))
            {
                return;
            }

            addEditCategoryFormViewModel.HasError = false;
            addEditCategoryFormViewModel.IsSubmitting = true;

            try
            {
                HashSet<ClothesSize> editedClothesSizes = [];
                HashSet<EmployeeClothesSize> editedEcs = [];
                HashSet<Clothes> clothesToEdit = GetClothesToEdit(addEditCategoryFormViewModel);

                //TODO: transaction implementieren
                //using var transaction = categoryStore.BeginTransaction();
                EditCategory(addEditCategoryFormViewModel);
                UpdateClothes(clothesToEdit, addEditCategoryFormViewModel);
                UpdateClothesSizes(clothesToEdit, editedClothesSizes);
                UpdateEmployeeClothesSizes(editedClothesSizes, editedEcs);
                UpdateEmployees(editedEcs);
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                LogError(ex);
                addEditCategoryFormViewModel.HasError = true;
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }

        private void EditCategory(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            addEditCategoryFormViewModel.SelectedCategory.Name = addEditCategoryFormViewModel.EditSelectedCategory;

            categoryStore.Update(addEditCategoryFormViewModel.SelectedCategory);
        }

        private HashSet<Clothes> GetClothesToEdit(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            return clothesStore.Clothes
                .Where(c => c.Category.Id == addEditCategoryFormViewModel.SelectedCategory.Id)
                .ToHashSet();
        }

        private void UpdateClothes(HashSet<Clothes> clothesToEdit, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (Clothes clothes in clothesToEdit)
            {
                clothes.Category = addEditCategoryFormViewModel.SelectedCategory;
                clothes.CategoryGuidId = addEditCategoryFormViewModel.SelectedCategory.Id;

                clothesStore.Update(clothes);
            }
        }

        private void UpdateClothesSizes(HashSet<Clothes> editedClothes, HashSet<ClothesSize> editedClothesSizes)
        {
            foreach (Clothes editedCl in editedClothes)
            {
                foreach (ClothesSize csToEdit in editedCl.Sizes)
                {
                    csToEdit.Clothes = editedCl;
                    editedClothesSizes.Add(csToEdit);

                    clothesSizeStore.Update(csToEdit);
                }
            }
        }

        private void UpdateEmployeeClothesSizes(HashSet<ClothesSize> editedClothesSizes, HashSet<EmployeeClothesSize> editedEcs)
        {
            foreach (ClothesSize clothesSize in editedClothesSizes)
            {
                List<EmployeeClothesSize> assignedClothesSizes = employeeClothesSizeStore.EmployeeClothesSizes
                    .Where(ecs => ecs.ClothesSizeGuidId == clothesSize.Id)
                    .ToList();

                foreach (EmployeeClothesSize ecs in assignedClothesSizes)
                {
                    ecs.ClothesSize = clothesSize;
                    editedEcs.Add(ecs);
                    employeeClothesSizeStore.Update(ecs);
                }
            }
        }

        private void UpdateEmployees(HashSet<EmployeeClothesSize> editedEcs)
        {
            foreach (EmployeeClothesSize employeeClothesSize in editedEcs)
            {
                var existingEcs = employeeClothesSize.Employee.Clothes
                    .FirstOrDefault(ecs => ecs.Id == employeeClothesSize.Id);

                if (existingEcs != null)
                {
                    employeeClothesSize.Employee.Clothes.Remove(existingEcs);
                }

                employeeClothesSize.Employee.Clothes.Add(employeeClothesSize);
                employeeStore.Update(employeeClothesSize.Employee);
            }
        }

        private static void LogError(Exception ex)
        {
            Debug.WriteLine($"[ERROR] {ex.Message}");
        }
    }
}
