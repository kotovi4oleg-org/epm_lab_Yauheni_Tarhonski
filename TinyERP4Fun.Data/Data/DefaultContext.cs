using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.Models.Stock;
using TinyERP4Fun.Interfaces;

namespace TinyERP4Fun.Data
{
    public class DefaultContext : IdentityDbContext, IDefaultContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CommunicationType> CommunicationType { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<BusinessDirection> BusinessDirection { get; set; }
        public DbSet<CostItem> CostItem { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Expences> Expences { get; set; }
        public DbSet<Currency> Currency { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Sample of UNIQUE Key
            /*builder.Entity<Person>()
                        .HasAlternateKey(c => c.UserId)
                        .HasName("AlternateKey_UserId");*/
            //Sample of UNIQUE Key Or multiple Null
            builder.Entity<Person>()
                        .HasIndex(x => x.UserId)
                        .IsUnique();
            //Sample of UNIQUE Key Or UNIQUE Null
            /*builder.Entity<Product>()
                        .HasIndex(b => b.ProductId)
                        .IsUnique()
                        .HasFilter(null);*/
            //Sample of NoAction on delete
            builder.Entity<CurrencyRates>()
                        .HasOne(p => p.Currency)
                        .WithMany()
                        .HasForeignKey(s => s.CurrencyId)
                        .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Expences>()
                        .HasOne(p => p.OurCompany)
                        .WithMany()
                        .HasForeignKey(s => s.OurCompanyId)
                        .OnDelete(DeleteBehavior.Restrict);/**/

            base.OnModelCreating(builder);

        }  /**/
        
        public DbSet<CurrencyRates> CurrencyRates { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Stock> Stock { get; set; }
    }
    public static class DefaultContextOptions
    {
        public static DbContextOptions<DefaultContext> GetOptions()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<DefaultContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;
            return options;
        }
    }
}
