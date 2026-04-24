using DespenzaLib.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Despenza.Pages.Authentication
{
    public class LogInModel : PageModel
    {

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public string Message { get; set; } = string.Empty;

        private readonly DespenzaLib.Services.IAuthenticationService _authenticationService;

        public LogInModel(DespenzaLib.Services.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Email og password skal udfyldes.";
                return Page();
            }

            var user = _authenticationService.Login(Email, Password);

            if (user == null)
            {
                Message = "Forkert email eller password.";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                });

            if (user.Role == "Admin")
            {
                return RedirectToPage("/Index");
            }
            if (user.Role == "User") 
            {
                return RedirectToPage("/Index"); 
            }

            return RedirectToPage("/Authentication/AccessDenied");

            Message = "Invalid username or password";
            return Page();
        }

        
    }
}
        //public IActionResult OnPost()
        //{
        //    AuthenticationService system = new AuthenticationService();

        //    var user = system.Login(Email, Password);



        //    if (user != null)
        //    {

        //        if (user.Role == "Admin")
        //        {
        //            return RedirectToPage("/Admin/CreateUser");
        //        }
        //        else if (user.Role == "User")
        //        {
        //            return RedirectToPage("/User/Index");
        //        }
        //        else
        //        {
        //            Message = "Test";
        //            return RedirectToPage("/Authentication/LogIn");
        //        }

        //    }
        //    Message = "Forkert Username eller Password";
        //    return Page();
        //}

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
//        //}

//        public void OnGet()
//        {
//        }
    

//        //public void OnGet()
//        //{          

      
//        //}
//    }
//}
