﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Monitors
{
    public class ImageViewModel : AbstractMonitorViewModel
    {
        //public ImageViewModel(string channel) : base(channel)
        //{
        //}
        public ImageViewModel(Stylet.IEventAggregator events) : base(events) { }
    }
}