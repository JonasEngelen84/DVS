namespace DVS.ViewModels.ListViewItems
{
    class EmployeeClothesListViewItemViewModel(int employeeId, string employeeFirstname, string employeeLastname,
        int clothesId, string clothesName, string clothesSize, int clothesQuantity, string comment)
    {
        public int EmployeeId { get; set; } = employeeId;
        public string EmployeeFirstname { get; set; } = employeeFirstname;
        public string EmployeeLastname { get; set; } = employeeLastname;
        public int ClothesId { get; set; } = clothesId;
        public string ClothesName { get; set; } = clothesName;
        public string ClothesSize { get; set; } = clothesSize;
        public int ClothesQuantity { get; set; } = clothesQuantity;
        public string Comment { get; set; } = comment;
    }
}
