using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartParcel.Models;

namespace SmartParcel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Drivers Table
      //  public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<AcceptedOffer> AcceptedOffers { get; set; }
        public DbSet<PendingWork> PendingWorks { get; set; }
        public DbSet<Offer> offers { get; set; }
        public DbSet<MakingOffer> MakingOffers { get; set; }
        public DbSet<SmartParcel.Models.Address> Address { get; set; }
      //  public DbSet<Address> Addresses { get; set; }
    }
}
