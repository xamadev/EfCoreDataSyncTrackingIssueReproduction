using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Model.Entities;
using Model.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Model.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Unit> Units { get; set; }

        //JOIN-Tables
        public DbSet<UnitToElement> UnitsToElements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("**** OnModelCreating");

            modelBuilder.Entity<Unit>().HasMany(e => e.Elements).WithMany(e => e.Units)
                .UsingEntity<UnitToElement>(
                    b => b.HasOne(e => e.Element).WithMany().HasForeignKey(e => e.FK_Element),
                    b => b.HasOne(e => e.Unit).WithMany().HasForeignKey(e => e.FK_Unit)
                );
        }
    }
}
