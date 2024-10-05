using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;

namespace DVS.WPF.Stores
{
    public class EmployeeClothesSizesStore(IGetAllEmployeeClothesSizesQuery getAllEmployeeClothesSizesQuery,
                                           ICreateEmployeeClothesSizeCommand createEmployeeClothesSizeCommand,
                                           IUpdateEmployeeClothesSizeCommand updateEmployeeClothesSizeCommand,
                                           IDeleteEmployeeClothesSizeCommand deleteEmployeeClothesSizeCommand)
    {
        private readonly IGetAllEmployeeClothesSizesQuery _getAllEmployeeClothesSizesQuery = getAllEmployeeClothesSizesQuery;
        private readonly ICreateEmployeeClothesSizeCommand _createEmployeeClothesSizeCommand = createEmployeeClothesSizeCommand;
        private readonly IUpdateEmployeeClothesSizeCommand _updateEmployeeClothesSizeCommand = updateEmployeeClothesSizeCommand;
        private readonly IDeleteEmployeeClothesSizeCommand _deleteEmployeeClothesSizeCommand = deleteEmployeeClothesSizeCommand;

        private readonly List<EmployeeClothesSize> _employeeClothesSizes = [];
        public IEnumerable<EmployeeClothesSize> EmployeeClothesSizes => _employeeClothesSizes;

        public async Task Load()
        {
            IEnumerable<EmployeeClothesSize> employeeClothesSize = await _getAllEmployeeClothesSizesQuery.Execute();

            _employeeClothesSizes.Clear();

            if (employeeClothesSize != null)
            {
                _employeeClothesSizes.AddRange(employeeClothesSize);
            }
        }

        public async Task Add(EmployeeClothesSize employeeClothesSize)
        {
            await _createEmployeeClothesSizeCommand.Execute(employeeClothesSize);

            _employeeClothesSizes.Add(employeeClothesSize);
        }

        public async Task Update(EmployeeClothesSize updatedEmployeeClothesSize)
        {
            await _updateEmployeeClothesSizeCommand.Execute(updatedEmployeeClothesSize);

            int index = _employeeClothesSizes.FindIndex(y => y.GuidID == updatedEmployeeClothesSize.GuidID);

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
            await _deleteEmployeeClothesSizeCommand.Execute(employeeClothesSize);

            _employeeClothesSizes.RemoveAll(y => y.GuidID == employeeClothesSize.GuidID);
        }
    }
}
