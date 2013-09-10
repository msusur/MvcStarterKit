using MvcStarterKit.Models;

namespace MvcStarterKit.Abstractions
{
    /// <summary>
    /// Session helper service for user context and stuff.
    /// </summary>
    public interface ISessionService
    {
        UserContext CurrentUser { get; set; }
    }
}
