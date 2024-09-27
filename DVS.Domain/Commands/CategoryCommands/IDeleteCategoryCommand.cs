using DVS.Domain.Models;

namespace DVS.Domain.Commands.CategoryCommands
{
    public interface IDeleteCategoryCommand
    {
        Task Execute(Category category);
    }
}
