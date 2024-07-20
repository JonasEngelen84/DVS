using System;
using System.Collections.ObjectModel;

namespace DVS.Models
{
    public class EmployeeModel(Guid guidID, string id, string lastname, string firstname, string? comment)
    {
        public Guid GuidID { get; set; } = guidID;
        public string ID { get; set; } = id;
        public string Lastname { get; set; } = lastname;
        public string Firstname { get; set; } = firstname;
        public string? Comment { get; set; } = comment;

        public ObservableCollection<ClothesModel> Clothes { get; set; } = [];
    }
}
