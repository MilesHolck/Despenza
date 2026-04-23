using DespenzaLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.User
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Baker Baker { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DatabaseTemporaryReplacement.Users.Add(Baker);

            return Redirect("/User/AllUsers");
        }
    }
}
