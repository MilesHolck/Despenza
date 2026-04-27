using DespenzaLib.Data;
using DespenzaLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Despenza.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class CreateUserModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateUserModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SelectedUserType { get; set; } //make this an enum 

        [BindProperty]
        public string Email { get; set; } 

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet() { }


        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid) return Page();

            User newUser;

            // I din OnPostAsync i CreateUser.cshtml.cs
            if (SelectedUserType == "Baker")
            {
                newUser = new Baker();
                newUser.Role = "Baker";
            }
            else if (SelectedUserType == "Adpprentice") // Hvis du også kan oprette admins her
            {
                newUser = new Apprentice();
                newUser.Role = "Admin";
            }


            if (SelectedUserType == "Baker") // make this a switch case - more readable and maintainable 
            {
                newUser = new Baker();
            }
            else
            {
                newUser = new Apprentice();
            }

            newUser.Name = Name;
            newUser.Email = Email;
            newUser.Password = Password;


            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
