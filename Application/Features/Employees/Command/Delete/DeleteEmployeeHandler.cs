using Application.Repositories.EmployeeRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.Delete
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<DeleteEmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeToDelete = await _employeeRepository.GetByIdAsync(request.Id);

            if (employeeToDelete == null)
            {
                throw new Exception($"Silmek istenilen Çalışan Id {request.Id} bulunamadı !");
            }

            //SoftDelete alanlarını güncelle
            employeeToDelete.IsDeleted = true;
            employeeToDelete.DeletedDate = DateTime.UtcNow;

            await _employeeRepository.UpdateAsync(employeeToDelete);

            return new DeleteEmployeeResponse
            {
                Id = employeeToDelete.Id,
                DeletedTime = employeeToDelete.DeletedDate.Value,
                Message = $"Çalışan ID {request.Id} başarıyla pasifize edilmiştir."
            };
        }
    }
}
