using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Configuration
{
    internal class Context : IContext
    {
        private Configuration _configuration;
        Configuration IContext.Configuration { get => _configuration; set => _configuration = value; }
    }
}