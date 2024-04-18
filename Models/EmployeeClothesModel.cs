using DVS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Models
{
    public class EmployeeClothesModel(string employeeFirstname, string employeeLastname, int employeeId, string clothesName, string clothesSize,int quantity)
    {
        public string EmployeeFirstname { get; set; } = employeeFirstname;
        public string EmployeeLastname { get; set; } = employeeLastname;
        public int EmployeeId { get; set; } = employeeId;
        public string ClothesName { get; set; } = clothesName;
        public string ClothesSize { get; set; } = clothesSize;
        public int Quantity { get; set; } = quantity;
        public string Comment { get; set; } = "";
    }

}
