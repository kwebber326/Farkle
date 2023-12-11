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
using System.Reflection;
using Farkle.Entities.Attributes;
using Farkle.File_IO;

namespace Farkle.UserControls
{
    public partial class PlayerStatWindow : UserControl
    {

        public PlayerStatWindow()
        {
            InitializeComponent();
        }

        private void PlayerStatWindow_Load(object sender, EventArgs e)
        {
            
        }

        public void ClearWindow()
        {
            lblPlayer.Text = $"Player: ";
            pnlStats.Controls.Clear();
        }

        public void SetWindow(PlayerStats stats)
        {
            lblPlayer.Text = $"Player: {stats.PlayerName}";
            pnlStats.Controls.Clear();
            PropertyInfo[] propertyInfos = stats.GetType().GetProperties();
            propertyInfos = propertyInfos
                .OrderBy(p => {
                    int order = 0;
                    var attr = (p.GetCustomAttribute(typeof(DisplayOrder)));
                    if (attr == null)
                        return order;

                    var orderAttr = attr as DisplayOrder;
                    if (orderAttr == null)
                        return order;

                    order = orderAttr.Order;
                    return order;
                    }).ToArray();
            int currentY = 0;
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var pi = propertyInfos[i];
                string propName = pi.Name;
                string propVal = Convert.ToString(pi.GetValue(stats));
                string text = $"{propName}: {propVal}";

                PlayerStatRow row = new PlayerStatRow(text, i + 1);
                int yPos = i * row.Height; 
                row.Location = new Point(pnlStats.Location.X, yPos);
                pnlStats.Controls.Add(row);
                row.BringToFront();
            }
        }
    }
}
