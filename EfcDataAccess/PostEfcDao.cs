using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess;

public class PostEfcDao : IPostDao
{
    private readonly DataContext context;
    
    public PostEfcDao(DataContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParams)
    {
        IQueryable<Post> query = context.Posts.Include(todo => todo.User).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(post =>
                post.User.UserName.ToLower().Equals(searchParams.Username.ToLower()));
        }
    
        if (searchParams.UserId != null)
        {
            query = query.Where(t => t.User.Id == searchParams.UserId);
        }
    
        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParams.TitleContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Post post)
    {
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? found = await context.Posts
            .Include(post => post.User)
            .SingleOrDefaultAsync(post => post.Id == id);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Posts with id {id} not found");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Post>?> GetAllAsync()
    {
        IQueryable<Post> postsQuery = context.Posts.Include(todo => todo.User).AsQueryable();
        IEnumerable<Post> result = await postsQuery.ToListAsync();
        return result;
    }
}