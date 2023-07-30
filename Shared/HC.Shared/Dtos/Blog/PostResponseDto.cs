namespace HC.Shared.Dtos.Blog;

public class PostResponseDto
{
    public string? Title { get; set; } = default!;
    public string? Description { get; set; }
    public byte[]? PreviewImage { get; set; }
    public string? Content { get; set; }
    public DateTime? PublishDate { get; set; }
    public int LikeCount { get; set; }
    public string? AuthorName { get; set; }
    public int CategoryId { get; set; }
}
