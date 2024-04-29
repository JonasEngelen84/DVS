namespace DVS.Models
{
    public class EmployeeClothesModel(int employeeId, string employeeFirstname,
        string employeeLastname, int clothesId, string clothesName, string size, int quantity)
    {
        public int EmployeeId { get; set; } = employeeId;
        public string EmployeeFirstname { get; set; } = employeeFirstname;
        public string EmployeeLastname { get; set; } = employeeLastname;
        public int ClothesId { get; set; } = clothesId;
        public string ClothesName { get; set; } = clothesName;
        public string Size { get; set; } = size;
        public int Quantity { get; set; } = quantity;
        public string Comment { get; set; } = "";
    }

}
