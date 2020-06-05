using Chat.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.DataAccess
{
    public class ChatContext : DbContext
    {
        public ChatContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-TALUG4B\SQLEXPRESS;Database=ChatDb;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Login="Galym",
                Password="123123",
                FirstName="Галымжан",
                LastName="Оралбаев"
            });
        }
    }
}
