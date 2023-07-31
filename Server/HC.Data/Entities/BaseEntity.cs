
namespace HC.Data.Entities;

public abstract class BaseEntity<PK>
{
    public PK Id { get; set; } = default!;
    public DateTime? CreateDate { get; set; } // Todo: should not nullable
    public DateTime? LastModifyDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public int? LastChangerUserId { get; set; } // Todo: should not nullable
    public bool IsDeleted { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}