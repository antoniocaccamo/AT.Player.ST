using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Settings
{
    public class SequenceGroupSettingViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SequenceGroupSettingViewModel()
        {
            DisplayName = "Sequence Manager";
            Logger.Warn("###### " + DisplayName);
        }

        protected override void OnInitialActivate()
        {
            foreach (string key in Helpers.SequenceHelper.SequencesLoaded.Keys)
            {
                var sequence = Helpers.SequenceHelper.SequencesLoaded[key];
                SequenceSettingViewModel ssvm = new SequenceSettingViewModel(sequence);
                ssvm.DisplayName = key;
                this.Items.Add(ssvm);
                this.ActiveItem = this.ActiveItem ?? ssvm;
            }
        }
    }
}