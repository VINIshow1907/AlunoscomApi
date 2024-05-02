﻿namespace AlunoApi.Services
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string password);
        Task Logout();
    }
}