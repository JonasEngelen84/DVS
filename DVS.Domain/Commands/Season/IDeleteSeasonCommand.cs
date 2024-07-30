namespace DVS.Domain.Commands.Season
{
    public interface IDeleteSeasonCommand
    {
        Task Execute(Guid GuidID);
    }
}
