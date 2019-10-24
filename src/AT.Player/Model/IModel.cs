using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Model
{
    public interface IModel
    {
        #region Public Methods

        void PostConstruct();

        #endregion Public Methods
    }
}