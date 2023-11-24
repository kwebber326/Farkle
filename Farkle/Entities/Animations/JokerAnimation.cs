using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farkle.Entities.Animations
{
    public class JokerAnimation : Animation
    {
        private readonly PictureBox[] _sequences;

        public JokerAnimation() : base(3000)
        {
            PictureBox p = new PictureBox();
            p.SizeMode = PictureBoxSizeMode.StretchImage;
            p.Image = Properties.Resources.joker;
            p.Size = new System.Drawing.Size(192, 192);
            _sequences = new PictureBox[] { p };
        }

        public override PictureBox[] Sequences => _sequences;
    }
}
