using Application.Repositories.TimeLogRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.Update
{
    public class UpdateTimeLogCommandHandler : IRequestHandler<UpdateTimeLogCommand, UpdateTimeLogResponse>
    {
        private readonly ITimeLogRepository _timeLogRepository;

        public UpdateTimeLogCommandHandler(ITimeLogRepository timeLogRepository)
        {
            _timeLogRepository = timeLogRepository;
        }

        public async Task<UpdateTimeLogResponse> Handle(UpdateTimeLogCommand request, CancellationToken cancellationToken)
        {
            //Güncellenecek kaydı bul
            Domain.Entities.TimeLog? timeLog = await _timeLogRepository.GetAsync(
                
                predicate: t =>t.Id == request.Id && t.EmployeeId == request.EmployeeId,
                cancellationToken: cancellationToken
            );

            if(timeLog == null)
            {
                throw new BusinessException($"ID {request.Id} olan zaman kaydı bulunamadı !");
            }

            //Güncelleme
            timeLog.LogTime = request.LogTime;
            timeLog.LogType = request.LogType;
            timeLog.UpdatedDate = DateTime.UtcNow;

            //Kaydet
           Domain.Entities.TimeLog updatedLog = await _timeLogRepository.UpdateReturningEntityAsync(timeLog);


            return new UpdateTimeLogResponse
            {
                Id = updatedLog.Id,
                LogTime = updatedLog.LogTime,
                LogType = updatedLog.LogType,
                UpdatedDate = updatedLog.UpdatedDate!.Value
            };
        }
    }
}
