using DespenzaLib.Data;
using DespenzaLib.Models;
using DespenzaLib.Repos;
using DespenzaLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

           
            var user = await _userRepo.GetByIdAsync(id); 

            if (user == null) return NotFound();

           
            UserId = user.Id;
            UserName = user.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            await _userRepo.DeleteAsync(UserId);


            return RedirectToPage("./UserList");
        }
    }
}
