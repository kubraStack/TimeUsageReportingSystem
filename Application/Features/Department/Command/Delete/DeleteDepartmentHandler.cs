using Application.Repositories.DepartmentRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Command.Delete
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, DeleteDepartmentResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DeleteDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DeleteDepartmentResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            //Silinecek departmanı veritabanından bul
            Domain.Entities.Department? departmentToDelete = await _departmentRepository.GetAsync(
                    predicate: d => d.Id == request.Id && d.IsDeleted == false,
                    cancellationToken: cancellationToken
            );

            //İş kuralı/kayıt bulunamadıysa
            if (departmentToDelete == null)
            {
                throw new Exception($"ID'si {request.Id} olan aktif departman bulunamadı.");
            }
            //Softdelete işlemi
            departmentToDelete.IsDeleted = true;
            await _departmentRepository.UpdateAsync(departmentToDelete);

            DeleteDepartmentResponse response = new DeleteDepartmentResponse
            {
                Id = departmentToDelete.Id,
                Message = $"ID'si {departmentToDelete.Id} olan departman başarıyla silindi."
            };
            return response;

        }
    }
}
