namespace DVS.Domain.Models
{
    public class EmployeeClothesSize(Guid guidID, Employee employee, ClothesSize clothesSize, int quantity)
    {
        public Guid GuidID { get; } = guidID;
        public Guid EmployeeGuidID { get; } = employee.GuidID;
        public Guid ClothesSizeGuidID { get; } = clothesSize.GuidID;
        public Employee Employee { get; } = employee;
        public ClothesSize ClothesSize { get; } = clothesSize;
        public int Quantity { get; set; } = quantity;
        public string? Comment { get; set; } = "";
    }
}
