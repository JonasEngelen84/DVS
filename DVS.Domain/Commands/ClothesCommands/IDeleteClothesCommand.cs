using DVS.Domain.Models;

namespace DVS.Domain.Commands.ClothesCommands
{
    public interface IDeleteClothesCommand
    {
        Task Execute(Clothes clothes);
    }
}
