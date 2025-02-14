using Sphere.Application.Interface;
using Sphere.Domain.DTO;
using Sphere.Domain.Entities;
using Sphere.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sphere.Application.Services
{
    public class TimerService : ITimerService
    {
        private readonly ITimerRepository _timerRepository;

        public TimerService(ITimerRepository timerRepository)
        {
            _timerRepository = timerRepository;
        }

        public TimerResponseDto CheckInUser(int userId)
        {
            Console.WriteLine("\n\n checkin service");
            var timers = _timerRepository.GetAllTimers();

            int newId = timers.Count > 0 ? timers.Max(t => t.Id) + 1 : 1;

            var newTimer = new CheckInCheckOutTimer
            {
                Id = newId,
                UserId = userId,
                CheckInTime = DateTime.UtcNow,
                CheckOutTime = null
            };

            timers.Add(newTimer);
            _timerRepository.SaveTimers(timers);

            return new TimerResponseDto
            {
                Id = newTimer.Id,
                UserId = newTimer.UserId,
                CheckInTime = newTimer.CheckInTime,
                CheckOutTime = newTimer.CheckOutTime
            };
        }

    }
}
