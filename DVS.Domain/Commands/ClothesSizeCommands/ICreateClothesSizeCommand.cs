using DVS.Domain.Models;

namespace DVS.Domain.Commands.ClothesSizeCommands
{
    public interface ICreateClothesSizeCommand
    {
        Task Execute(ClothesSize clothesSize);
    }
}
