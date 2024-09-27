using DVS.Domain.Models;

namespace DVS.Domain.Commands.SeasonCommands
{
    public interface IDeleteSeasonCommand
    {
        Task Execute(Season season);
    }
}
