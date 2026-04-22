using DespenzaLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.User
{
    public class EditModel : PageModel
    {
        private static string OriginalName { get; set; }
        [BindProperty]
        public Baker Baker { get; set; }

        public void OnGet(string name)
        {
            Baker = new Baker();

            OriginalName = name;
            Baker.Name = name;
        }

        public IActionResult OnPost()
        {
            var index = DatabaseTemporaryReplacement.Users.FindIndex(u => u.Name == OriginalName);

            DatabaseTemporaryReplacement.Users[index] = Baker;

            return Redirect("/User/AllUsers");
        }
    }
}
