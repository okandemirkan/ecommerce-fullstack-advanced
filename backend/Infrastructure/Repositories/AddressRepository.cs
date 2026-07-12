using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ECommerceDbContext _context;
        public AddressRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Address?>> GetAddressesByUserId(int id)
        {
            var address = await _context.Addresses.Include(c => c.User).
                Where(a => a.UserId == id).ToListAsync();
            return address;
        }
        public async Task<Address?> GetAddressById(int id)
        {
            var address = await _context.Addresses.
                SingleOrDefaultAsync(a => a.Id == id);
            return address;
        }
        public async Task AddAddress(Address newAddress)
        {
            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAddress(Address updatedAddress)
        {
            _context.Addresses.Update(updatedAddress);
            await _context.SaveChangesAsync();
        }
    }
}
