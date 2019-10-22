using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Configuration
{
    internal class Context : IContext
    {
        private Preference _configuration;
        Preference IContext.Configuration { get => _configuration; set => _configuration = value; }

        public void Dispose()
        {
        }
    }
}