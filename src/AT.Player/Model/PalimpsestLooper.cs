namespace AT.Player.Model
{
    using System;

    [ToString]
    public class PalimpsestLooper
    {
        #region Private Fields

        private int _loop, _current, _next;
        private Palimpsest _palimpsest;

        #endregion Private Fields

        #region Public Constructors

        public PalimpsestLooper()
        {
            _loop = _current = 0;
            _next = -1;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Loop => _loop;
        public Media Next => getNext(System.DateTime.Now);

        public Palimpsest Palimpsest
        {
            get => _palimpsest;
            set
            {
                _palimpsest = value;
                _loop = _current = 0;
                _next = -1;
            }
        }

        #endregion Public Properties

        #region Private Methods

        private Media getNext(DateTime now)
        {
            bool found = false;
            Media media = null;
            lock (_palimpsest)
            {
                while (!found)
                {
                    _next = ++_next % _palimpsest.Medias.Count;
                    media = _palimpsest.Medias[_next];
                    if (media.IsPlayable(now))
                    {
                        found = true;
                    }
                    else
                    {
                        found = true;
                    }

                    _loop += _next == 0 ? 1 : 0;
                }
            }
            return media;
        }

        #endregion Private Methods

        #region Public Methods

        public bool isPlayable()
        {
            if (Palimpsest == null || Palimpsest.Medias == null || Palimpsest.Medias.Count == 0)
                return false;
            return true;
        }

        #endregion Public Methods
    }
}