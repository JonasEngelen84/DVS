using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesSize
    {
        public Guid GuidID { get; }
        public Guid ClothesGuidID { get; private set; }
        public Guid SizeGuidID { get; private set; }
        public int Quantity { get; private set; }
        public string Comment { get; private set; }
        public SizeModel Size { get; private set; }
        public Clothes Clothes { get; private set; }

        public ObservableCollection<EmployeeClothesSize> EmployeeClothesSizes { get; set; }

        public ClothesSize(Guid guidID, Clothes clothes, SizeModel size, int quantity, string comment)
        {
            GuidID = guidID;
            ClothesGuidID = clothes.GuidID;
            SizeGuidID = size.GuidID;
            Size = size;
            Quantity = quantity;
            Comment = comment;

            EmployeeClothesSizes = [];
        }

        public ClothesSize()
        {

        }
    }
}
