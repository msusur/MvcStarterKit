using System.Web.Mvc;
using System.Web.Routing;
using MvcStarterKit.Abstractions;

namespace MvcStarterKit.ActionFilters
{
    /// <summary>
    /// Marks the controllers or actions if they are secured. 
    /// </summary>
    public class SecureActionFilter : ActionFilterAttribute
    {
        private readonly ISessionService _sessionService;

        public SecureActionFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public SecureActionFilter()
            : this(Bootstrapper.GetService<ISessionService>())
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_sessionService.CurrentUser == null)
            {
                var mergedRouteValues = new RouteValueDictionary();
                mergedRouteValues["action"] = "Login";
                mergedRouteValues["controller"] = "Login";
                filterContext.Result = new RedirectToRouteResult(mergedRouteValues);
            }
        }
    }
}