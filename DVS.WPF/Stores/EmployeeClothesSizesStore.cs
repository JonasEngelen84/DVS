using DVS.Domain.Commands.EmployeeClothesSize;
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
            try
            {
                IEnumerable<EmployeeClothesSize> employeeClothesSize = await _getAllEmployeeClothesSizesQuery.Execute();

                _employeeClothesSizes.Clear();

                if (employeeClothesSize != null)
                {
                    _employeeClothesSizes.AddRange(employeeClothesSize);
                }
            }
            catch (Exception ex)
            {
                //TODO: Fehlerbehandlung beim laden der Bekleidungsgrößen aus DB
                Console.WriteLine($"Fehler beim Laden der EmployeeClothesSizes: {ex.Message}");
            }
        }

        public async Task Add(EmployeeClothesSize employeeClothesSize)
        {
            //await _createEmployeeClothesSizeCommand.Execute(employeeClothesSize);
            _employeeClothesSizes.Add(employeeClothesSize);
        }

        public async Task Update(EmployeeClothesSize employeeClothesSize)
        {
            //await _updateEmployeeClothesSizeCommand.Execute(employeeClothesSize);

            int index = _employeeClothesSizes.FindIndex(y => y.GuidID == employeeClothesSize.GuidID);

            if (index != -1)
            {
                _employeeClothesSizes[index] = employeeClothesSize;
            }
            else
            {
                _employeeClothesSizes.Add(employeeClothesSize);
            }
        }

        public async Task Delete(Guid guidID)
        {
            //await _deleteEmployeeClothesSizeCommand.Execute(guidID);
            _employeeClothesSizes.RemoveAll(y => y.GuidID == guidID);
        }
    }
}
