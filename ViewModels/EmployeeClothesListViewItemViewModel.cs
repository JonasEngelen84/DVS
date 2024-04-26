using DVS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVS.ViewModels
{
    class EmployeeClothesListViewItemViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstname { get; set; }
        public string EmployeeLastname { get; set; }
        public int ClothesId { get; set; }
        public string ClothesName { get; set; }
        public string ClothesSize { get; set; }
        public int ClothesQuantity { get; set; }
        public string Comment { get; set; }

        public EmployeeClothesListViewItemViewModel(int employeeId, string employeeFirstname, string employeeLastname,
            int clothesId, string clothesName, string clothesSize, int clothesQuantity, string comment)
        {
            EmployeeId = employeeId;
            EmployeeFirstname = employeeFirstname;
            EmployeeLastname = employeeLastname;
            ClothesId = clothesId;
            ClothesName = clothesName;
            ClothesSize = clothesSize;
            ClothesQuantity = clothesQuantity;
            Comment = comment;
        }

    }
}
