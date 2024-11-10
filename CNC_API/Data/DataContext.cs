using CNC_shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNC_shared.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clientes>().HasIndex(x => x.Documento).IsUnique(true);


            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.Documento);

                entity.Property(e => e.Documento)
                .IsRequired()
                .HasMaxLength(50);

            });

        }
    }
}
