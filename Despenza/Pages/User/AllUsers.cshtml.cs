using DespenzaLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.User
{
    public class AllUsersModel : PageModel
    {
        public List<Baker> Users { get; set; }
        public void OnGet()
        {
            Users = DatabaseTemporaryReplacement.Users;
        }
    }
}
