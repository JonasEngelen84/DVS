using DVS.Domain.Models;

namespace DVS.Domain.Commands.SeasonCommands
{
    public interface IUpdateSeasonCommand
    {
        Task Execute(Season season);
    }
}
