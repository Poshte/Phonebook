using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.DAL
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> options)
            : base(options)
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityUserClaim<string>> Claims { get; set; }
    }
}
