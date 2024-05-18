using System.ComponentModel.DataAnnotations;

namespace WebAppWithDatabase.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(12, MinimumLength = 3), RegularExpression(@"^[a-zA-Z\d]*$")]
        public string? Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }

        /* Must figure out how get regex match with a ".*" for the domain (Example: .com)
         * because right now it doesn't check for a valid email with domain.
         */ 
        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
