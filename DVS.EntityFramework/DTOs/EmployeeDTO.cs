using DVS.Domain.Models;
using System.Collections.ObjectModel;

namespace DVS.EntityFramework.DTOs
{
    public class EmployeeDTO
    {
        public Guid GuidID { get; set; }
        public string ID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string? Comment { get; set; }

        public ObservableCollection<EmployeeClothesSize> Clothes { get; set; } = [];
    }
}
