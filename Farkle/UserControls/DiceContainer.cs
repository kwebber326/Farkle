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

namespace Farkle.UserControls
{
    public partial class DiceContainer : UserControl
    {
        private const int HORIZONTAL_MARGIN = 8;
        private const int VERTICAL_MARGIN = 8;
        private int _x = HORIZONTAL_MARGIN;
        private int _y = VERTICAL_MARGIN;

        public DiceContainer()
        {
            InitializeComponent();
        }

        private void DiceContainer_Load(object sender, EventArgs e)
        {

        }

        public void InsertDiceToRow(List<Dice> dice)
        {
            foreach (var d in dice)
            {
                pnlChosenDice.Controls.Add(d);
                d.Location = new Point(_x, _y);
                _x = d.Right + HORIZONTAL_MARGIN;
            }
            foreach (var d in dice)
            {
                pnlChosenDice.ScrollControlIntoView(d);
            }
            _y = pnlChosenDice.Controls.OfType<Dice>().ToList().Select(d => d.Bottom).Max() + VERTICAL_MARGIN;
            _x = HORIZONTAL_MARGIN;
        }

        public void ClearContainer()
        {
            _x = HORIZONTAL_MARGIN;
            _y = VERTICAL_MARGIN;
            pnlChosenDice.Controls.Clear();
        }
    }
}
