using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Domain.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserDao iUserDao;

    private readonly IList<User> users = new List<User>()
    {
new User()
{
    UserName = "ricardo",
    Password = "123321"
}
    };

    public AuthService(IUserDao iUserDao)
    {
        this.iUserDao = iUserDao;
    }
    
    public Task<User> ValidateUser(string username, string password)
    {

        Task<User?> existingUser = iUserDao.GetUserAsync(username, password);
        

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Result.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
    

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        
        //users.Add(user);
        
        return Task.CompletedTask;
    }
}