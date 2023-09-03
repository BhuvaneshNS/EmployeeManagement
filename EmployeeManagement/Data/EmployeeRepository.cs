using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public EmployeeRepository(EmployeeManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);

        }

        public async Task AddAsync(Employee employee)
        {
            
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }
        
    }
}
