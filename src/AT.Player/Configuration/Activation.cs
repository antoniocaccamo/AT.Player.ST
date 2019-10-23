namespace AT.Player.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Activation //: Stylet.PropertyChangedBase
    {
        public enum ActivationEnum
        {
            ALLDAY,
            TIMED
        }

        public enum WhenNotActiveEnum
        {
            BLACK,
            IMAGE,
            WATCH
        }

        public ActivationEnum Type { get; set; }

        private System.DateTime From { get; set; }

        private System.DateTime To { get; set; }

        public WhenNotActiveEnum WhenNotActive { get; set; }

        public string Image { get; set; }

        public override string ToString()
        {
            var ret = $"Type {Type} From {From} To {To} WhenNotActive {WhenNotActive} Image {Image}";
            return ret;
        }
    }
}