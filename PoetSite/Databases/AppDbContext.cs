using Microsoft.EntityFrameworkCore;
using PoetSite.Areas.Admin.Models;
using PoetSite.Models;

namespace PoetSite.Databases;

public class AppDbContext:DbContext
{

    public DbSet<Poem> Poems => Set<Poem>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<GalleryImage> GalleryImages => Set<GalleryImage>();
    public DbSet<AudioPoem> AudioPoems => Set<AudioPoem>();
    public DbSet<Biography> Biographies => Set<Biography>();
    
    public DbSet<AdminUser> AdminUsers { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        var poem = new Poem()
        {
          Id = 1,
          Title = "Шохномаи Фирдавси",
          Text = "тест",
          CreatedAt = DateTime.Now.Date,
          Language = "TJ",
        };
        modelBuilder.Entity<Poem>(op =>
        {
            op.HasData(poem);
        });
        
        var hash = BCrypt.Net.BCrypt.HashPassword("admin123");

        var admin =new AdminUser
        {
            Id = 1,  
            Username = "admin",
            PasswordHash = hash
        };
        modelBuilder.Entity<AdminUser>(op =>
        {
           op.HasData(admin);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}