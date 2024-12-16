namespace BarBreak.Infrastructure;

public class ApplicationDbContext : DbContext
{
    // Entity datasets
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<CourseEntity> Courses { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Host=localhost;Port=5432;Database=BarBreak;Username=postgres;Password=1909";
            
        }
    }

    // Model configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
