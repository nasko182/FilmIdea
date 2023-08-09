namespace FilmIdea.Web.Infrastructure.Extensions;

using ViewModels.Actor.Interfaces;
using ViewModels.Director.Interfaces;
using ViewModels.Movie.Interfaces;

public static class ViewModelsExtensions
{
    public static string GetUrlInformation(this IMovieDetailsModel model)
    {
        return model.Title.Replace(" ", "_");
    }

    public static string GetUrlInformation(this IActorDetailsModel model)
    {
        return model.Name.Replace(" ", "_");
    }

    public static string GetUrlInformation(this IDirectorDetailsModel model)
    {
        return model.Name.Replace(" ", "_");
    }
}

