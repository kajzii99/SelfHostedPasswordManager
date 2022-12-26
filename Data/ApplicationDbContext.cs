using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SelfHostedPasswordManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        private DbContextOptions<ApplicationDbContext> options;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> _options)
            : base(_options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}