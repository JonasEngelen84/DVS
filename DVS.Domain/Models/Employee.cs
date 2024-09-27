using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Employee
    {
        public Guid GuidID { get; }
        public string ID { get; private set; }
        public string Lastname { get; private set; }
        public string Firstname { get; private set; }
        public string Comment { get; private set; }

        public ObservableCollection<EmployeeClothesSize> Clothes { get; set; }

        public Employee(Guid guidID, string id, string lastname, string firstname, string comment)
        {
            GuidID = guidID;
            ID = id;
            Lastname = lastname;
            Firstname = firstname;
            Comment = comment;

            Clothes = [];
        }

        public Employee()
        {

        }
    }
}
