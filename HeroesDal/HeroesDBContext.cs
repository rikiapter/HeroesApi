using HeroesDal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HeroesDal
{
    public class HeroesDBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("Trainer");
            });
            modelBuilder.Entity<Entity>(entity =>
            {
                entity.ToTable("Entity");
            });
        }
            
            public HeroesDBContext(DbContextOptions<HeroesDBContext> options)
    : base(options)
        { }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Entity> Entities { get; set; }
    }
}
