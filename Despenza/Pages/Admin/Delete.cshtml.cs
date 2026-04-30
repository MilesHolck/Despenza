using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Despenza.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IRepository<User> _userRepo; 

        public DeleteModel(IRepository<User> userRepo)
        {
            _userRepo = userRepo;   
        }

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null) return NotFound();

            // Find brugeren i databasen
            var user = await _userRepo.GetByIdAsync(id); 

            if (user == null) return NotFound();

            // Gem værdierne så de kan vises i HTML
            UserId = user.UserId;
            UserName = user.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Find brugeren baseret på det skjulte ID fra formen
            var userToDelete = _userRepo.DeleteAsync(UserId);

            if (userToDelete != null)
            {
                return NotFound();
            }

            return RedirectToPage("./UserList");
        }
    }
}
