using DVS.Domain.Models;
using DVS.EntityFramework.Commands.EmployeeClothesSizeCommands;
using DVS.WPF.Stores;

namespace DVS.WPF.Commands
{
    public class PlusCommand(
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
                ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes
                    .First(cs => cs.Id == selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.ClothesSizeGuidId);

                if (existingClothesSize.Quantity == 0)
                {
                    ShowErrorMessageBox("Diese Größe ist nicht vorrätig!", "Stückzahl erhöhen");
                    return;
                }

                existingClothesSize.Quantity -= 1;
                UpdateClothes(existingClothesSize);
                clothesSizeStore.Update(existingClothesSize);

                EmployeeClothesSize existingEcs = employeeClothesSizeStore.EmployeeClothesSizes
                    .First(ecs => ecs.Id == selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize.Id);

                existingEcs.Quantity += 1;
                UpdateEmployee(existingEcs);
                employeeClothesSizeStore.Update(existingEcs);
                selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize = existingEcs;
            }
            else if (selectedClothesSizeStore.SelectedClothesSize != null)
            {
                ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes
                    .First(cs => cs.Id == selectedClothesSizeStore.SelectedClothesSize.Id);

                existingClothesSize.Quantity += 1;
                UpdateClothes(existingClothesSize);
                clothesSizeStore.Update(existingClothesSize);
                selectedClothesSizeStore.SelectedClothesSize = existingClothesSize;
            }
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

        private void UpdateEmployee(EmployeeClothesSize editedEcs)
        {
            Employee existingEmployee = employeeStore.Employees
                .First(e => e.Id == editedEcs.Employee.Id);

            EmployeeClothesSize oldEcs = existingEmployee.Clothes
                .First(ecs => ecs.Id == editedEcs.Id);

            existingEmployee.Clothes.Remove(oldEcs);
            existingEmployee.Clothes.Add(editedEcs);

            employeeStore.Update(existingEmployee);
        }
    }
}
