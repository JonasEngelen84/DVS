using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Employee
    {
        public string Id { get; }
        public string Lastname { get; }
        public string Firstname { get; }
        public string Comment { get; }

        public ObservableCollection<EmployeeClothesSize> Clothes { get; set; }

        public Employee(
            string id,
            string lastname,
            string firstname,
            string comment)
        {
            Id = id;
            Lastname = lastname;
            Firstname = firstname;
            Comment = comment;

            Clothes = [];
        }

        public Employee() {}
    }
}
