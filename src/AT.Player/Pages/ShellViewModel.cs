using AT.Player.Events;
using AT.Player.Pages.Settings;
using Stylet;

namespace AT.Player.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IHandle<IShowEvent>
    {
        private WeatherViewModel _weather;
        private MonitorGroupViewModel _monitorGroup;

        public ShellViewModel(IEventAggregator events, MonitorGroupViewModel MonitorGroupViewModel, WeatherViewModel weatherViewModel)
        {
            this.DisplayName = "AT Player";
            this._monitorGroup = MonitorGroupViewModel;
            this._weather = weatherViewModel;

            events.Subscribe(this);

            Items.Add(_monitorGroup);
            Items.Add(_weather);

            this.ActivateItem(_weather);
        }

        public void Handle(IShowEvent message)
        {
            if (message.GetType() == typeof(ShowWeatherSettingEvent))
                this.ActivateItem(_weather);

            this.ActivateItem(_monitorGroup);
        }
    }
}