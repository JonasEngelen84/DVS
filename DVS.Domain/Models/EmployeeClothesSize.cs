namespace DVS.Domain.Models
{
    public class EmployeeClothesSize(Employee employee, ClothesSize clothesSize, int quantity)
    {
        public Guid GuidID { get; } = Guid.NewGuid();
        public Guid EmployeeGuidID { get; } = employee.GuidID;
        public Guid ClothesSizeGuidID { get; } = clothesSize.GuidID;
        public int Quantity { get; set; } = quantity;
        public string Comment { get; set; }

        public Employee Employee { get; } = employee;
        public ClothesSize ClothesSize { get; } = clothesSize;
    }
}
