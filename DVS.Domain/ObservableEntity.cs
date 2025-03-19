using DVS.Domain.Services;

namespace DVS.Domain
{
    public abstract class ObservableEntity
    {
        private bool _isDirty;
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                
                if (_isDirty)
                    GlobalDirtyTrackingService?.AddToDirtyEntities(this);
            }
        }

        public static DirtyTrackingService? GlobalDirtyTrackingService { get; private set; }

        public static void Initialize(DirtyTrackingService store) => GlobalDirtyTrackingService = store;
    }
}
