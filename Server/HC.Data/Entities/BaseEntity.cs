namespace HC.Data.Entities;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}
