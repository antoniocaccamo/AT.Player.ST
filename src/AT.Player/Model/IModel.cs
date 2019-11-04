using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Model
{
    public abstract class IModel : PropertyChangedBase
    {
        #region Public Methods

        public abstract void PostConstruct();

        #endregion Public Methods
    }
}