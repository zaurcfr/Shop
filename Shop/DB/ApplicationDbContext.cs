using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(i => new { i.AuthorId, i.BookId });
            modelBuilder.Entity<BookAuthor>().HasOne(i => i.Book).WithMany(i => i.BookAuthors).HasForeignKey(i => i.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(i => i.Author).WithMany(i => i.BookAuthors).HasForeignKey(i => i.AuthorId);
            modelBuilder.Entity<Student>().HasOne(i => i.StudentAddress).WithOne(i => i.Student).HasForeignKey<StudentAddress>(i=>i.StudentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
