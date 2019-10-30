namespace AT.Player.Events
{
    using System;

    public abstract class AbstractShowEvent : EventArgs
    {
        public Uri Source { get; set; }
    }
}