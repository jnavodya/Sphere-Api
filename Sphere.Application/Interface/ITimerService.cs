using Sphere.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphere.Application.Interface
{
    public interface ITimerService
    {
        TimerResponseDto CheckInUser(int userId);
        
    }
}
