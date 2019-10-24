namespace AT.Player.Domain
{
    using AT.Player.Domain.Interfaces;

    public class TabItemEventArgs
    {
        public ITabItem Selected_TabItem { get; set; }

        public TabItemEventArgs(ITabItem selected_tabitem)
        {
            Selected_TabItem = selected_tabitem;
        }
    }
}