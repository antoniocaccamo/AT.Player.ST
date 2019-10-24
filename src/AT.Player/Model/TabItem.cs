namespace AT.Player.Domain
{
    using AT.Player.Domain.Interfaces;
    using Catel;
    using Catel.MVVM;
    using System;
    using System.Threading.Tasks;

    public class TabItem : ITabItem
    {
        #region Public Constructors

        public TabItem(IViewModel viewModel)
        {
            Argument.IsNotNull(() => viewModel);

            ViewModel = viewModel;
            CanClose = true;

            if (!viewModel.IsClosed)
            {
                viewModel.ClosedAsync += OnViewModelClosed;
            }
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<EventArgs> Closed;

        #endregion Public Events

        #region Public Properties

        public bool CanClose { get; set; }
        public object Tag { get; set; }
        public IViewModel ViewModel { get; private set; }

        #endregion Public Properties

        #region Private Methods

        private async Task OnViewModelClosed(object sender, ViewModelClosedEventArgs e)
        {
            var vm = ViewModel;
            if (vm != null)
            {
                vm.ClosedAsync -= OnViewModelClosed;
            }

            Closed.Invoke(sender, e);
        }

        #endregion Private Methods
    }
}