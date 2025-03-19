using DVS.Domain.Models;

namespace DVS.Domain.Services.Interfaces
{
    public interface IDataLoaderService
    {
        Task<List<Category>> LoadCategoriesAsync();
        Task<List<Season>> LoadSeasonsAsync();
        Task<List<Clothes>> LoadClothesAsync();
        Task<List<Employee>> LoadEmployeesAsync();
        Task<List<ClothesSize>> LoadClothesSizesAsync();
        Task<List<EmployeeClothesSize>> LoadEmployeeClothesSizesAsync();
    }
}
