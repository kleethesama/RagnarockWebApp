using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppWithDatabase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RagnarockWebApp.Data
{
    public class RagnarockWebAppContext : IdentityDbContext<User>
    {
        public RagnarockWebAppContext (DbContextOptions options) : base(options)
        {
        }
    }
}
