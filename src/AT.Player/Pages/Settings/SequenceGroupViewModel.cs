﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Settings
{
    public class SequenceGroupViewModel : Stylet.Screen
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SequenceGroupViewModel() : base()
        {
            DisplayName = "Sequence Manager";
            Logger.Warn("###### " + DisplayName);
        }
    }
}