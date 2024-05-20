using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppWithDatabase.Models;

namespace RagnarockWebApp.Pages
{
    public class UserLoginModel : PageModel
    {
        private readonly Data.RagnarockWebAppContext _context;
        private readonly Models.PwdVerifier _verifier;

        public UserLoginModel(Data.RagnarockWebAppContext context, Models.PwdVerifier verifier)
        {
            _context = context;
            _verifier = verifier;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User UserInput { get; set; } = default!;
        public IList<User> Users { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UserInput.Email is not null)
            {
                throw new Exception("The email input is not null for some reason.");
            }

            var users = from u in _context.User select u;
            var userSearch = users.Where(s => s.Username.Contains(UserInput.Username));
            var usersFound = await userSearch.ToListAsync();

            //Users = await _context.User.ToListAsync();


            //SignIn
            //foreach (User user in Users)
            //{
            //    if (UserInput.Username.ToLower() == user.Username.ToLower())
            //    {
            //        bool isPasswordCorrect = _verifier.VerifyHash(UserInput.Password, user.Password);
            //        if (isPasswordCorrect)
            //        {
            //            _context.User.
            //            this.SignIn();
            //        }
            //    }
            //}

            //User.Password = _hasher.GetHash(User.Password ?? throw new NullReferenceException("User password input is null!"));

            //_context.User.Add(User);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}