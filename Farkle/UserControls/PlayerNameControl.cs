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
    public partial class PlayerNameControl : UserControl
    {
        public PlayerNameControl()
        {
            InitializeComponent();
        }

        private void PlayerNameControl_Load(object sender, EventArgs e)
        {

        }

        public bool IsCpu
        {
            get
            {
                return chkCPU.Checked;
            }
        }

        public int Number
        {
            get; set;
        }

        public string TitleText
        {
            get
            {
                return lblName.Text;
            }
            set
            {
                lblName.Text = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return txtName.Text;
            }
        }
    }
}
