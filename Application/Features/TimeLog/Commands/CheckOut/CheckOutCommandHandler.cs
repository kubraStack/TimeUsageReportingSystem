using Application.Repositories.EmployeeRepo;
using Application.Repositories.TimeLogRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TimeLog.Commands.CheckOut
{
    public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, CheckOutResponse>
    {
        private readonly ITimeLogRepository _timeLogRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public CheckOutCommandHandler(ITimeLogRepository timeLogRepository, IEmployeeRepository employeeRepository)
        {
            _timeLogRepository = timeLogRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CheckOutResponse> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            // 1. İş Kuralı: Çalışanın  en son kaydını bul
            Domain.Entities.TimeLog lastLog = await _timeLogRepository.GetLastestLogByEmpIdAsync(request.EmployeeId);
            if (lastLog == null || lastLog.LogType != LogType.Entry)
            {
                //Çıkış yapmak için bir mesai giriş kaydının (Entry) bulunması gerekir.
                throw new BusinessException("Çıkış yapmak için aktif bir mesai giriş kaydı bulunmamaktadır.");
            }

            //Çalışan bilgisini çekelim
            Employee? employee = await _employeeRepository.GetAsync(
                predicate: e => e.Id == request.EmployeeId,
                cancellationToken: cancellationToken
            );
            if (employee == null)
            {
                throw new BusinessException("Mesai kaydı yapılan çalışan bulunamadı.");
            }

            //Yeni çıkış kaydı oluşturulsun
            var newExitLog = new Domain.Entities.TimeLog
            {
                EmployeeId = request.EmployeeId,
                LogType = LogType.Exit,
                LogTime = DateTime.UtcNow,
            };

            //Kaydet
            var createdExitLog = await _timeLogRepository.AddAsync(newExitLog);
            //Süreyi hesapla 
            TimeSpan duration = createdExitLog.LogTime - lastLog.LogTime;
            return new CheckOutResponse
            {
                Id = createdExitLog.Id,
                ChechInTime = lastLog.LogTime, //En son entry zamanı
                ChechOutTime = createdExitLog.LogTime,
                Duration = duration,
                EmployeeEmail = employee.Email
            };
        }
    }
}
