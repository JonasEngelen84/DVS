namespace DVS.Domain.Services.Interfaces
{
    public interface IDirtyEntitySaver
    {
        Task SaveDirtyEntitiesAsync();
    }
}
