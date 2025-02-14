using DVS.Domain.Models;

namespace DVS.Domain.Services
{
    public interface IDataLoaderService
    {
        Task<List<SizeModel>> LoadSizesAsync();
        Task<List<Category>> LoadCategoriesAsync();
        Task<List<Season>> LoadSeasonsAsync();
        Task<List<Clothes>> LoadClothesAsync();
        Task<List<Employee>> LoadEmployeesAsync();
        Task<List<ClothesSize>> LoadClothesSizesAsync();
        Task<List<EmployeeClothesSize>> LoadEmployeeClothesSizesAsync();
    }
}
