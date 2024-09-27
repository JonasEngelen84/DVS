namespace DVS.Domain.Models
{
    public class EmployeeClothesSize
    {
        public Guid GuidID { get; }
        public Guid EmployeeGuidID { get; private set; }
        public Guid ClothesSizeGuidID { get; private set; }
        public int Quantity { get; private set; }
        public string Comment { get; private set; }
        public Employee Employee { get; private set; }
        public ClothesSize ClothesSize { get; private set; }

        public EmployeeClothesSize(Guid guidID, Employee employee, ClothesSize clothesSize, int quantity, string comment)
        {
            GuidID = guidID;
            EmployeeGuidID = employee.GuidID;
            ClothesSizeGuidID = clothesSize.GuidID;
            Employee = employee;
            ClothesSize = clothesSize;
            Quantity = quantity;
            Comment = comment;
        }

        public EmployeeClothesSize()
        {

        }
    }
}
