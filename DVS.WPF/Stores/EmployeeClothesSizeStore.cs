using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    //TODO: IDirtyEntitySaver implementieren
    public class EmployeeClothesSizeStore(ICreateEmployeeClothesSizeCommand createEmployeeClothesSizeCommand,
                                          IDeleteEmployeeClothesSizeCommand deleteEmployeeClothesSizeCommand)
    {
        private readonly List<EmployeeClothesSize> _employeeClothesSizes = [];
        public IEnumerable<EmployeeClothesSize> EmployeeClothesSizes => _employeeClothesSizes;

        public event Action<EmployeeClothesSize> EmployeeClothesSizeAdded;
        public event Action<EmployeeClothesSize> EmployeeClothesSizeUpdated;
        public event Action<EmployeeClothesSize> EmployeeClothesSizeDeleted;

        public void Load(List<EmployeeClothesSize> employeeClothesSize)
        {
            _employeeClothesSizes.Clear();

            if (employeeClothesSize != null)
            {
                _employeeClothesSizes.AddRange(employeeClothesSize);
            }
        }

        public async Task AddDataBase(EmployeeClothesSize employeeClothesSize)
        {
            await createEmployeeClothesSizeCommand.Execute(employeeClothesSize);

            _employeeClothesSizes.Add(employeeClothesSize);
            EmployeeClothesSizeAdded.Invoke(employeeClothesSize);
        }
        
        public void AddStore(EmployeeClothesSize employeeClothesSize)
        {
            _employeeClothesSizes.Add(employeeClothesSize);
        }

        public void Update(EmployeeClothesSize editedEmployeeClothesSize)
        {
            int index = _employeeClothesSizes.FindIndex(y => y.Id == editedEmployeeClothesSize.Id);

            if (index != -1)
            {
                _employeeClothesSizes[index] = editedEmployeeClothesSize;
            }
            else
            {
                _employeeClothesSizes.Add(editedEmployeeClothesSize);
            }

            EmployeeClothesSizeUpdated.Invoke(editedEmployeeClothesSize);

            editedEmployeeClothesSize.IsDirty = true;
        }

        public async Task Delete(EmployeeClothesSize employeeClothesSize)
        {
            await deleteEmployeeClothesSizeCommand.Execute(employeeClothesSize);

            int index = _employeeClothesSizes.FindIndex(y => y.Id == employeeClothesSize.Id);

            if (index != -1)
            {
                _employeeClothesSizes.RemoveAll(y => y.Id == employeeClothesSize.Id);
            }
            else
            {
                throw new InvalidOperationException("Entfernen der Bekleidung nicht möglich.");
            }

            EmployeeClothesSizeDeleted.Invoke(employeeClothesSize);
        }
    }
}
