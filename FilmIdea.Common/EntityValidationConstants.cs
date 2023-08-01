namespace FilmIdea.Common;

public static class EntityValidationConstants
{
    public static class MovieValidations
    {
        public const int TitleMaxLength = 255;
        public const int TitleMinLength = 3;

        public const int DescriptionMaxLength = 1000;

        public const int CoverImageUrlMaxLength = 2048;

        public const int TrailerUrlMaxLength = 2048;
    }

    public static class ActorValidations
    {
        public const int NameMaxLength = 105;

        public const int BioMaxLength = 10000;

        public const int ProfileImageUrlMaxLength = 2048;

    }

    public static class DirectorValidations
    {
        public const int NameMaxLength = 105;

        public const int BioMaxLength = 10000;

        public const int ProfileImageUrlMaxLength = 2048;

    }

    public static class CriticValidations
    {
        public const int NameMaxLength = 105;
        public const int NameMinLength = 3;

        public const int BioMaxLength = 10000;
        public const int BioMinLength = 3;

        public const int ProfileImageUrlMaxLength = 2048;
    }

    public static class GenreValidations
    {
        public const int NameMaxLength = 30;
    }

    public static class GroupValidations
    {
        public const int NameMaxLength = 150;
        public const int NameMinLength = 3;
    }

    public static class MessageValidations
    {
        public const int ContentMaxLength = 255;
        public const int ContentMinLength = 1;

        public const int GroupIdLength = 36;
    }

    public static class ReviewValidations
    {
        public const string RatingMaxValue = "10";
        public const string RatingMinValue = "0";

        public const int ContentMaxLength = 1000;
        public const int ContentMinLength = 1;

        public const int TitleMaxLength = 255;
        public const int TitleMinLength = 3;
    }

    public static class CommentValidations
    {
        public const int ContentMaxLength = 255;
        public const int ContentMinLength = 1;
    }

    public static class PhotoValidations
    {
        public const int UrlMaxLength = 2048;
    }

    public static class VideoValidations
    {
        public const int UrlMaxLength = 2048;
    }
}