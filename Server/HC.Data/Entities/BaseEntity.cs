
namespace HC.Data.Entities;

public abstract class BaseEntity<PK>
{
    public PK Id { get; set; } = default!;
    public DateTimeOffset CreateDate { get; set; }
    public DateTimeOffset? LastModifyDate { get; set; }
    public DateTimeOffset? DeleteDate { get; set; }
    public int LastChangerUserId { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}