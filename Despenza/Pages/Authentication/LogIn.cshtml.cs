using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Authentication
{
    public class LogInModel : PageModel
    {

        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }

        public IActionResult OnPost()
        {
            AuthenticationService system = new AuthenticationService();

            var user = system.Login(Username, Password);

            if (user != null)
            {

                if (user.Role == "Admin")
                {
                    return RedirectToPage("/Admin/Index");
                }
                else if (user.Role == "User")
                {
                    return RedirectToPage("/User/Index");
                }
                else
                {
                    return RedirectToPage("/Authentication/LogIn");
                }
               
            }

            return Page();
        }

        public void OnGet()
        {          

      
        }
    }
}
