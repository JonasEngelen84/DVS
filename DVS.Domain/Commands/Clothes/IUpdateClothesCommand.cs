namespace DVS.Domain.Commands.Clothes
{
    public interface IUpdateClothesCommand
    {
        Task Execute(Models.Clothes clothesModel);
    }
}
