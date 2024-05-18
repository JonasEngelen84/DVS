namespace DVS.Models
{
    public class EmployeeModel(int id, string firstname, string lastname)
    {
        public int Id { get; set; } = id;
        public string Firstname { get; set; } = firstname;
        public string Lastname { get; set; } = lastname;
        public string Comment { get; set; } = "";
        public List<ClothesModel> EmployeeClothesList = [];
    }

}
