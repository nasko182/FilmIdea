﻿namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.User;

public interface IUserService
{

    Task<IEnumerable<FullUserViewModel>> AllAsync();

}
