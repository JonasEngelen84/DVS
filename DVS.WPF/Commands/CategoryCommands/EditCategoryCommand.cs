using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

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

            if (Confirm($"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditCategoryFormViewModel.EditSelectedCategory}\" umbenannt.\n\nUmbennen fortsetzen?", "Kategorie umbenennen"))
            {
                addEditCategoryFormViewModel.HasError = false;
                addEditCategoryFormViewModel.IsSubmitting = true;

                HashSet<ClothesSize> editedClothesSizes = [];
                HashSet<EmployeeClothesSize> editedEcs = [];
                HashSet<Clothes> clothesToEdit = GetClothesToEdit(addEditCategoryFormViewModel);

                EditCategory(addEditCategoryFormViewModel);
                UpdateClothes(clothesToEdit, addEditCategoryFormViewModel);
                UpdateClothesSizes(clothesToEdit, editedClothesSizes);
                UpdateEmployeeClothesSizes(editedClothesSizes, editedEcs);
                UpdateEmployees(editedEcs);

                addEditCategoryFormViewModel.IsDeleting = false;
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
                .Where(c => c.Category.Name == addEditCategoryFormViewModel.SelectedCategory.Name)
                .ToHashSet();
        }

        private void UpdateClothes(HashSet<Clothes> clothesToEdit, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (Clothes clothes in clothesToEdit)
            {
                clothes.Category = addEditCategoryFormViewModel.SelectedCategory;

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
                EmployeeClothesSize existingEcs = employeeClothesSize.Employee.Clothes
                    .First(ecs => ecs.Id == employeeClothesSize.Id);

                employeeClothesSize.Employee.Clothes.Remove(existingEcs);
                employeeClothesSize.Employee.Clothes.Add(employeeClothesSize);
                employeeStore.Update(employeeClothesSize.Employee);
            }
        }
    }
}
