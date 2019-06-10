using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyERP4Fun.Models.Common;
using TinyERP4Fun.Models.Expenses;
using TinyERP4Fun.Models.Stock;

namespace TinyERP4Fun.Data
{
    public class DefaultContext : IdentityDbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }
        public DbSet<TinyERP4Fun.Models.Common.Country> Country { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.State> State { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.City> City { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.CommunicationType> CommunicationType { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.Communication> Communication { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.Position> Position { get; set; }
        public DbSet<TinyERP4Fun.Models.Expenses.BusinessDirection> BusinessDirection { get; set; }
        public DbSet<TinyERP4Fun.Models.Expenses.CostItem> CostItem { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.Person> Person { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.Company> Company { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.Employee> Employee { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.Department> Department { get; set; }
        public DbSet<TinyERP4Fun.Models.Expenses.DocumentType> DocumentType { get; set; }
        public DbSet<TinyERP4Fun.Models.Expenses.Expences> Expences { get; set; }
        public DbSet<TinyERP4Fun.Models.Common.Currency> Currency { get; set; }
        
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
        
        public DbSet<TinyERP4Fun.Models.Common.CurrencyRates> CurrencyRates { get; set; }
        
        public DbSet<TinyERP4Fun.Models.Stock.Unit> Unit { get; set; }
        
        public DbSet<TinyERP4Fun.Models.Stock.Warehouse> Warehouse { get; set; }
        
        public DbSet<TinyERP4Fun.Models.Stock.Item> Item { get; set; }
        
        public DbSet<TinyERP4Fun.Models.Stock.Stock> Stock { get; set; }
    }
}
