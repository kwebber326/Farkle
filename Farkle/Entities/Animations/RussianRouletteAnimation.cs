using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farkle.Entities.Animations
{
    public class RussianRouletteAnimation : Animation
    {
        private PictureBox[] _sequences;

        public RussianRouletteAnimation() : base(18000)
        {
            PictureBox p = new PictureBox();
            p.SizeMode = PictureBoxSizeMode.AutoSize;
            p.Image = Properties.Resources.russian_roulette;
            _sequences = new PictureBox[] { p };
        }

        public override PictureBox[] Sequences => _sequences;
    }
}
