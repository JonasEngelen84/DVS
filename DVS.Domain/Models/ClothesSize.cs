using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize(Guid guidID, Clothes clothes, SizeModel size, int quantity, string? comment)
    {
        public Guid GuidID { get; } = guidID;
        public Guid ClothesGuidID { get; } = clothes.GuidID;
        public Guid SizeGuidID { get; } = size.GuidID;
        public SizeModel Size { get; } = size;
        public Clothes Clothes { get; } = clothes;
        public int Quantity { get; } = quantity;
        public string? Comment { get; } = comment;

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; } = [];
    }
}
