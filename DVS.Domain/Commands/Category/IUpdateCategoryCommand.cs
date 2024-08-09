using DVS.Domain.Models;

namespace DVS.Domain.Commands.Category
{
    public interface IUpdateCategoryCommand
    {
        Task Execute(Models.Category category);
    }
}
