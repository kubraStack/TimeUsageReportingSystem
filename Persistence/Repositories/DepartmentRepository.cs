using Application.Repositories.DepartmentRepo;
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
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Department>> GetActiveDepartmentAsync()
        {
            return await _dbContext.Departments
                .Where(d => !d.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Department?> GetDepartmentByNameAsync(string name)
        {
            return await _dbContext.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<IReadOnlyList<Employee>> GetEmpByDepartmentIdAsync(int departmentId)
        {
            return await _dbContext.Employees
                .Where(e => e.DepartmentId == departmentId)
                // Soft Delete mantığına uygun olarak sadece aktif çalışanları getirir
                .Where(e => !e.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
