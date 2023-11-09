using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetUserAsync(dto.UserName,dto.Password);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
            
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }

    

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return  userDao.GetAsync(searchParameters);    }

    public async Task<User?> GetUserAsync(string userName, string password)
    {
        return await userDao.GetUserAsync(userName, password);
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return await userDao.GetAllAsync();
    }


    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}