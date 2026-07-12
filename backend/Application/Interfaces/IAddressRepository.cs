using Domain.Entities;
namespace Application.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressById(int id);
        Task<IEnumerable<Address?>> GetAddressesByUserId(int id);
        Task AddAddress(Address newAddress);
        Task UpdateAddress(Address updatedAddress);
    }
}
