using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Authentication
{
    public class LogInModel : PageModel
    {

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public string Message { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            AuthenticationService system = new AuthenticationService();

            var user = system.Login(Email, Password);



            if (user != null)
            {

                if (user.Role == "Admin")
                {
                    return RedirectToPage("/Admin/CreateUser");
                }
                else if (user.Role == "User")
                {
                    return RedirectToPage("/User/Index");
                }
                else
                {
                    Message = "Test";
                    return RedirectToPage("/Authentication/LogIn");
                }

            }
            Message = "Forkert Username eller Password";
            return Page();
        }

        //public IActionResult OnPost()
        //{
        //    AuthenticationService system = new AuthenticationService();

        //    var user = system.Login(Email, Password);

        //    if (user == null)
        //    {
        //        return Content("User er null - login fejler");
        //    }

        //    if (user.Role == "Admin")
        //    {
        //        return Content("Login virker - Admin");
        //    }
        //    else if (user.Role == "User")
        //    {
        //        return Content("Login virker - User");
        //    }

        //    return Content("Login virker - men rolle matcher ikke");
        //}

        public void OnGet()
        {
        }
    

        //public void OnGet()
        //{          

      
        //}
    }
}
