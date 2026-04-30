using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Despenza.Pages.Admin
{
    [Authorize(Roles = "Admin")] 
    public class EditModel : PageModel
    {
        private readonly IRepository<User> _userRepo; 

        public EditModel(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        [BindProperty]
        public User UserToEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null) return NotFound();

            UserToEdit = await _userRepo.GetByIdAsync(id);  

            if (UserToEdit == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Fortæller EF at dette objekt skal opdateres i databasen
            await _userRepo.UpdateAsync(UserToEdit); 

            
            return RedirectToPage("./UserList");
        }
    }
}
