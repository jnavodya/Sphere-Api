using Sphere.Domain.Entities;

namespace Sphere.Domain.Repositories
{
    public interface ITimerRepository
    {
        Task<WorkLog> AddOrUpdateWorkLog(WorkLog workLog);
        List<WorkLog> GetWorkLogs(int userId);

    }
}
