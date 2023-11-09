namespace Domain.DTOs;

public class PostUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; }
    public string? Title { get; }
    public string? Context { get; }
    public string? Content { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}