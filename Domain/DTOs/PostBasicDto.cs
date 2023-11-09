using Domain.Models;

namespace Domain.DTOs;

public class PostBasicDto
{
    public String  OwnerUsername { get; }
    public string Title { get; }
    public string Content { get; set; }

    public PostBasicDto(String ownerUsername, string title, string content)
    {
        OwnerUsername = ownerUsername;
        Title = title;
        Content = content;
    }
}