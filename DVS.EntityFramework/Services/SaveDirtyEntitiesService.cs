using DVS.Domain.Models;
using DVS.Domain.Services;
using DVS.Domain.Services.Interfaces;

namespace DVS.EntityFramework.Services
{
    public class SaveDirtyEntitiesService(
        DirtyTrackingService dirtyTrackingService,
        DVSDbContextFactory contextFactory)
        : IDirtyEntitySaver
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task SaveDirtyEntitiesAsync()
        {
            using var context = _contextFactory.Create();

            var updateService = new EntityUpdateService(context);

            foreach (var entity in dirtyTrackingService.DirtyEntities)
            {
                entity.IsDirty = false;

                switch (entity)
                {
                    case Clothes clothes:
                        await updateService.UpdateEntityAsync(clothes);
                        break;
                    case ClothesSize clothesSize:
                        await updateService.UpdateEntityAsync(clothesSize);
                        break;
                    case Employee employee:
                        await updateService.UpdateEntityAsync(employee);
                        break;
                    case EmployeeClothesSize employeeClothesSize:
                        await updateService.UpdateEntityAsync(employeeClothesSize);
                        break;
                    case Category category:
                        await updateService.UpdateEntityAsync(category);
                        break;
                    case Season season:
                        await updateService.UpdateEntityAsync(season);
                        break;
                    default:
                        throw new InvalidOperationException($"Unbekannter Entity-Typ: {entity.GetType().Name}");
                }
            }

            await context.SaveChangesAsync();
            dirtyTrackingService.ClearDirtyEntities();
        }
    }

}
