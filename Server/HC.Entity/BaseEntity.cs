namespace HC.Entity;

//public interface IEntity
//{
//}

//public interface IEntity<TKey> : IEntity
//{
//    public TKey Id { get; set; }
//    public DateTime CreateDate { get; set; }
//    public long CreatorUserId { get; set; }
//    public DateTime? LastModifiedDate { get; set; }
//    public long? LastModifierUserId { get; set; }
//    public bool IsDeleted { get; set; }
//}

public abstract class BaseEntity<TKey> /*: IEntity<TKey>*/
{
    public TKey Id { get; set; }
    public DateTime CreateDate { get; set; }
    public long CreatorUserId { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public long? LastModifierUserId { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}
