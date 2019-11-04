using AT.Player.Model;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Settings
{
    public class SequenceSettingViewModel : Screen
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Sequence _sequence;

        #endregion Private Fields

        #region Public Constructors

        public SequenceSettingViewModel(Sequence sequence)
        {
            _sequence = sequence;
        }

        #endregion Public Constructors

        #region Public Properties

        public Media SelectedMedia { get; set; }
        public Sequence Sequence => _sequence;

        public bool CanDoAdd => true;

        public bool CanDoRemove => SelectedMedia != null;

        public bool CanDoEdit => SelectedMedia != null;

        public bool CanDoMoveUp => SelectedMedia != null;

        public bool CanDoMoveDown => SelectedMedia != null;

        #endregion Public Properties

        public void DoAdd()
        {
            _logger.Info("DoAdd()..");
        }

        public void DoRemove()
        {
            _logger.Info("DoRemove()..");
        }

        public void DoEdit()
        {
            _logger.Info("DoEdit()..");
        }

        public void DoMoveUp()
        {
            _logger.Info("DoMoveUp()..");
        }

        public void DoMoveDown()
        {
            _logger.Info("DoMoveDown()..");
        }
    }
}