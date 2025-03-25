using System.Diagnostics;

namespace DVS.Domain.Services
{
    public class DirtyTrackingService
    {
        private readonly HashSet<ObservableEntity> _dirtyEntities = [];
        public HashSet<ObservableEntity> DirtyEntities => _dirtyEntities;

        public bool HasUnsavedChanges => _dirtyEntities.Count > 0;

        public void AddDirtyEntity(ObservableEntity entity)
        {
            _dirtyEntities.Remove(entity);
            _dirtyEntities.Add(entity);
        }

        public void ClearDirtyEntities()
        {
            _dirtyEntities.Clear();
        }
    }
}
