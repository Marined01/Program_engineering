namespace BarBreak.Infrastructure.Repositories
{
    using BarBreak.Core.User;
    public class UserRepository(ApplicationDbContext dbContext)
        : RepositoryBase<int, UserEntity, ApplicationDbContext>(dbContext),
            IUserRepository;
}
