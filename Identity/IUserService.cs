using Service.Entities.Identity;

namespace Identity
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();

        Task<User> GetUser(string userID);
    }
}
