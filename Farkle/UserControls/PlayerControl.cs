using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farkle.Entities;
using Farkle.Entities.Utilities;

namespace Farkle.UserControls
{
    public partial class PlayerControl : UserControl
    {
        private int _score;
        private int _position;
        private bool _isMyTurn;
        private readonly Player _player;

        public PlayerControl(Player player)
        {
            InitializeComponent();
            _player = player;
            gbPlayer.Text = _player.Name;
            this.IsMyTurn = _player.IsMyTurn;
            if (_player is CPUPlayer)
            {
                gbPlayer.Text += " [CPU]";
            }
        }

        private void PlayerControl_Load(object sender, EventArgs e)
        {
            SetDisplayFromPlayerStats();
        }

        public Player Player
        {
            get
            {
                return _player;
            }
        }

        public bool IsBrokenIn
        {
            get
            {
                return _player.IsBrokenIn;
            }
            set
            {
                _player.IsBrokenIn = value;
            }
        }

        public bool IsMyTurn
        {
            get
            {
                return _isMyTurn;
            }
            set
            {
                _isMyTurn = value;
                this.BackColor = _isMyTurn ? Color.Beige : Color.Transparent;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                _player.Score = value;
                lblScore.Text = $"Score: {_score}";
            }
        }

        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                _player.Position = value;
                lblPosition.Text = FarkleUtilities.GetPositionText(_position);
            }
        }

        private void SetDisplayFromPlayerStats()
        {
            this.Score = _player.Score;
            this.Position = _player.Position;
        }
    }
}
