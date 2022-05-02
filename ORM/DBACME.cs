using Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORM
{
    public partial class DBACME : DbContext
    {
        public DBACME()
            : base("name=DBACME")
        {
        }

        public virtual DbSet<Authentification> Authentifications { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<AddToBasket> AddToBaskets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authentification>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Authentification>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Authentification>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Authentification>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Authentification>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Authentification>()
                .HasMany(e => e.Baskets)
                .WithOptional(e => e.Authentification)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Basket>()
                .Property(e => e.Invoice)
                .IsUnicode(false);

            modelBuilder.Entity<Basket>()
                .Property(e => e.TotalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Basket>()
                .HasMany(e => e.AddToBaskets)
                .WithRequired(e => e.Basket)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("AddToCategory").MapLeftKey("CategoryID").MapRightKey("ProductID"));

            modelBuilder.Entity<Product>()
                .Property(e => e.Reference)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.AddToBaskets)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductImages)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductImage>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<ProductImage>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<AddToBasket>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<AddToBasket>()
                .Property(e => e.Width)
                .IsUnicode(false);
        }
    }
}
