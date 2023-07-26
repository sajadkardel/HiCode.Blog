namespace HC.Shared.Dtos.Blog;

public class CategoryRequestDto
{
    public int? ParentId { get; set; }
    public string Name { get; set; } = default!;
    public string? IconName { get; set; }
}
