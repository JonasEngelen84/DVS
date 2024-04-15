using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Models
{
    public class EmployeeModel(string firstname, string lastname, int id)
    {
        public string Firstname { get; set; } = firstname;
        public string Lastname { get; set; } = lastname;
        public int Id { get; set; } = id;
        public List<ClothesModel> Clothes = [];
    }

}
