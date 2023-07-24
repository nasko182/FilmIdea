﻿namespace FilmIdea.Data.Models.Join_Tables;

public class MovieGenre
{
    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public int GenreId { get; set; }

    public Genre Genre { get; set; } = null!;
}