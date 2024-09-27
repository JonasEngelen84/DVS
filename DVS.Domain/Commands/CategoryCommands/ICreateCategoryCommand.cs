using DVS.Domain.Models;

namespace DVS.Domain.Commands.CategoryCommands
{
    public interface ICreateCategoryCommand
    {
        Task Execute(Category category);
    }
}
