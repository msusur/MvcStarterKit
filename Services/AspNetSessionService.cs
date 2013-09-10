using System.Web;
using MvcStarterKit.Abstractions;
using MvcStarterKit.Models;

namespace MvcStarterKit.Services
{
    public class AspNetSessionService : ISessionService
    {
        public UserContext CurrentUser
        {
            get { return HttpContext.Current.Session[":User"] as UserContext; }
            set { HttpContext.Current.Session[":User"] = value; }
        }
    }
}