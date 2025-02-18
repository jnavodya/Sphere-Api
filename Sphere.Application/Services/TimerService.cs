using Sphere.Application.Interfaces;
using Sphere.Domain.Entities;
using Sphere.Domain.Repositories;

namespace Sphere.Application.Services
{
    public class TimerService(ITimerRepository timerRepository) : ITimerService
    {
        private readonly ITimerRepository _timerRepository = timerRepository;

        public async Task<WorkLog?> CheckIn(int userId)
        {
            var workLogs = _timerRepository.GetWorkLogs(userId);
            var lastLog = workLogs.LastOrDefault();

            if (lastLog != null && lastLog.CheckOutTime == null)
            {
                //return "User is already checked in.";
                // TODO fix errro handling
                return null;
            }

            var workLog = new WorkLog
            {
                UserId = userId,
                CheckInTime = DateTime.Now,
                CheckOutTime = null
            };

            await _timerRepository.AddOrUpdateWorkLog(workLog);
            return workLog;

        }

        public async Task<WorkLog?> CheckOut(int userId)
        {
            var workLogs = _timerRepository.GetWorkLogs(userId);
            var lastLog = workLogs.LastOrDefault();

            if (lastLog == null || lastLog.CheckOutTime != null)
            {
                //return "User has not checked in or already checked out.";
                // TODO fix errro handling
                return null;
            }

            lastLog.CheckOutTime = DateTime.Now;
            lastLog.TotalWorkedHours = (lastLog.CheckOutTime.Value - lastLog.CheckInTime).TotalHours;
            await _timerRepository.AddOrUpdateWorkLog(lastLog);

            return lastLog;
        }


        public List<WorkLog> GetWorkLogsForDate(int userId, DateTime date)
        {
            var workLogs = _timerRepository.GetWorkLogs(userId);

            return workLogs
                .Where(log => log.CheckInTime.Date == date.Date)
                .ToList();
        }

    }
}
