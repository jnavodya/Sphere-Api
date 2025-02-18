using Sphere.Domain.Enums;

namespace Sphere.Api.Models.Request
{
    public class AttendenceRequest
    {
        public int UserId { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
