namespace DVS.Domain.Commands.ClothesSize
{
    public interface ICreateClothesSizeCommand
    {
        Task Execute(Models.ClothesSize clothesSize);
    }
}
