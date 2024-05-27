using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppWithDatabase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RagnarockWebApp.Data
{
    public class RagnarockWebAppContext : DbContext
    {
        public RagnarockWebAppContext (DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<WebAppWithDatabase.Models.User> User { get; set; } = default!;
    }
}
