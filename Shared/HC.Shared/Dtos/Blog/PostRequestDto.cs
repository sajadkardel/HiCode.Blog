
namespace HC.Shared.Dtos.Blog;

public class PostRequestDto
{
    public int Id { get; set; }
    public string? Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? PreviewImageName { get; set; }
    public string? Content { get; set; }
    public DateTimeOffset? ScheduledPublishDate { get; set; }
    public int CategoryId { get; set; }
    public int AuthorUserId { get; set; }
}
