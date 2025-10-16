using Core.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
        //Yetkilendirme, raporlama
        Task<IReadOnlyList<Employee>> GetEmployeesByDepartment(string departmentName);
        //Kimlik doğrulama,şifre sıfırlama
        Task<Employee?> GetByEmailAsync(string email);
        //Yetki kontrolü,Admin paneli 
        Task<IReadOnlyList<Employee>> GetEmployeesByRoleAsync(UserType role);


        //Soft Delete mekanizması IsDeleted= true olan çalışanları getirme
        Task<IReadOnlyList<Employee>> GetTerminatedEmpAsync();

        //Sadece aktif çalışanları getirir
        Task<IReadOnlyList<Employee>> GetActiveEmpAsync();
    }
}
