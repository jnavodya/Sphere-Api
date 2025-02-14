using Sphere.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphere.Domain.Repositories
{
    public interface ITimerRepository
    {       
        void SaveTimers(List<CheckInCheckOutTimer> timers);
    }
}
