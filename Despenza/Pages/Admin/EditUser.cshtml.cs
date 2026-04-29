using DespenzaLib.Data;
using DespenzaLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Despenza.Pages.Admin
{
    [Authorize(Roles = "Admin")] 
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context) => _context = context;

        [BindProperty]
        public User UserToEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            UserToEdit = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);

            if (UserToEdit == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Fortæller EF at dette objekt skal opdateres i databasen
            _context.Attach(UserToEdit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.UserId == UserToEdit.UserId)) return NotFound();
                else throw;
            }

            return RedirectToPage("./UserList");
        }
    }
}
