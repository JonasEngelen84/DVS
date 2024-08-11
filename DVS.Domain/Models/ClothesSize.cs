using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize(Guid guidID, Clothes clothes, SizeModel size, int quantity)
    {
        public Guid GuidID { get; } = guidID;
        public Guid ClothesGuidID { get; } = clothes.GuidID;
        public Guid SizeGuidID { get; } = size.GuidID;
        public SizeModel Size { get; } = size;
        public Clothes Clothes { get; } = clothes;
        public int Quantity { get; set; } = quantity;
        public string? Comment { get; set; } = "";

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; } = [];
    }
}
