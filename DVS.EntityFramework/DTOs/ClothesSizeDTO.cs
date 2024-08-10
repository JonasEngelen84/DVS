using DVS.Domain.Models;
using System.Collections.ObjectModel;

namespace DVS.EntityFramework.DTOs
{
    public class ClothesSizeDTO
    {
        public Guid GuidID { get; set; }
        public Guid ClothesGuidID { get; set; }
        public Guid SizeGuidID { get; set; }
        public int Quantity { get; set; }
        public string? Comment { get; set; }

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; } = [];
    }
}
