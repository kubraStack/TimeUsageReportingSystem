using Application.Repositories.EmployeeRepo;
using Core.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Employee>> GetActiveEmpAsync()
        {

            return await _dbContext.Employees
                .Where(e => !e.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeesByDepartment(string departmentName)
        {
            return await _dbContext.Employees
                .Include(e => e.Department)
                .Where(e => e.Department.Name == departmentName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeesByRoleAsync(UserType role)
        {
            return await _dbContext.Employees
                .Where(e => e.Role == role)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Employee>> GetTerminatedEmpAsync()
        {
            return await _dbContext.Employees
                .Where(e => !e.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
