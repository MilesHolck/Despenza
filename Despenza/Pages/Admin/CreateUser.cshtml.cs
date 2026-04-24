using DespenzaLib.Data;
using DespenzaLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Despenza.Pages.Admin
{
    public class CreateUserModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateUserModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SelectedUserType { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            User newUser;


            if (SelectedUserType == "Baker")
            {
                newUser = new Baker();
            }
            else
            {
                newUser = new Apprentice();
            }

            newUser.Name = Name;
            newUser.Password = Password;


            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
