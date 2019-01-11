using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CCS.Models;
using CCS.Models.ViewModels.Reports;

namespace CCS.Data
{
    public class ApplicationDbContext : IdentityDbContext<CCSUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<DonationTransaction> DonationTransactions { get; set; }
        public DbSet<DonationType> DonationTypes { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<TransactionLineItem> TransactionLineItems { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbQuery<GroceryQueryModel> GroceryQuery { get; set; }
        public DbSet<CCSUser> CCSUsers { get; set; }

    }
}
