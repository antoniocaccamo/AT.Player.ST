﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Configuration
{
    public interface IContext
    {
        Configuration Configuration { get; set; }
    }
}