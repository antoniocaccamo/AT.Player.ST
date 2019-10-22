using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Events
{
    public abstract class AbstractShowEvent
    {
        public Uri Source { get; set; }
    }
}