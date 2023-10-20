using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Wales_Online_Bank.Models;

namespace Wales_Online_Bank.Data
{
    public class AppDbContext : IdentityDbContext<CustomerUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       public DbSet<Account>Accounts  { get; set; }
        public DbSet<CustomerUser> CustomerUsers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Account>()
            //.HasOne(a => a.CustomerUser)
            //.WithOne(c => c.Account)
            //.HasForeignKey<Account>(a => a.CustomerUserId);



        }

    }
}
 