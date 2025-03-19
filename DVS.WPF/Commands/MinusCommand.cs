using DVS.Domain.Models;
using DVS.WPF.Stores;

namespace DVS.WPF.Commands
{
    public class MinusCommand(
        SelectedClothesSizeStore selectedClothesSizeStore,
        SelectedEmployeeClothesSizeStore selectedEmployeeClothesSizeStore,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize != null)
            {
                EmployeeClothesSize existingEcs = employeeClothesSizeStore.EmployeeClothesSizes
                    .First(ecs => ecs.Id == selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.Id);

                ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes
                    .First(cs => cs.Id == selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.ClothesSizeGuidId);

                if (existingEcs.Quantity == 1)
                {
                    if (!Confirm("Soll die Bekleidungsgröße wirklich restlos dem Mitarbeiter entzogen werden?", "Bekleidung entfernen"))
                    {
                        return;
                    }

                    await EditOrDeleteEmployeeClothesSize(existingClothesSize, existingEcs, true);
                }
                else
                    await EditOrDeleteEmployeeClothesSize(existingClothesSize, existingEcs, false);
            }
            else if (selectedClothesSizeStore.SelectedClothesSize != null)
            {
                ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes
                    .First(cs => cs.Id == selectedClothesSizeStore.SelectedClothesSize.Id);

                if (existingClothesSize.Quantity == 0)
                {
                    ShowErrorMessageBox("Diese Größe ist bereits nicht mehr vorrätig!", "Stückzahl verringern");
                    return;
                }

                existingClothesSize.Quantity -= 1;
                UpdateClothes(existingClothesSize);
                clothesSizeStore.Update(existingClothesSize);
                selectedClothesSizeStore.SelectedClothesSize = existingClothesSize;
            }
        }

        private async Task EditOrDeleteEmployeeClothesSize(
            ClothesSize editedClothesSize,
            EmployeeClothesSize existingEcs,
            bool deleteEcs)
        {
            Employee existingEmployee = employeeStore.Employees.First(e => e.Id == existingEcs.EmployeeId);
            EmployeeClothesSize oldEcs = existingEmployee.Clothes.First(ecs => ecs.Id == existingEcs.Id);
            existingEmployee.Clothes.Remove(oldEcs);
            editedClothesSize.Quantity += 1;
            UpdateClothes(editedClothesSize);
            clothesSizeStore.Update(editedClothesSize);

            if (deleteEcs)
            {
                try
                {
                    await employeeClothesSizeStore.Delete(existingEcs);
                    selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize = null;
                }
                catch
                {
                    ShowErrorMessageBox("Entfernen der Bekleidung ist fehlgeschlagen!", "Bekleidung entziehen");
                }
            }
            else
            {
                existingEcs.Quantity -= 1;
                existingEmployee.Clothes.Add(existingEcs);
                employeeStore.Update(existingEmployee);
                employeeClothesSizeStore.Update(existingEcs);
                selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize = existingEcs;
            }

            employeeStore.Update(existingEmployee);

        }

        private void UpdateClothes(ClothesSize editedClothesSize)
        {
            Clothes existingClothes = clothesStore.Clothes
                .First(c => c.Id == editedClothesSize.ClothesId);

            ClothesSize existingClothesSize = existingClothes.Sizes
                .First(cs => cs.Id == editedClothesSize.Id);

            existingClothes.Sizes.Remove(existingClothesSize);
            existingClothes.Sizes.Add(editedClothesSize);
            clothesStore.Update(existingClothes);
        }
    }
}
