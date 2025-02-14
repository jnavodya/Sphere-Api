using Sphere.Infrasturcture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using Sphere.Domain.Repositories;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using Sphere.Domain.Entities;

namespace Sphere.Infrasturcture.Repositories
{
    public class TimerRepository : ITimerRepository
    {
        // Need to manually create this file & insert []
        private readonly string _filePath = "../Sphere.Infrasturcture/Data/timers.json";

        public void SaveTimers(List<CheckInCheckOutTimer> timers)
        {
            var json = JsonConvert.SerializeObject(timers, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

    }
}
