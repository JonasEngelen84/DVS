namespace DVS.Domain.Commands.Clothes
{
    public interface IDeleteClothesCommand
    {
        Task Execute(Guid guidID);
    }
}
