﻿namespace FilmIdea.Common;

public static class GeneralApplicationConstants
{
    public const int DefaultPage = 1;
    public const int EntitiesPerPage = 5;

    public const string AdminAreaName = "Admin";
    public const string AdminRoleName = "Administrator";
    public const string DevelopmentAdminEmail = "admin@admin.com";

    public const string UserCacheKey = "UsersCache";
    public const int UserCacheKeyExpirationMinutes = 5;

    public const string OnlineUsersCookieName = "IsOnline";
    public const int LastActivityBeforeOfflineMinutes = 10;
}
