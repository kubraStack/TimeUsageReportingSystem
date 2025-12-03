using Application.Repositories.TimeLogRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.Delete
{
    public class DeleteTimeLogCommandHandler : IRequestHandler<DeleteTimeLogCommand, DeleteTimeLogResponse>
    {
        private readonly ITimeLogRepository _timeLogRepository;

        public DeleteTimeLogCommandHandler(ITimeLogRepository timeLogRepository)
        {
            _timeLogRepository = timeLogRepository;
        }

        public async Task<DeleteTimeLogResponse> Handle(DeleteTimeLogCommand request, CancellationToken cancellationToken)
        {
            //SoftDelete aktif olduğu için sadece IsDeleted= false olanları arayabiliriz.
            Domain.Entities.TimeLog? timeLog = await _timeLogRepository.GetAsync(
                predicate: t => t.Id == request.Id,
                cancellationToken: cancellationToken
            );

            if (timeLog == null)
            {
                throw new BusinessException($"ID {request.Id}  olan aktif zaman kaydı bulunamadı.");
            }

            //silme işlemi
            await _timeLogRepository.DeleteAsync(timeLog);



            //SoftDelete olduğu için silme tarihi ataması
            DateTime deletedAt = DateTime.UtcNow;

            return new DeleteTimeLogResponse
            {
                Id = timeLog.Id,
                DeletedDate = timeLog.DeletedDate.HasValue ? timeLog.DeletedDate.Value : deletedAt
            };
        }
    }
}
