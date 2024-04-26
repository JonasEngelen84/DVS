using DVS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class EmployeesClothesListViewViewModel : ViewModelBase
    {
        // Bereitstellung einer Mitarbeiterliste
        // (plus eines KleiderDictionary pro Mitarbeiter).
        private readonly List<EmployeeModel> EmployeeList;

        // Bereitstellung einer ObservableCollection der Mitarbeiter-Kleidung
        // (Kleidungsliste der Mitarbeiter aufgelöst für ListView).
        private readonly ObservableCollection<EmployeeModel> _employeeClothesList;

        // Zur encapsulation (private) von "_employeeClothesList"
        // wird ein IEnumerable als pointer verwendet.
        public IEnumerable<EmployeeModel> EmployeeClothesList => _employeeClothesList;

        public EmployeesClothesListViewViewModel()
        {
            EmployeeList = [];
            _employeeClothesList = [];

            FillEL();
            FillOC();
        }

        private void FillOC()
        {
            foreach (var employee in EmployeeList)
            {
                employee.EmployeeClothesDictionary[0].
                _employeeClothesList.Add(new EmployeeClothesListViewItemViewModel(
                    employee.EmployeeId, employee.EmployeeFirstname, employee.EmployeeLastname,
                    employee.EmployeeClothesDictionary[0].Key, employee.EmployeeClothesDictionary[0],
                    )
            }//TODO: hier
        }

        private void FillEL()
        {
            EmployeeList.Add(new EmployeeModel(111, "Jonas", "Engelen"));
            EmployeeList[0].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[0].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[0].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[0].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[0].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[0].EmployeeClothesDictionary.Add(852, 1);
            EmployeeList.Add(new EmployeeModel(123, "Markus", "Oettken"));
            EmployeeList[1].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[1].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[1].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[1].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[1].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[1].EmployeeClothesDictionary.Add(852, 1);
            EmployeeList.Add(new EmployeeModel(456, "Nadine", "Molik"));
            EmployeeList[2].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[2].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[2].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[2].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[2].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[2].EmployeeClothesDictionary.Add(852, 1);
            EmployeeList.Add(new EmployeeModel(134, "Kemal", "Yüksel"));
            EmployeeList[3].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[3].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[3].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[3].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[3].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[3].EmployeeClothesDictionary.Add(852, 1);
            EmployeeList.Add(new EmployeeModel(133, "Irfan", "Yüksel"));
            EmployeeList[4].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[4].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[4].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[4].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[4].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[4].EmployeeClothesDictionary.Add(852, 1);
            EmployeeList.Add(new EmployeeModel(245, "Walther", "Löwenkamp"));
            EmployeeList[5].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[5].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[5].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[5].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[5].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[5].EmployeeClothesDictionary.Add(852, 1);
            EmployeeList.Add(new EmployeeModel(142, "Daniel", "Nickol"));
            EmployeeList[6].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[6].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[6].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[6].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[6].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[6].EmployeeClothesDictionary.Add(852, 1);
            EmployeeList.Add(new EmployeeModel(231, "Stefan", "Killen"));
            EmployeeList[7].EmployeeClothesDictionary.Add(111, 4);
            EmployeeList[7].EmployeeClothesDictionary.Add(444, 3);
            EmployeeList[7].EmployeeClothesDictionary.Add(123, 2);
            EmployeeList[7].EmployeeClothesDictionary.Add(147, 1);
            EmployeeList[7].EmployeeClothesDictionary.Add(654, 1);
            EmployeeList[7].EmployeeClothesDictionary.Add(852, 1);
        }
    }
}
