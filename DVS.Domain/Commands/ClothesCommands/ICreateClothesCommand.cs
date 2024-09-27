using DVS.Domain.Models;

namespace DVS.Domain.Commands.ClothesCommands
{
    public interface ICreateClothesCommand
    {
        Task Execute(Clothes clothes);
    }
}
