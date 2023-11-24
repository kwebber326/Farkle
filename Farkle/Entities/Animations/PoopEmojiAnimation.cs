using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farkle.Entities.Animations
{
    public class PoopEmojiAnimation : Animation
    {
        private readonly PictureBox[] _sequences;

        public PoopEmojiAnimation(Size containerSize) : base(100)
        {
            Size initialPoopSize = new Size(64, 64);
            int sizeIncrement = 2;
            int animationCount = 32;
            _sequences = new PictureBox[animationCount];
            for (int i = 0; i < animationCount; i++)
            {
                PictureBox p = new PictureBox();
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                int sizeOffset = i * sizeIncrement;
                p.Size = new Size(initialPoopSize.Width + sizeOffset, initialPoopSize.Height + sizeOffset);
                int x = (containerSize.Width - p.Size.Width) / 4;
                int y = (containerSize.Height - p.Height) / 4;
                p.Location = new Point(x, y);
                int rotationSequenceIndex = i % 4;
                p.Image = Properties.Resources.poop;
                switch (rotationSequenceIndex)
                {
                    case 1:
                        p.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 2:
                        p.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 3:
                        p.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }

                _sequences[i] = p;
            }
        }
        public override void Stop()
        {
            _currentAnimationIndex = 0;
            base.Stop();
        }
        public override PictureBox[] Sequences => _sequences;
    }
}
