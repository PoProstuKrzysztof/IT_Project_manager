#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IT_Project_manager.Areas.Identity.Pages.Account
{

    

    public class AccessDeniedModel : PageModel
    {
        private readonly ILogger<AccessDeniedModel> _Logger;

        public AccessDeniedModel(ILogger<AccessDeniedModel> logger)
        {
            _Logger = logger;
        }

        public void OnGet()
        {
            var username = HttpContext.User.Identity.Name;
            _Logger.LogError( ( EventId )400, $"{username} not authorized operation on {DateTime.Now}" );
        }
    }
}
