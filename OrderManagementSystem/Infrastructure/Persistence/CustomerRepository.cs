
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Customers.Entities;
using OrderManagementSystem.Domain.Customers.Repositories;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderManagementSystem.Infrastructure.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderDbContext _context;

        public CustomerRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await GetByIdAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }

}



