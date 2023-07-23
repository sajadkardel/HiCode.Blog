namespace HC.Shared.Dtos.Blog;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? IconName { get; set; }
    public int? ParentId { get; set; }
}