namespace Prommerce.Data.Entites
{
    public interface ITrackedEntity
    {
        DateTimeOffset CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
    }

    public interface ISoftDeleteEntity
    {
        bool Deleted { get; set; }
    }

    public class BaseEntity
    {
        public Guid Id { get; set; }
    }

    public class TrackedWithoutId : ITrackedEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Tracked : BaseEntity, ITrackedEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class SoftDelete : ISoftDeleteEntity
    {
        public bool Deleted { get; set; }
    }

    public class TrackedAndSoftDelete : BaseEntity, ITrackedEntity, ISoftDeleteEntity
    {
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }
    }
}