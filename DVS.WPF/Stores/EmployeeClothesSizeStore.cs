using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class EmployeeClothesSizeStore(ICreateEmployeeClothesSizeCommand createEmployeeClothesSizeCommand,
                                          IUpdateEmployeeClothesSizeCommand updateEmployeeClothesSizeCommand,
                                          IDeleteEmployeeClothesSizeCommand deleteEmployeeClothesSizeCommand)
    {
        private readonly List<EmployeeClothesSize> _employeeClothesSizes = [];
        public IEnumerable<EmployeeClothesSize> EmployeeClothesSizes => _employeeClothesSizes;

        public void Load(List<EmployeeClothesSize> employeeClothesSize)
        {
            _employeeClothesSizes.Clear();

            if (employeeClothesSize != null)
            {
                _employeeClothesSizes.AddRange(employeeClothesSize);
            }
        }

        public async Task Add(EmployeeClothesSize employeeClothesSize)
        {
            await createEmployeeClothesSizeCommand.Execute(employeeClothesSize);

            _employeeClothesSizes.Add(employeeClothesSize);
        }

        public async Task Update(EmployeeClothesSize updatedEmployeeClothesSize)
        {
            await updateEmployeeClothesSizeCommand.Execute(updatedEmployeeClothesSize);

            int index = _employeeClothesSizes.FindIndex(y => y.GuidId == updatedEmployeeClothesSize.GuidId);

            if (index != -1)
            {
                _employeeClothesSizes[index] = updatedEmployeeClothesSize;
            }
            else
            {
                _employeeClothesSizes.Add(updatedEmployeeClothesSize);
            }
        }

        public async Task Delete(EmployeeClothesSize employeeClothesSize)
        {
            await deleteEmployeeClothesSizeCommand.Execute(employeeClothesSize);

            int index = _employeeClothesSizes.FindIndex(y => y.GuidId == employeeClothesSize.GuidId);

            if (index != -1)
            {
                _employeeClothesSizes.RemoveAll(y => y.GuidId == employeeClothesSize.GuidId);
            }
            else
            {
                throw new InvalidOperationException("Entfernen der Bekleidung nicht möglich.");
            }            
        }
    }
}
