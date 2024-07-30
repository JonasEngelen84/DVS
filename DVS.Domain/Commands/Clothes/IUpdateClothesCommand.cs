using DVS.Domain.Models;

namespace DVS.Domain.Commands.Clothes
{
    public interface IUpdateClothesCommand
    {
        Task Execute(ClothesModel clothesModel);
    }
}
