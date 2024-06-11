using System.Collections.ObjectModel;

namespace DVS.Models
{
    public class EmployeeModel(string id, string lastname, string firstname, string? comment)
    {
        public string ID { get; set; } = id;
        public string Firstname { get; set; } = firstname;
        public string Lastname { get; set; } = lastname;
        public string? Comment { get; set; } = comment;

        public ObservableCollection<EmployeeClothesModel> Clothes { get; private set; } = [];
    }
}
