using Sphere.Domain.Entities;
using Sphere.Domain.Repositories;

namespace Sphere.Infrasturcture.Repositories
{
    public class TimerRepository : ITimerRepository
    {
        private readonly Dictionary<int, List<WorkLog>> _workLogs = new();

        public async Task<WorkLog> AddOrUpdateWorkLog(WorkLog workLog)
        {
            if (!_workLogs.ContainsKey(workLog.UserId))
            {
                _workLogs[workLog.UserId] = new List<WorkLog>();
            }
            _workLogs[workLog.UserId].Add(workLog);

            return await Task.FromResult(workLog);
        }

        public List<WorkLog> GetWorkLogs(int userId)
        {
            return _workLogs.ContainsKey(userId) ? _workLogs[userId] : new List<WorkLog>();
        }


    }
}
