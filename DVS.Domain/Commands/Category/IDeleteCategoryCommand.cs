namespace DVS.Domain.Commands.Category
{
    public interface IDeleteCategoryCommand
    {
        Task Execute(Guid guidID);
    }
}
