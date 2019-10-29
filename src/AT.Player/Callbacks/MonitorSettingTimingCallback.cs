using AT.Player.Configuration;
using AT.Player.Pages.Settings;
using Stylet;
using System;

namespace AT.Player.Callbacks
{
    public class MonitorSettingTimingCallback : ITimingCallback
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly MonitorSettingViewModel _vm;

        #endregion Private Fields

        public MonitorSettingTimingCallback(MonitorSettingViewModel viewModel)
        {
            _vm = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void timingCallBack(object obj)
        {
            if (_vm.Monitor == null)
            {
                return;
            }

            _logger.Debug(
                "Monitor.DisplayName [{0}] Monitor.MonitorStatus.PLAYING ? [{1}] Monitor.MonitorStatus [{2}] Setting.Activation [{3}] ",
                _vm.DisplayName, MonitorSettingViewModel.MonitorStatusEnum.PLAYING.Equals(_vm.MonitorStatus),
                _vm.MonitorStatus, _vm.Monitor.Activation
                );

            switch (_vm.Monitor.Activation.Type)
            {
                case Activation.ActivationEnum.TIMED:
                    break;

                case Activation.ActivationEnum.ALLDAY:
                    if (!MonitorSettingViewModel.MonitorStatusEnum.PLAYING.Equals(_vm.MonitorStatus))
                    {
                        Execute.OnUIThreadAsync(new Action(() => _vm.DoPlay()));
                    }
                    break;
            }

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