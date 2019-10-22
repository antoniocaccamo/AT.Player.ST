using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Service
{
    public interface IPreferenceService
    {
        Configuration.Preference Get();

        void Save(Configuration.Preference configuration);
    }
}