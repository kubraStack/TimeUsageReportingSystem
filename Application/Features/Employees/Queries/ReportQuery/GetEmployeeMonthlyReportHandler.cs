using Application.Models.Reports;
using Application.Repositories.EmployeeRepo;
using Application.Repositories.TimeLogRepo;
using Core.Utilities.Encryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.ReportQuery
{
    public class GetEmployeeMonthlyReportHandler : IRequestHandler<GetEmployeeMonthlyReportQuery, List<ReportResultDto>>
    {
        private readonly IEmployeeRepository  _employeeRepository;
        private readonly ITimeLogRepository _timeLogRepository;
        private readonly EncryptionHelper _encryptionHelper;
        public GetEmployeeMonthlyReportHandler(IEmployeeRepository employeeRepository, ITimeLogRepository timeLogRepository, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _timeLogRepository = timeLogRepository;
            _encryptionHelper = encryptionHelper;
        }
        public async Task<List<ReportResultDto>> Handle(GetEmployeeMonthlyReportQuery request, CancellationToken cancellationToken)
        {
            //Tüm aktif çalışanları çekme
            var activeEmployees = await _employeeRepository.GetListAsync(
                predicate: e => e.IsDeleted == false
            );

            //Final raporlarının sonuçlarını tutacak liste
            var reportResults = new List<ReportResultDto>();

            //Mesai başlangıç saatini temsil eden sabit zaman aralığı
            TimeSpan officialStartTime = new TimeSpan(9, 0, 0); // 09:00 AM

            
            foreach (var employee in activeEmployees.Items)
            {
                
                //Çalışana ait filtrelenmiş logları Repository metodunu kullanarak çekme
                var employeeLogs = await _timeLogRepository.GetLogsByEmpAndDateRangeAsync(
                    employee.Id,
                    request.StartDate,
                    request.EndDate                    
                );

                double totalWorkHours = 0;
                int lateEntryCount = 0;

                // Hesaplamaların kronolojik sırada (zaman akışına uygun) yapılabilmesi için sıralanır.
                var sortedLogs = employeeLogs.OrderBy(l => l.LogTime).ToList();

                for (int i = 0; i < sortedLogs.Count; i++)
                {
                    var currentLog = sortedLogs[i];

                    TimeSpan logTimeComponent = currentLog.LogTime.TimeOfDay;
                    // Geç giriş kontrolü
                    if (currentLog.LogType == Domain.Entities.LogType.Entry)
                    {
                        lateEntryCount++;
                    }

                    //Çalışma süresi hesaplama
                    var exidLogIndex = sortedLogs.FindIndex(i + 1, l => l.LogType == Domain.Entities.LogType.Exit);
                    if (exidLogIndex > i) //Geçerli bir exit logu bulunduysa 
                    {
                        var exitLog = sortedLogs[exidLogIndex];
                        TimeSpan grossDuration = exitLog.LogTime -currentLog.LogTime; //Brüt süre
                        TimeSpan breakDuration = TimeSpan.Zero; //Mola süresi

                        // Giriş ve Çıkış arasındaki Mola (LunchStart/End) çiftlerini hesapla
                        for (int j = i + 1; j < exidLogIndex; j++)
                        {
                            var log = sortedLogs[j];
                            if(log.LogType == Domain.Entities.LogType.LunchStart)
                            {
                                // Başlangıçtan sonraki ilk Bitişi bul (Çıkıştan önce olmalı)
                                var lunchEndLog = sortedLogs
                                    .Skip(j + 1)
                                    .FirstOrDefault(l => l.LogType == Domain.Entities.LogType.LunchEnd && l.LogTime < exitLog.LogTime);

                                if (lunchEndLog != null)
                                {
                                    breakDuration += (lunchEndLog.LogTime - log.LogTime);
                                }
                            }

                        }

                        //Net çalışma süresi
                        totalWorkHours += (grossDuration - breakDuration).TotalHours;
                        i = exidLogIndex; //İndeksi Exit loguna güncelle

                    }
                }

                int totalRefectoryUsage = employeeLogs.Count(l => l.LogType == Domain.Entities.LogType.LunchStart);
                //Rapor sonucunu oluşturma ve listeye ekleme
                reportResults.Add(new ReportResultDto
                {
                    EmployeeId = employee.Id,
                    FirstName = _encryptionHelper.DecryptString(employee.EncryptedFirstName),
                    LastName = _encryptionHelper.DecryptString(employee.EncryptedLastName),
                    TotalWorkHours = Math.Round(totalWorkHours, 2),
                    TotalRefectoryUsage = totalRefectoryUsage,
                    LateEntryCount = lateEntryCount
                });
            }

            return reportResults;

        }
    }
}
