using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farkle.UserControls
{
    public partial class PlayerStatRow : UserControl
    {
        private readonly string _text;
        private readonly int _order;

        public PlayerStatRow()
        {
            InitializeComponent();
        }

        public PlayerStatRow(string text, int order)
        {
            InitializeComponent();
            _text = text;
            _order = order;
        }

        private void PlayerStatRow_Load(object sender, EventArgs e)
        {
            this.BackColor = _order % 2 == 0 ? Color.White : Color.LightBlue;
            lblStat.ForeColor = _order % 2 == 0 ? Color.LightBlue : Color.White;
            lblStat.Text = _text;
        }
    }
}
