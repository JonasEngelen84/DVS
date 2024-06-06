using System.Collections.ObjectModel;

namespace DVS.Models
{
    public class EmployeeModel(string? id, string? firstname, string? lastname, string? comment)
    {
        public string? Id { get; set; } = id;
        public string? Firstname { get; set; } = firstname;
        public string? Lastname { get; set; } = lastname;
        public string? Comment { get; set; } = comment;
        public ObservableCollection<ClothesModel> Clothes { get; private set; } = [];
    }
}
