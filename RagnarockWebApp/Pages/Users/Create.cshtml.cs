using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RagnarockWebApp.Data;
using RagnarockWebApp.Interfaces;
using WebAppWithDatabase.Models;

namespace RagnarockWebApp.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly RagnarockWebApp.Data.RagnarockWebAppContext _context;
        private readonly RagnarockWebApp.Interfaces.IPwdHasher _hasher;

        public CreateModel(RagnarockWebApp.Data.RagnarockWebAppContext context, IPwdHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User.Password = _hasher.GetHash(User.Password);

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
