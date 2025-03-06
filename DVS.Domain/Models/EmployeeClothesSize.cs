namespace DVS.Domain.Models
{
    public class EmployeeClothesSize : ObservableEntity
    {
        public Guid GuidId { get; set; }
        public Guid ClothesSizeGuidId { get; set; }
        public string EmployeeId { get; set; }
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public Employee Employee { get; set; }
        public ClothesSize ClothesSize { get; set; }

        public EmployeeClothesSize(
            Guid guidId,
            Employee employee,
            ClothesSize clothesSize,
            int quantity,
            string comment)
        {
            GuidId = guidId;
            Employee = employee;
            EmployeeId = employee.Id;
            ClothesSize = clothesSize;
            ClothesSizeGuidId = clothesSize.GuidId;
            Quantity = quantity;
            Comment = comment;
        }

        public EmployeeClothesSize() {}
    }
}
