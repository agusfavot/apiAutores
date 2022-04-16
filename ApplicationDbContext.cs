using apiVS.Models;
using Microsoft.EntityFrameworkCore;


namespace apiVS
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Mi identidad AutoresLibros va a tener una llave primaria compuesta
            modelBuilder.Entity<AuthorsBooks>().HasKey(al => new { al.AuthorId, al.BookId });
        }

        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<AuthorsBooks> AuthorsBooks { get; set; }

    }
}
