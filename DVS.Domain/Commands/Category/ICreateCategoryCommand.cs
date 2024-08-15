namespace DVS.Domain.Commands.Category
{
    public interface ICreateCategoryCommand
    {
        Task Execute(Models.Category category);
    }
}
