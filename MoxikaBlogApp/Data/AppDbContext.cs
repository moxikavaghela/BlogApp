using Microsoft.EntityFrameworkCore;
using MoxikaBlogApp.Models;

namespace MoxikaBlogApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }

        //Uncomment this if relationships are defined in your models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                
                new Category { Id = 1, Name = "Technology", Description = "All about technology" },
                new Category { Id = 2, Name = "Food", Description = "All about Food-Recipies" },
                new Category { Id = 3, Name = "Consultancy", Description = "All about Consulting" }
                );
            modelBuilder.Entity<Post>().HasData(

                new Post
                {
                    Id = 1,
                    Title = "Tech Post 1",
                    Content = "Content of Tech Post 1",
                    Author = "John Doe",
                    PublishedDate = new DateTime(2023, 1, 1), // Static date instead of DateTime.Now
                    CategoryId = 1,
                    FeatureImagePath = "tech_image.jpg", // Sample image path
                },
                new Post
                {
                    Id = 2,
                    Title = "Health Post 1",
                    Content = "Content of Health Post 1",
                    Author = "Jane Doe",
                    PublishedDate = new DateTime(), // Static date
                    CategoryId = 2,
                    FeatureImagePath = "health_image.jpg", // Sample image path
                },
                new Post
                {
                    Id = 3,
                    Title = "Lifestyle Post 1",
                    Content = "Content of Lifestyle Post 1",
                    Author = "Alex Smith",
                    PublishedDate = new DateTime(), // Static date
                    CategoryId = 3,
                    FeatureImagePath = "lifestyle_image.jpg", // Sample image path
                },
                new Post
                {
                     Id = 4,
                     Title = "Lifestyle Post 2",
                     Content = "Content of Lifestyle Post 2",
                     Author = "Kiana Mancholds",
                     PublishedDate = DateTime.Now, // Static date
                     CategoryId = 3,
                     FeatureImagePath = "lifestyle_image.jpg", // Sample image path
                                }
                );
                //.HasOne(p => p.Category)
                //.WithMany(c => c.Posts)
                //.HasForeignKey(p => p.CategoryId);
            //modelBuilder.Entity<Comments>()
            //    .HasOne(c => c.Post)
            //    .WithMany(p => p.Comments)
            //    .HasForeignKey(c => c.PostId);
        }
    }
}
