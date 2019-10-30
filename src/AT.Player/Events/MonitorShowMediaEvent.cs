using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Events
{
    [ToString]
    public class MonitorShowMediaEvent : AbstractShowEvent
    {
        #region Private Fields

        private readonly Model.Media _media;

        #endregion Private Fields

        #region Public Constructors

        public MonitorShowMediaEvent(Model.Media media)
        {
            _media = media;
        }

        #endregion Public Constructors

        #region Public Properties

        public Model.Media Media => _media;

        #endregion Public Properties
    }
}