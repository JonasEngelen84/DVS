using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class Employee(Guid guidID, string id, string lastname, string firstname, string? comment)
    {
        public Guid GuidID { get; } = guidID;
        public string ID { get; } = id;
        public string Lastname { get; } = lastname;
        public string Firstname { get; } = firstname;
        public string? Comment { get; } = comment;

        public ObservableCollection<EmployeeClothesSize> Clothes { get; set; } = [];
    }
}
