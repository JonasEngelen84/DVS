namespace DVS.Domain.Models
{
    public class EmployeeClothesSize
    {
        public Guid GuidId { get; }
        public Guid ClothesSizeGuidId { get; private set; }
        public string EmployeeId { get; private set; }
        public int Quantity { get; private set; }
        public string Comment { get; private set; }
        public Employee Employee { get; private set; }
        public ClothesSize ClothesSize { get; private set; }

        public EmployeeClothesSize(Guid guidId, Employee employee, ClothesSize clothesSize, int quantity, string comment)
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
