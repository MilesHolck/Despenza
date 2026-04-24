using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Despenza.Pages.Admin
{
    using DespenzaLib.Data;
    using DespenzaLib.Models;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserListModel : PageModel
    {
        private readonly AppDbContext _context;

        public UserListModel(AppDbContext context)
        {
            _context = context;
        }

        
        public IList<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            
            Users = await _context.Users.ToListAsync();
        }
    }
}
