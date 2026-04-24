using DespenzaLib.Data;
using DespenzaLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Despenza.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        // Constructor: Denne henter din database-forbindelse ind
        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Find brugeren i databasen
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null) return NotFound();

            // Gem værdierne så de kan vises i HTML
            UserId = user.UserId;
            UserName = user.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Find brugeren baseret på det skjulte ID fra formen
            var userToDelete = await _context.Users.FindAsync(UserId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./UserList");
        }
    }
}
