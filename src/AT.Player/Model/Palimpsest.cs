/*
 * User: antonio.caccamo
 * Date: 29/01/2019
 * Time: 17:31
 */

namespace AT.Player.Model
{
    using Stylet;
    using System.Collections.Generic;
    using YamlDotNet.Serialization;

    [ToString]
    public class Palimpsest : IModel
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Public Properties

        public string LocalFile { get; set; }

        public BindableCollection<Media> Medias { get; set; }

        public string Name { get; set; }

        public string RemoteFile { get; set; }

        //[YamlIgnore]
        //public PalimpsestStatusEnum Status { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void PostConstruct()
        {
            _logger.Info($"postConstruct {LocalFile} ...");
            IEnumerator<Media> iem = Medias.GetEnumerator();
            while (iem.MoveNext())
            {
                iem.Current.PostConstruct();
            }
            //Status = PalimpsestStatusEnum.NOT_ACTIVE;
        }

        #endregion Public Methods

        //public enum PalimpsestStatusEnum
        //{
        //    NOT_ACTIVE, STOPPED, PLAYING, PAUSED
        //};
    }
}