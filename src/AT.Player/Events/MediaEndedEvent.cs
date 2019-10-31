using AT.Player.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Events
{
    public delegate void OnMediaEnded(object sender, MediaEndedEvent evt);

    public class MediaEndedEvent : AbstractShowEvent
    {
    }
}