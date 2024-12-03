namespace ProductService.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }

        public void DeleteAudit(string deletedBy)
        {
            IsDeleted = true;
            DeletedBy = deletedBy;
            ModifiedAt = DateTime.UtcNow;
        }

        public void UpdateAudit(string modifiedBy)
        {
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }
    }
}
