using AT.Player.Events;
using AT.Player.Pages.Settings;
using Stylet;

namespace AT.Player.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IHandle<IShowEvent>
    {
        private WeatherViewModel _weather;
        private SequenceGroupViewModel _sequenceGroup;
        private MonitorGroupViewModel _monitorGroup;

        public ShellViewModel(IEventAggregator events,
            MonitorGroupViewModel monitorGroupViewModel, SequenceGroupViewModel sequenceGroupViewModel, WeatherViewModel weatherViewModel)
        {
            this.DisplayName = "AT Player";
            this._monitorGroup = monitorGroupViewModel;
            this._sequenceGroup = sequenceGroupViewModel;
            this._weather = weatherViewModel;

            events.Subscribe(this);

            Items.Add(_monitorGroup);
            Items.Add(_sequenceGroup);
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