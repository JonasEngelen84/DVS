namespace DVS.Domain.Commands.ClothesSize
{
    public interface IDeleteClothesSizeCommand
    {
        Task Execute(Guid guidID);
    }
}
