using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.User
{
    public class DeleteModel : PageModel
    {
        public void OnGet(string name)
        {
            var Index = DatabaseTemporaryReplacement.Users.FindIndex(u => u.Name == name);
            DatabaseTemporaryReplacement.Users.RemoveAt(Index);
        }
    }
}
