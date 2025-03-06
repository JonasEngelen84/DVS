using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Employee : ObservableEntity
    {
        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Comment { get; set; }

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
