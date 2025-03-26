using CommunityToolkit.Mvvm.Input;
using DVS.Domain.Models;
using DVS.Domain.Services.Interfaces;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows.Input;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class DeleteSeasonCommand(
        AddEditSeasonViewModel addEditSeasonViewModel,
        SeasonStore seasonStore,
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
            AddEditSeasonFormViewModel addEditSeasonFormViewModel = addEditSeasonViewModel.AddEditSeasonFormViewModel;

            if (Confirm($"Wenn die Saison \"{addEditSeasonFormViewModel.SelectedSeason.Name}\" " +
                $"gelöscht wird, werden ihre Schnittstellen auf \"Saisonlos\" gesetzt.\n\nLöschen fortsetzen?", "Saison löschen"))
            {
                addEditSeasonFormViewModel.HasError = false;
                addEditSeasonFormViewModel.IsDeleting = true;

                HashSet<ClothesSize> editedClothesSizes = [];
                HashSet<EmployeeClothesSize> editedEcs = [];
                HashSet<Clothes> clothesToEdit = GetClothesToEdit(addEditSeasonFormViewModel);

                UpdateClothes(clothesToEdit);
                UpdateClothesSizes(clothesToEdit, editedClothesSizes);
                UpdateEmployeeClothesSizes(editedClothesSizes, editedEcs);
                UpdateEmployees(editedEcs);
                SaveCommand.Execute(null);
                await DeleteSeason(addEditSeasonFormViewModel);

                addEditSeasonFormViewModel.IsDeleting = false;
            }
        }

        private HashSet<Clothes> GetClothesToEdit(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            return clothesStore.Clothes
                .Where(c => c.Season.Name == addEditSeasonFormViewModel.SelectedSeason.Name)
                .ToHashSet();
        }

        private void UpdateClothes(HashSet<Clothes> clothesToEdit)
        {
            Season Seasonless = seasonStore.Seasons
                .First(s => s.Name == "-Saisonlos-");

            foreach (Clothes clothes in clothesToEdit)
            {
                clothes.Season = Seasonless;
                clothes.SeasonGuidId = Seasonless.Id;

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

        private async Task DeleteSeason(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            try
            {
                await seasonStore.Delete(addEditSeasonFormViewModel);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Löschen der Saison ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Saison löschen");

                addEditSeasonFormViewModel.HasError = true;
            }

            addEditSeasonFormViewModel.SelectedSeason = new(Guid.NewGuid(), "Saison wählen");
            addEditSeasonFormViewModel.EditSelectedSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
        }
    }
}
