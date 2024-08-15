namespace DVS.Domain.Commands.Clothes
{
    public interface ICreateClothesCommand
    {
        Task Execute(Models.Clothes clothesModel);
    }
}
