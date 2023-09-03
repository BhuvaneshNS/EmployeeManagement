using EmployeeManagement.Models;

namespace EmployeeManagement.Data
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
