﻿namespace FilmIdea.Common;

public static class ExceptionMessages
{
    public static string InvalidInputErrorMessage(string input) => $"Invalid {input}. Please check your data and try again";

    public static string GeneralErrorMessage => "Unexpected error occurred! Please try again later or contact administrator";

    public static string UnauthorizedAccessErrorMessage => "You must become an critic in order to add new review";

    public static string AlreadyCriticErrorMessage => "You are already an critic";

    public static string InvalidDateOfBirthErrorMessage => "Please enter an valid date of birth or contact administrator";

    public static string UsersDoNotExistErrorMessage => "One or more users don't exist.";

    public static string InvalidLogin =>
        "There was an error while logging you in! Please try again later or contact administrator!";

    public static string InvalidToken =>
        "Invalid Dropbox token! Please contact creator of the project for new one :)";


}
