namespace AT.Player.Model
{
    using System;

    [ToString]
    public class SequenceLooper
    {
        #region Private Fields

        private int _loop, _current, _next;
        private Sequence _sequence;

        #endregion Private Fields

        #region Public Constructors

        public SequenceLooper()
        {
            _loop = _current = 0;
            _next = -1;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Loop => _loop;
        public Media Next => getNext(DateTime.Now);

        public Sequence Palimpsest
        {
            get => _sequence;
            set
            {
                _sequence = value;
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
            lock (_sequence)
            {
                while (!found)
                {
                    _next = ++_next % _sequence.Medias.Count;
                    media = _sequence.Medias[_next];
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