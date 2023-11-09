﻿using System.Net.Http.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient: IUserService
{
    
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task RegisterUserAsync(UserLoginDto dto)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync("/users", dto);
        string result = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
    }

}