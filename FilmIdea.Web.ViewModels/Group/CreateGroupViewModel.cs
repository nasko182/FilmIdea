﻿namespace FilmIdea.Web.ViewModels.Group;

using User;

public class CreateGroupViewModel
{
    public CreateGroupViewModel()
    {
        this.AllUsers = new HashSet<UserViewModel>();
        this.Icons = new HashSet<IconViewModel>();
    }

    public AddGroupViewModel AddGroupData { get; set; } = null!;

    public ICollection<UserViewModel> AllUsers { get; set; }

    public ICollection<IconViewModel> Icons { get; set; }




}
