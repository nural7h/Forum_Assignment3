using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<ICollection<Post>> ViewAllPostsAsync();
    Task<PostBasicDto> GetPostByIdAsync(int id);

    Task CreatePost(PostCreationDto postCreationDto);
}