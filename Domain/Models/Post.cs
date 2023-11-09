using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public User User { get;private set; }
    public string Title { get;private set; }
    public string Content { get; set; }
    
    public Post(string title,string content, User user)
    {
        User = user;
        Title = title;
        Content = content;
    }
    
    private Post(){}
}