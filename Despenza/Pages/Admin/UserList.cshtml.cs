using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    using DespenzaLib.Data;
    using DespenzaLib.Models;
    using DespenzaLib.Repos;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserListModel : PageModel
    {
        private readonly IRepository<User> _userRepository; 

        public UserListModel(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        public IList<User> Users { get; set; } = new List<User>();

        public async Task OnGetAsync()
        {

            Users = await _userRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            
            await _userRepository.DeleteAsync(id);

            return RedirectToPage(); 
        }

    }
}
