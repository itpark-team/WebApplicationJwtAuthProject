using WebApplicationJwtAuthProject.Db;

namespace WebApplicationJwtAuthProject.Services;

public class UsersService
{
    private MyDbContext _dbContext;

    public UsersService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetUserByUsernameAndPassword(string username, string password)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username && user.Password == password);
    }

    public User GetUserByUsername(string username)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Username == username);
    }
}