using Farkle.Entities.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farkle.Entities
{
    public class Dice : PictureBox
    {
        private readonly Random _random;
        private readonly Timer _timer = new Timer();
        private const int NUMBER_OF_TICKS = 12;
        private int _currentTickNum;
        private int _faceValue;
        public event EventHandler<DiceRollEventArgs> DiceRolled;
        public Dice()
        {
            int seed = CommonRandom.Random.Next(1, int.MaxValue);
            _random = new Random(seed);

            _timer.Interval = 100;
            _timer.Tick += _timer_Tick;
            this.SizeMode = PictureBoxSizeMode.AutoSize;
            this.FaceValue = 1;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            this.FaceValue = _random.Next(1, 7);
            if (++_currentTickNum >= NUMBER_OF_TICKS)
            {
                _currentTickNum = 0;
                _timer.Stop();
                OnDiceRolled();
            }
        }

        public int FaceValue
        {
            get
            {
                return _faceValue;
            }
            set
            {
                _faceValue = value;
                SetImageBasedOnFaceValue();
            }
        }
        public void Roll()
        {
            _timer.Start();
        }

        private void OnDiceRolled()
        {
            DiceRollEventArgs e = new DiceRollEventArgs()
            {
                FaceValue = this.FaceValue
            };
            this.DiceRolled?.Invoke(this, e);
        }

        private void SetImageBasedOnFaceValue()
        {
            this.Image = DiceSpriteSheet.DiceSprites[this.FaceValue - 1];
        }
    }
}
