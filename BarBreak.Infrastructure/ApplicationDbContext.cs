namespace BarBreak.Infrastructure;

public class ApplicationDbContext : DbContext
{
    // Entity datasets
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<CourseEntity> Courses { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

    // Конструктор для DI контейнера
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Виконуйте цю конфігурацію лише для сценаріїв, коли контекст використовується без DI
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("DB_CONNECTION_STRING environment variable is not set.");
            }

            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    // Model configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
