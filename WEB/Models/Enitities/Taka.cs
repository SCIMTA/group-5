using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace taka.Models.Enitities
{
    public partial class Taka : DbContext
    {
        public Taka()
            : base("name=Taka")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Author)
                .HasForeignKey(e => e.idAuthor);

            modelBuilder.Entity<Book>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.Book)
                .HasForeignKey(e => e.idBook);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.Book)
                .HasForeignKey(e => e.idBook);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Rates)
                .WithOptional(e => e.Book)
                .HasForeignKey(e => e.idBook);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Orders)
                .WithMany(e => e.Books)
                .Map(m => m.ToTable("OrderDetail").MapLeftKey("idBook").MapRightKey("idOrder"));

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.idCategory);

            modelBuilder.Entity<Image>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Language)
                .HasForeignKey(e => e.idLanguage);

            modelBuilder.Entity<Order>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Publisher>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Publisher)
                .HasForeignKey(e => e.idPublisher);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.Type)
                .HasForeignKey(e => e.idType);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Addresses)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.idUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.idUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rates)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.idUser);
        }
    }
}
