using Application.Pagination;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetAllUsersPaged(int pageNumber, int pageSize);
        Task<PagedList<User>> GetSoftDeletedUsersPaged(int pageNumber, int pageSize);
        Task<User?> GetUserById(int id);
        Task<User?> GetAnyUserByPhoneNumber(string phoneNumber);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetAnyUserByEmail(string email);
        Task<User?> GetAnyUserById(int userId);
        Task<User?> GetUserByAddressId(int addressId);
        Task<PagedList<User>> SearchUserByName(string userName, int pageNumber, int pageSize);
        Task AddUser(User newUser);
        Task DeleteUser(User user);
        Task UpdateUser(User updatedUser);
        Task<bool> IsUserActive(int userId);
        Task<bool> IsEmailExist(string email, int currentId = 0);
        Task<bool> IsPhoneNumberExist(string phoneNumber, int currentId = 0);
    }

}
