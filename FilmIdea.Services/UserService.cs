﻿namespace FilmIdea.Services.Data;

using Microsoft.EntityFrameworkCore;

using FilmIdea.Data;
using Interfaces;
using Web.ViewModels.User;

public class UserService : IUserService
{
    private readonly FilmIdeaDbContext _dbContext;

    public UserService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<IEnumerable<FullUserViewModel>> AllAsync()
    {
        var allUsers = new List<FullUserViewModel>();

        var critics = await this._dbContext
            .Critics
            .Include(c=>c.User)
            .Select(c => new FullUserViewModel()
            {
                Id = c.UserId.ToString(),
                UserName = c.User.UserName,
                Name = c.Name,
                Email = c.User.Email
            })
            .ToListAsync();

        allUsers.AddRange(critics);

        var users = await this._dbContext
            .Users
            .Where(u => !this._dbContext.Critics.Any(cr => cr.UserId == u.Id))
            .Select(u => new FullUserViewModel()
            {
                Id = u.Id.ToString(),
                UserName = u.UserName,
                Email = u.Email,
                Name = string.Empty
            })
            .ToListAsync();

        allUsers.AddRange(users);

        return allUsers;
    }
}
