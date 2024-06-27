using DVS.ViewModels;

namespace DVS.Models
{
    public class EmployeeModel(string id, string lastname, string firstname, string? comment)
    {
        public string ID { get; set; } = id;
        public string Lastname { get; set; } = lastname;
        public string Firstname { get; set; } = firstname;
        public string? Comment { get; set; } = comment;

        public List<DetailedClothesListingItemViewModel> Clothes { get; set; } = [];
    }
}
