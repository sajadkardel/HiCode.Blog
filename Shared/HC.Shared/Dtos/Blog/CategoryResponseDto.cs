namespace HC.Shared.Dtos.Blog;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? IconName { get; set; }
    public List<CategoryResponseDto>? ChildCategories { get; set; }
}