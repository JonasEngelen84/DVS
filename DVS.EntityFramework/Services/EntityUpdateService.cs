namespace DVS.EntityFramework.Services
{
    public class EntityUpdateService(DVSDbContext context)
    {
        private readonly DVSDbContext _context = context;

        public async Task UpdateEntityAsync<T>(T entity) where T : class
        {
            var dbEntity = await _context.Set<T>().FindAsync(entity.GetType().GetProperty("Id")?.GetValue(entity));

            if (dbEntity != null)
            {
                _context.Entry(dbEntity).CurrentValues.SetValues(entity);
            }
        }
    }
}
