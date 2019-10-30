using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Monitors
{
    public class BrowserViewModel : AbstractMonitorViewModel
    {
        //public BrowserViewModel(string channel) : base(channel)
        //{
        //}

        public BrowserViewModel(Stylet.IEventAggregator events) : base(events)
        {
        }
    }
}