namespace DVS.Domain.Models
{
    public class EmployeeClothesSize
    {
        public Guid GuidId { get; }
        public Guid ClothesSizeGuidId { get; }
        public string EmployeeId { get; }
        public int Quantity { get; }
        public string Comment { get; }
        public Employee Employee { get; }
        public ClothesSize ClothesSize { get; }

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
