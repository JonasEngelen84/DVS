using System.Collections.Generic;
using System.Threading.Tasks;
using DVS.Domain.Models;

namespace DVS.Domain.Commands.Category
{
    public interface ICreateCategoryCommand
    {
        Task Execute(Models.Category category);
    }
}
