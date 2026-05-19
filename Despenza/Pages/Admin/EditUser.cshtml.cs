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
        public int Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Role { get; set; }



        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (id == null) 
                return NotFound();

            Id = user.Id; 
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            Role = user.Role;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userRepo.GetByIdAsync(Id);

            if (user == null) 
                return NotFound();


            user.Name = Name; 
            user.Email = Email;
            user.Password = Password;
            user.Role = Role;

            await _userRepo.UpdateAsync(user);

            
            return RedirectToPage("/Admin/UserList");
        }
    }
}
