using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
        //Departmanın adına göre departmanı getirir
        Task<Department?> GetDepartmentByNameAsync(string name);

        //Belirli bir departmandaki tüm çalışanları getirir
        Task<IReadOnlyList<Employee>> GetEmpByDepartmentIdAsync(int departmentId);

        //Aktif(IsDeleted = false) departmanları getirir.
        Task<IReadOnlyList<Department>> GetActiveDepartmentAsync();
    }
}
