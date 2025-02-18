using Sphere.Domain.Entities;

namespace Sphere.Application.Interfaces
{
    public interface ITimerService
    {
        Task<WorkLog?> CheckIn(int userId);
        Task<WorkLog?> CheckOut(int userId);
        List<WorkLog> GetWorkLogsForDate(int userId, DateTime date);

    }
}
