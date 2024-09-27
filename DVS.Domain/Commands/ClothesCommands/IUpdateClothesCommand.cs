using DVS.Domain.Models;

namespace DVS.Domain.Commands.ClothesCommands
{
    public interface IUpdateClothesCommand
    {
        Task Execute(Clothes clothes);
    }
}
