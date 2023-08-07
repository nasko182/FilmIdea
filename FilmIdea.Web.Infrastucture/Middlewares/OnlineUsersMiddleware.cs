namespace FilmIdea.Web.Infrastructure.Middlewares;

using System.Collections.Concurrent;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

using Extensions;
using static Common.GeneralApplicationConstants;

public class OnlineUsersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _cookieName;
    private readonly int _lastActivityMinutes;

    private static readonly ConcurrentDictionary<string, bool> AllKeys = new ConcurrentDictionary<string, bool>();

    public OnlineUsersMiddleware(RequestDelegate next,
        string cookieName = OnlineUsersCookieName,
        int lastActivityMinutes = LastActivityBeforeOfflineMinutes)
    {
        this._next = next;
        this._cookieName = cookieName;
        this._lastActivityMinutes = lastActivityMinutes;
    }

    public Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            if (!context.Request.Cookies.TryGetValue(this._cookieName, out string userId))
            {
                userId = context.User.GetId()!;

                context.Response.Cookies.Append(this._cookieName, userId, new CookieOptions()
                {
                    HttpOnly = true,
                    MaxAge = TimeSpan.FromDays(30)
                });
            }

            memoryCache.GetOrCreate(userId, cacheEntry =>
           {
               if (!AllKeys.TryAdd(userId, true))
               {
                   cacheEntry.AbsoluteExpiration = DateTimeOffset.MinValue;
               }
               else
               {
                   cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(this._lastActivityMinutes);
                   cacheEntry.RegisterPostEvictionCallback(this.RemoveKeyWhenExpired);
               }

               return string.Empty;
           });
        }
        else
        {
            if (context.Request.Cookies.TryGetValue(this._cookieName,out string userId))
            {
                if (AllKeys.TryRemove(userId, out _))
                {
                    AllKeys.TryUpdate(userId, false, true);
                }
                
                context.Response.Cookies.Delete(this._cookieName);
            }
        }

        return this._next(context);
    }

    public static bool CheckIfUserIsOnline(string userId)
    {
        bool valueTaken = AllKeys.TryGetValue(userId.ToLower(), out bool success);

        return success && valueTaken;
    }

    private void RemoveKeyWhenExpired(object key, object value, EvictionReason reason, object state)
    {
        string keyStr = (string)key;

        if (AllKeys.TryRemove(keyStr, out _))
        {
            AllKeys.TryUpdate(keyStr, false, true);
        }
    }
}
