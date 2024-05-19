using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RagnarockWebApp.Data;
using RagnarockWebApp.Models;
using WebAppWithDatabase.Models;

namespace RagnarockWebApp.Pages.Users
{
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
            var hasher = new PwdHasher();
            Debug.Assert(hasher.GetHash("qwe") == User[0].Password);
        }
    }
}
