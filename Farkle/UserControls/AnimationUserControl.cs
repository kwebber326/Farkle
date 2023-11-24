using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farkle.Entities.Animations;
using Farkle.Entities;

namespace Farkle.UserControls
{
    public partial class AnimationUserControl : UserControl
    {
        private readonly RussianRouletteAnimation _russianRouletteAnimation;
        private readonly PoopEmojiAnimation _poopEmojiAnimation;
        private readonly JokerAnimation _jokerAnimation;

        private readonly HotDiceAnimation _hotDiceAnimation;

        private readonly Animation[] _farkleAnimations;
        private readonly Animation[] _hotDiceAnimations;
        private readonly Random _random = new Random();

        public AnimationUserControl()
        {
            InitializeComponent();
            _russianRouletteAnimation = new RussianRouletteAnimation();
            _russianRouletteAnimation.AnimationStarted += _AnimationStarted;
            _russianRouletteAnimation.AnimationComplete += _AnimationComplete;

            _poopEmojiAnimation = new PoopEmojiAnimation(pnlAnimation.Size);
            _poopEmojiAnimation.AnimationStarted += _AnimationStarted;
            _poopEmojiAnimation.AnimationComplete += _AnimationComplete;
            _poopEmojiAnimation.AnimationSequenceChanged += _AnimationSequenceChanged;

            _jokerAnimation = new JokerAnimation();
            _jokerAnimation.AnimationStarted += _AnimationStarted;
            _jokerAnimation.AnimationComplete += _AnimationComplete;

            _farkleAnimations = new Animation[]
            {
                _russianRouletteAnimation,
                _poopEmojiAnimation,
                _jokerAnimation
            };

            _hotDiceAnimation = new HotDiceAnimation();
            _hotDiceAnimation.AnimationStarted += _AnimationStarted;
            _hotDiceAnimation.AnimationComplete += _AnimationComplete;

            _hotDiceAnimations = new Animation[]
            {
                _hotDiceAnimation
            };
        }

        private void _AnimationSequenceChanged(object sender, EventArgs e)
        {
            var animation = sender as Animation;
            var pictureBoxes = pnlAnimation.Controls.OfType<PictureBox>();
            if (animation != null && animation.Current != null && pictureBoxes.Any())
            {
                var pb = pictureBoxes.First();
                var currentPb = animation.Current;
                pb.Image =  currentPb.Image;
                pb.Size = new Size(currentPb.Width, currentPb.Height);
            }
        }

        private void _AnimationComplete(object sender, EventArgs e)
        {
            pnlAnimation.Controls.Clear();
            lblPlayerAction.Text = string.Empty;
        }

        private void _AnimationStarted(object sender, EventArgs e)
        {
            var animation = sender as Animation;
            if (animation != null)
            {
                pnlAnimation.Controls.Add(animation.Current);
            }
        }

        private void AnimationUserControl_Load(object sender, EventArgs e)
        {
            lblPlayerAction.Text = string.Empty;
        }

        public void RunFarkleAnimation(Player player)
        {
            StopAllAnimations();
            lblPlayerAction.Text = $"{player?.Name} Farkled!!!";
            int index = _random.Next(0, _farkleAnimations.Length);
            _farkleAnimations[index].Start();
        }

        public void RunHotDiceAnimation(Player player)
        {
            StopAllAnimations();
            lblPlayerAction.Text = $"{player?.Name} got hot dice!!!";
            int index = _random.Next(0, _hotDiceAnimations.Length);
            _hotDiceAnimations[index].Start();
        }

        private void StopAllAnimations()
        {
            foreach (var animation in _farkleAnimations)
            {
                animation.Stop();
            }
            _hotDiceAnimation.Stop();
        }
    }
}
