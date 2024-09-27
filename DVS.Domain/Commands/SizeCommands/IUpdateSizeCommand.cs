using DVS.Domain.Models;

namespace DVS.Domain.Commands.SizeCommands
{
    public interface IUpdateSizeCommand
    {
        Task Execute(SizeModel size);
    }
}
