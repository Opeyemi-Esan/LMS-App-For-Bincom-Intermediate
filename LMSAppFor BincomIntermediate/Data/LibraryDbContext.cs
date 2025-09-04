using Microsoft.EntityFrameworkCore;

namespace LMSAppFor_BincomIntermediate.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        public DbSet<Models.LibraryUser> LibraryUsers { get; set; }
        public DbSet<Models.Book> Books { get; set; }
        public DbSet<Models.BorrowedBookHistory> BorrowedBookHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Unique constraint on Email
            modelBuilder.Entity<Models.LibraryUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relationships
            modelBuilder.Entity<Models.BorrowedBookHistory>()
                .HasOne(b => b.User)
                .WithMany(u => u.BorrowHistory)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Models.BorrowedBookHistory>()
                .HasOne(b => b.Book)
                .WithMany(bk => bk.BorrowHistories)
                .HasForeignKey(b => b.BookId);
        }
    }
}
