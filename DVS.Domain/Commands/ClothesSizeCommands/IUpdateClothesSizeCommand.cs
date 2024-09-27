using DVS.Domain.Models;

namespace DVS.Domain.Commands.ClothesSizeCommands
{
    public interface IUpdateClothesSizeCommand
    {
        Task Execute(ClothesSize clothesSize);
    }
}
