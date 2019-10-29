using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Events
{
    [ToString]
    public class MonitorShowMediaProgressPercentageEvent : AbstractShowEvent
    {
        public int ProgressPercentage { get; set; }
    }
}