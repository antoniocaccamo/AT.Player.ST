using AT.Player.Pages.Settings;
using System;

namespace AT.Player.Callbacks
{
    public class MonitorSettingTimingCallback : ITimingCallback
    {
        #region Private Fields

        private readonly MonitorSettingViewModel _vm;

        #endregion Private Fields

        public MonitorSettingTimingCallback(MonitorSettingViewModel viewModel)
        {
            _vm = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void timingCallBack(object obj)
        {
            //Log.Debug(
            //    $"Index [{_settingViewModel.Index}] SettingStatus.RUNNING [{_settingViewModel.Setting.Status.Equals(SettingStatusEnum.RUNNING)}] " +
            //    $"SettingStatus [{_settingViewModel.Setting.Status}] Setting.Activation [{_settingViewModel.Setting.Activation}] ");

            if (Configuration.Activation.ActivationEnum.ALLDAY.Equals(_vm.Monitor.Activation.Type))
            {
                return;
            }
            //
            //if (Configuration.Activation.ActivationEnum.TIMED.Equals(_settingViewModel.Monitor.Activation.Type))
            //{
            //    if (!_settingViewModel.Setting.Status.Equals(SettingStatusEnum.RUNNING) ||
            //         !(
            //            _settingViewModel.Palimpsest.Status.Equals(PalimpsestStatusEnum.PLAYING) ||
            //            _settingViewModel.Palimpsest.Status.Equals(PalimpsestStatusEnum.PAUSED)
            //          )
            //       )
            //    {
            //        _settingViewModel.PlayCommand.Execute();
            //    }
            //}

            //if (!_settingViewModel.Setting.Activation.AllDay)
            //{
            //    DateTime now = DateTime.Now;

            // if (_settingViewModel.Setting.Status.Equals(SettingStatusEnum.RUNNING)) { switch
            // (_settingViewModel.Palimpsest.Status) { case PalimpsestStatusEnum.PLAYING: case
            // PalimpsestStatusEnum.PAUSED: // if (_settingViewModel.Setting.Activation.Timed.) break;

            //            case PalimpsestStatusEnum.NOT_ACTIVE:
            //                break;
            //        }
            //    }
            //}
        }
    }
}