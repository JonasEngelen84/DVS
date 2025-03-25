using CommunityToolkit.Mvvm.Input;
using DVS.Domain.Models;
using DVS.Domain.Services.Interfaces;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows.Input;

namespace DVS.WPF.Commands.CategoryCommands
{
    public class DeleteCategoryCommand(
        AddEditCategoryViewModel addEditCategoryViewModel,
        CategoryStore categoryStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeStore employeeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore,
        IDirtyEntitySaver dirtyEntitySaver)
        : AsyncCommandBase
    {
        public ICommand SaveCommand { get; } = new RelayCommand(async () => await dirtyEntitySaver.SaveDirtyEntitiesAsync());

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = addEditCategoryViewModel.AddEditCategoryFormViewModel;

            if (Confirm($"Wenn die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\" " +
                $"gelöscht wird, werden ihre Schnittstellen auf \"Kategorielos\" gesetzt.\n\nLöschen fortsetzen?", "Kategorie löschen"))
            {
                addEditCategoryFormViewModel.HasError = false;
                addEditCategoryFormViewModel.IsDeleting = true;

                HashSet<ClothesSize> editedClothesSizes = [];
                HashSet<EmployeeClothesSize> editedEcs = [];
                HashSet<Clothes> clothesToEdit = GetClothesToEdit(addEditCategoryFormViewModel);

                UpdateClothes(clothesToEdit);
                UpdateClothesSizes(clothesToEdit, editedClothesSizes);
                UpdateEmployeeClothesSizes(editedClothesSizes, editedEcs);
                UpdateEmployees(editedEcs);
                SaveCommand.Execute(null);
                await DeleteCategory(addEditCategoryFormViewModel);

                addEditCategoryFormViewModel.IsDeleting = false;
            }
        }

        private HashSet<Clothes> GetClothesToEdit(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            return clothesStore.Clothes
                .Where(c => c.Category.Name == addEditCategoryFormViewModel.SelectedCategory.Name)
                .ToHashSet();
        }

        private void UpdateClothes(HashSet<Clothes> clothesToEdit)
        {
            Category Categoryless = categoryStore.Categories
                .First(c => c.Name == "-Kategorielos-");

            foreach (Clothes clothes in clothesToEdit)
            {
                clothes.Category = Categoryless;
                clothes.CategoryGuidId = Categoryless.Id;

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

        private async Task DeleteCategory(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            try
            {
                await categoryStore.Delete(addEditCategoryFormViewModel);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Kategorie löschen");

                addEditCategoryFormViewModel.HasError = true;
            }
        }
    }
}
