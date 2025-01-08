using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Employee
    {
        public Guid GuidId { get; }
        public string Id { get; private set; }
        public string Lastname { get; private set; }
        public string Firstname { get; private set; }
        public string Comment { get; private set; }

        public ObservableCollection<EmployeeClothesSize> Clothes { get; set; }

        public Employee(Guid guidId, string id, string lastname, string firstname, string comment)
        {
            GuidId = guidId;
            Id = id;
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
