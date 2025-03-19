using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DVS.Domain.Models
{
    public class Employee : ObservableEntity
    {
        public string Id { get; set; }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                }
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;
                }
            }
        }

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                }
            }
        }

        public ObservableCollection<EmployeeClothesSize> Clothes {  get; set; }

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

        public Employee() { }
    }
}
