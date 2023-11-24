using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farkle.Entities.Animations
{
    public abstract class Animation
    {
        public event EventHandler AnimationStarted;
        public event EventHandler AnimationSequenceChanged;
        public event EventHandler AnimationComplete;
        protected Timer _animationTimer = new Timer();
        protected int _currentAnimationIndex;
        protected readonly int _delay;

        public Animation(int delayPerSequenceMilliseconds)
        {
            _delay = delayPerSequenceMilliseconds;
            _animationTimer.Interval = _delay;
            _animationTimer.Tick += _animationTimer_Tick;
        }

        private void _animationTimer_Tick(object sender, EventArgs e)
        {
            if (_currentAnimationIndex < this.Sequences.Length - 1)
            {
                this.MoveNext();
            }
            else
            {
                this.Stop();
            }
        }

        public abstract PictureBox[] Sequences { get; }

        public PictureBox Current { get; protected set; }

        protected virtual void OnAnimationStarted()
        {
            this.AnimationStarted?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnAnimationComplete()
        {
            this.AnimationComplete?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnAnimationSequenceChanged()
        {
            this.AnimationSequenceChanged?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Start()
        {
            _currentAnimationIndex = 0;
            _animationTimer.Start();
            this.Current = this.Sequences?.FirstOrDefault();
            OnAnimationStarted();
        }

        public virtual void Stop()
        {
            _currentAnimationIndex = 0;
            _animationTimer.Stop();
            this.Current = null;
            OnAnimationComplete();
        }

        public virtual void MoveNext()
        {
            if (_currentAnimationIndex < this.Sequences.Length - 1)
            {
                this.Current = this.Sequences[++_currentAnimationIndex];
                OnAnimationSequenceChanged();
            }
        }
    }
}
