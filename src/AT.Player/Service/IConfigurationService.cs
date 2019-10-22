using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Service
{
    public interface IConfigurationService
    {
        Configuration.Configuration GetConfiguration();

        void SaveConfiguration(Configuration.Configuration configuration);
    }
}