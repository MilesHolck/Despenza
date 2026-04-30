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
    public class CreateUserModel : PageModel
    {
        private readonly IRepository<User> _userRepo; 

        public CreateUserModel(IRepository<User> userRepo)
        {
            _userRepo = userRepo;   
        }

        [BindProperty]
        public string SelectedUserType { get; set; } //make this an enum?

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

            // Tilføj dette tjek i toppen:
            if (SelectedUserType == "Admin")
            {
                newUser = new DespenzaLib.Models.Admin();
                newUser.Role = "Admin";
            }
            else if (SelectedUserType == "Baker")
            {
                newUser = new Baker();
                newUser.Role = "User"; 
            }
            else if (SelectedUserType == "Apprentice") 
            {
                newUser = new Apprentice();
                newUser.Role = "User";
            }
            else
            {
                
                newUser = new Baker();
            }

            
            newUser.Name = Name;
            newUser.Email = Email;
            newUser.Password = Password;


            _userRepo.AddAsync(newUser);

            return Page();
        }
    }
}
