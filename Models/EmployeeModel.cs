using DVS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Models
{
    public class EmployeeModel(int employeeId, string employeeFirstname, string employeeLastname)
    {
        public int EmployeeId { get; set; } = employeeId;
        public string EmployeeFirstname { get; set; } = employeeFirstname;
        public string EmployeeLastname { get; set; } = employeeLastname;
        public Dictionary<int, int> EmployeeClothesDictionary { get; set; } = [];
    }

}
