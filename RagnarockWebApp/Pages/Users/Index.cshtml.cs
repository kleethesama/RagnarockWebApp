using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RagnarockWebApp.Data;
using WebAppWithDatabase.Models;

namespace RagnarockWebApp.Pages.Users
{
    [Authorize(Policy = "RegisteredUser")]
    public class IndexModel : PageModel
    {
        private readonly RagnarockWebApp.Data.RagnarockWebAppContext _context;

        public IndexModel(RagnarockWebApp.Data.RagnarockWebAppContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
