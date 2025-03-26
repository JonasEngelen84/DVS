using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class EditSeasonCommand(
        AddEditSeasonViewModel addEditSeasonViewModel,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeStore employeeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = addEditSeasonViewModel.AddEditSeasonFormViewModel;

            if (Confirm($"Die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" und ihre Schnittstellen werden in" +
                    $"\"{addEditSeasonFormViewModel.EditSelectedSeason}\" umbenannt.\n\nUmbennen fortsetzen?", "Saison umbenennen"))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsSubmitting = true;

                HashSet<ClothesSize> editedClothesSizes = [];
                HashSet<EmployeeClothesSize> editedEcs = [];
                HashSet<Clothes> clothesToEdit = GetClothesToEdit(addEditSeasonFormViewModel);

                UpdateSeason(addEditSeasonFormViewModel);
                UpdateClothes(clothesToEdit, addEditSeasonFormViewModel);
                UpdateClothesSizes(clothesToEdit, editedClothesSizes);
                UpdateEmployeeClothesSizes(editedClothesSizes, editedEcs);
                UpdateEmployees(editedEcs);

                addEditSeasonFormViewModel.IsSubmitting = false;
            }
        }

        private void UpdateSeason(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            addEditSeasonFormViewModel.SelectedSeason.Name = addEditSeasonFormViewModel.EditSelectedSeason;
            seasonStore.Update(addEditSeasonFormViewModel.SelectedSeason);
        }

        private HashSet<Clothes> GetClothesToEdit(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            return clothesStore.Clothes
                .Where(c => c.Season.Name == addEditSeasonFormViewModel.SelectedSeason.Name)
                .ToHashSet();
        }

        private void UpdateClothes(HashSet<Clothes> clothesToEdit, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            foreach (Clothes clothes in clothesToEdit)
            {
                clothes.Season = addEditSeasonFormViewModel.SelectedSeason;

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
