using AT.Player.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Service
{
    public interface IPalimpsestService
    {
        //Model.Palimpsest Get(System.IO.File file);

        Palimpsest Get(string file);

        Task<Palimpsest> GetAsync(string file);

        bool Save(Model.Palimpsest palimpsest);

        Task<bool> SaveAsync(Model.Palimpsest palimpsest);
    }
}