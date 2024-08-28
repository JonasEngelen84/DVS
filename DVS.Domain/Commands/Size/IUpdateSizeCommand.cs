namespace DVS.Domain.Commands.Size
{
    public interface IUpdateSizeCommand
    {
        Task Execute(Models.SizeModel size);
    }
}
