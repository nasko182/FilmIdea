﻿namespace FilmIdea.Data.Models.Join_Tables;

public class MovieActor
{
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public int ActorId { get; set; }

    public Actor Actor { get; set; } = null!;
}