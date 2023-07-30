namespace HC.Shared.Dtos.Blog;

public class CategoryRequestDto
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; } = default!;
    public string? IconName { get; set; }
}
