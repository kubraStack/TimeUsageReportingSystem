using Application.Repositories.EmployeeRepo;
using Application.Repositories.TimeLogRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.CheckIn
{
    public class CheckInCommandHandler : IRequestHandler<CheckInCommand, CheckInCommandResponse>
    {
        private readonly ITimeLogRepository _timeLogRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CheckInCommandHandler(ITimeLogRepository timeLogRepository, IEmployeeRepository employeeRepository)
        {
            _timeLogRepository = timeLogRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CheckInCommandResponse> Handle(CheckInCommand request, CancellationToken cancellationToken)
        {
            //Çalışanın en son zaman kaydını al
            Domain.Entities.TimeLog? lastLog = await _timeLogRepository.GetLastestLogByTypeAsync(request.EmployeeId, Domain.Entities.LogType.Entry);

            //Eğer en son Entry kaydı varsa, tekrar Entry yapamaz
            if (lastLog != null)
            {
                throw new BusinessException("Çalışanın zaten açık bir mesai giriş kaydı bulunmaktadır.");
            }

            //Çalışanın bilgilerini al 
            Employee? employee = await _employeeRepository.GetAsync(
                
                predicate:  e => e.Id == request.EmployeeId,
                cancellationToken: cancellationToken
            );

            if (employee == null)
            {
                //Eğer çalışan yoksa
                throw new BusinessException("Mesai kaydı yapılan çalışan bulunamadı.");
            }

            //Yeni bir zaman kaydı oluştur
            var newLog = new Domain.Entities.TimeLog
            {
                EmployeeId = request.EmployeeId,
                LogTime = DateTime.UtcNow,
                LogType = Domain.Entities.LogType.Entry //Giriş kaydı
            };
            //Kaydetme işlemi
            var createdLog = await _timeLogRepository.AddAsync(newLog);
            //Response objesi oluştur
            return new CheckInCommandResponse
            {
                Id = createdLog.Id,
                CheckInTime = createdLog.LogTime,
                EmployeeEmail = employee.Email
            };
        }
    }
}
