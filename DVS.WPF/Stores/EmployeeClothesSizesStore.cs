using DVS.Domain.Commands.EmployeeClothesSizeCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using System.Windows;

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
            IEnumerable<EmployeeClothesSize> employeeClothesSize = [];

            try
            {
                employeeClothesSize = await _getAllEmployeeClothesSizesQuery.Execute();
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Laden der EmployeeClothesSizes von Datenbank ist fehlgeschlagen!", "EmployeeClothesSizesStore, Load", button, icon);
            }

            _employeeClothesSizes.Clear();

            if (employeeClothesSize != null)
            {
                _employeeClothesSizes.AddRange(employeeClothesSize);
            }
        }

        public async Task Add(EmployeeClothesSize employeeClothesSize)
        {
            try
            {
                await _createEmployeeClothesSizeCommand.Execute(employeeClothesSize);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Hinzufügen der EmployeeClothesSize in Datenbank ist fehlgeschlagen!", "EmployeeClothesSizesStore, Add", button, icon);
            }

            _employeeClothesSizes.Add(employeeClothesSize);
        }

        public async Task Update(EmployeeClothesSize employeeClothesSize)
        {
            try
            {
                //await _updateEmployeeClothesSizeCommand.Execute(employeeClothesSize);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Updaten des EmployeeClothesSize in Datenbank ist fehlgeschlagen!", "EmployeeClothesSizesStore, Update", button, icon);
            }

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

        public async Task Delete(EmployeeClothesSize employeeClothesSize)
        {
            try
            {
                await _deleteEmployeeClothesSizeCommand.Execute(employeeClothesSize);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Löschen der EmployeeClothesSize aus Datenbank ist fehlgeschlagen!", "EmployeeClothesSizesStore, Delete", button, icon);
            }

            _employeeClothesSizes.RemoveAll(y => y.GuidID == employeeClothesSize.GuidID);
        }
    }
}
