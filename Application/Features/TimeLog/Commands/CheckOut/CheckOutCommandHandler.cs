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

            //En son Entry ve Exit kayıtlarını al 
            Domain.Entities.TimeLog? lastEntryLog = await _timeLogRepository.GetLastestLogByTypeAsync(request.EmployeeId, LogType.Entry);
            Domain.Entities.TimeLog? lastExitLog = await _timeLogRepository.GetLastestLogByTypeAsync(request.EmployeeId, LogType.Exit);

            //Kontrol A:
            //Hiç Entry kaydı yoksa çıkış yapılamaz
            if (lastEntryLog == null)
            {
                throw new BusinessException("Çıkış yapmak için aktif bir giriş kaydı bulunamadı !");
            }
            //Kontrol B:
            //Eğer Entry var ancak Exit kaydı daha yeniyse veya aynı anda ise zaten çıkış yapılmış demektir.
            if (lastExitLog != null && lastExitLog.LogTime >= lastEntryLog.LogTime)
            {
                throw new BusinessException("Çıkış yapmak için aktif bir mesai giriş kaydı bulunmaktadır.(En son kayıt zaten çıkış/izin kaydıdır.)");
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

            ////Kaydet
            var createdExitLog = await _timeLogRepository.AddAsync(newExitLog);

            ////Süreyi hesapla 
            TimeSpan duration = createdExitLog.LogTime - lastEntryLog.LogTime;

            return new CheckOutResponse
            {
                Id = createdExitLog.Id,
                ChechInTime = lastEntryLog.LogTime, //En son entry zamanı
                ChechOutTime = createdExitLog.LogTime,
                Duration = duration,
                EmployeeEmail = employee.Email
            };
        }
    }
}
