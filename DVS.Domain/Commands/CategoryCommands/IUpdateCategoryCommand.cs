using DVS.Domain.Models;

namespace DVS.Domain.Commands.CategoryCommands
{
    public interface IUpdateCategoryCommand
    {
        Task Execute(Category category);
    }
}
