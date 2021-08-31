using HoraExtra.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoraExtra
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Funcionario> Funcionario { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>().HasKey(func => func.Id);
        }
    }
}
