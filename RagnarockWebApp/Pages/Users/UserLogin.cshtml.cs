using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppWithDatabase.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RagnarockWebApp.Pages.Users
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

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public User UserInput { get; set; } = default!;

        public IList<User> Users { get; set; } = default!;

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            ReturnUrl = returnUrl;
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Use Input.Email and Input.Password to authenticate the user
            // with your custom authentication logic.
            //
            // For demonstration purposes, the sample validates the user
            // on the email address maria.rodriguez@contoso.com with 
            // any password that passes model validation.

            var user = await AuthenticateUser(UserInput);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            var claims = new List<Claim>
                {
                    new Claim("Username", user.Username),
                    new Claim(ClaimTypes.Role, "User")
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            //_logger.LogInformation("User {Email} logged in at {Time}.",
            //    user.Email, DateTime.UtcNow);

            //return LocalRedirect(Pages.IndexModel);
            return RedirectToPage("./Index");
        }

            //User.Password = _hasher.GetHash(User.Password ?? throw new NullReferenceException("User password input is null!"));

        private async Task<User> AuthenticateUser(User userInput)
        {
            // For demonstration purposes, authenticate a user
            // with a static email address. Ignore the password.
            // Assume that checking the database takes 500ms

            await Task.Delay(500);

            if (userInput.Email is not null)
            {
                throw new Exception("The email input is not null for some reason.");
            }

            var users = from u in _context.User select u;
            var userSearch = users.Where(s => s.Username.Contains(userInput.Username)); // This search is supposedly case insensitive.
            var usersFound = await userSearch.ToListAsync();

            if (usersFound.Any())
            {
                bool isPasswordCorrect = _verifier.VerifyHash(userInput.Password, usersFound[0].Password);
                if (isPasswordCorrect)
                {
                    return new User()
                    {
                        Username = UserInput.Username,
                        Password = UserInput.Password
                    };
                }
            }
            return null;
        }
    }
}
