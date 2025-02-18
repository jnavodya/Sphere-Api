namespace Sphere.Domain.Entities
{
    public class WorkLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public double? TotalWorkedHours { get; set; }
    }
}
