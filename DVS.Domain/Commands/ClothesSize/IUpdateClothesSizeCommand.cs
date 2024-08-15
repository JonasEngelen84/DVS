namespace DVS.Domain.Commands.ClothesSize
{
    public interface IUpdateClothesSizeCommand
    {
        Task Execute(Models.ClothesSize clothesSize);
    }
}
