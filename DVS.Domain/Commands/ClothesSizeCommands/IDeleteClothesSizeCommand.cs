using DVS.Domain.Models;

namespace DVS.Domain.Commands.ClothesSizeCommands
{
    public interface IDeleteClothesSizeCommand
    {
        Task Execute(ClothesSize clothesSize);
    }
}
