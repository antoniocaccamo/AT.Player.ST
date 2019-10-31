using AT.Player.Configuration;
using AT.Player.Pages.Settings;
using Stylet;
using System;
using System.Threading;
using System.Threading.Tasks;
using static AT.Player.Pages.Settings.MonitorSettingViewModel;

namespace AT.Player.Callbacks
{
    public class MonitorSettingTimingCallback : ITimingCallback
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly MonitorSettingViewModel _monitorSettingViewModel;

        #endregion Private Fields

        public MonitorSettingTimingCallback(MonitorSettingViewModel viewModel)
        {
            _monitorSettingViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public void timingCallBack(object obj)
        {
            try
            {
                _logger.Info($"{_monitorSettingViewModel.Channel} : EnterUpgradeableReadLock ..");
                _monitorSettingViewModel.ReaderWriterLock.EnterUpgradeableReadLock();

                if (_monitorSettingViewModel.Monitor == null)
                {
                    return;
                }

                _logger.Debug(
                    "Monitor.DisplayName [{0}] Monitor.MonitorStatus.PLAYING ? [{1}] Monitor.MonitorStatus [{2}] Setting.Activation [{3}] ",
                    _monitorSettingViewModel.DisplayName, MonitorSettingViewModel.MonitorStatusEnum.PLAYING.Equals(_monitorSettingViewModel.MonitorStatus),
                    _monitorSettingViewModel.MonitorStatus, _monitorSettingViewModel.Monitor.Activation
                    );

                switch (_monitorSettingViewModel.Monitor.Activation.Type)
                {
                    case Activation.ActivationEnum.TIMED:
                        break;

                    case Activation.ActivationEnum.ALLDAY:
                        if (!MonitorSettingViewModel.MonitorStatusEnum.PLAYING.Equals(_monitorSettingViewModel.MonitorStatus))
                        {
                            _logger.Info($"{_monitorSettingViewModel.Channel} : EnterWriteLock ..");
                            _monitorSettingViewModel.ReaderWriterLock.EnterWriteLock();
                            _logger.Info($"{_monitorSettingViewModel.Channel} : EnterWriteLock : acquired..");

                            Task.Factory.StartNew(() =>
                                _monitorSettingViewModel.DoPlay()
                            );
                            _logger.Info($"{_monitorSettingViewModel.Channel} : ExitWriteLock  ..");
                            _monitorSettingViewModel.ReaderWriterLock.ExitWriteLock();
                        }
                        break;
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
            catch (Exception e)
            {
                _logger.Error($"error occurred: {e.Message}");
            }
            finally
            {
                if (_monitorSettingViewModel.ReaderWriterLock.IsWriteLockHeld)
                {
                    _logger.Info($"{_monitorSettingViewModel.Channel} : ExitWriteLock (2)..");
                    _monitorSettingViewModel.ReaderWriterLock.ExitWriteLock();
                }

                //if (_monitorSettingViewModel.ReaderWriterLock.IsReadLockHeld)
                //{
                _logger.Info($"{_monitorSettingViewModel.Channel} : ExitReadLock ..");
                _monitorSettingViewModel.ReaderWriterLock.ExitUpgradeableReadLock();
                //}
            }
        }
    }
}