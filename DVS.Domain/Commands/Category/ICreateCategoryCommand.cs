using DVS.Domain.Models;

namespace DVS.Domain.Commands.Category
{
    public interface ICreateCategoryCommand
    {
        Task Execute(CategoryModel category);
    }
}
