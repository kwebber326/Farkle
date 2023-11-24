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
    public partial class GameLogUserControl : UserControl
    {
        private const int VERTICAL_OFFSET = 2;

        public GameLogUserControl()
        {
            InitializeComponent();
        }

        private void GameLogUserControl_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            MyListBoxItem item = lstLog.Items[e.Index] as MyListBoxItem; // Get the current item and cast it to MyListBoxItem
            if (item != null)
            {
                e.Graphics.DrawString( // Draw the appropriate text in the ListBox
                    item.Message, // The message linked to the item
                    lstLog.Font, // Take the font from the listbox
                    new SolidBrush(item.ItemColor), // Set the color 
                    0, // X pixel coordinate
                    e.Bounds.Y // Y pixel coordinate.  use the bounds of the list view to correctly identify the y position
                );
            }
        }

        private void myListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            Font myFont;
            Brush myBrush;
            int i = e.Index;

            if (e.Index == 3)
            {
                myFont = e.Font;
                myBrush = Brushes.Yellow;
            }
            else
            {
                myFont = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold);
                myBrush = Brushes.Red;
            }

            e.Graphics.DrawString(lstLog.Items[i].ToString(), myFont, myBrush, e.Bounds, StringFormat.GenericDefault);
        }

        public bool ExportOnGameOver
        {
            get
            {
                return chkExport.Checked;
            }
        }

        public void LogEntry(GameLogEntry entry)
        {
            ListViewItem li = new ListViewItem();
            var eventType = entry.EventType;
            var message = entry.Message;
            if (eventType == CommonConstants.EVENT_BANK)
            {
                li.ForeColor = Color.Green;
            }
            else if (eventType == CommonConstants.EVENT_FARKLE)
            {
                li.ForeColor = Color.Red;
            }
            else if (eventType == CommonConstants.EVENT_HOT_DICE)
            {
                li.ForeColor = Color.Orange;
            }
            else if (eventType == CommonConstants.EVENT_GAME_OVER)
            {
                li.ForeColor = Color.Gold;
            }
            else
            {
                li.ForeColor = Color.White;
            }
            string msg = $"[{entry.TimeStamp.ToString()}] {message}";
            MyListBoxItem item = new MyListBoxItem(li.ForeColor, msg);
            lstLog.Items.Add(item);
            int visibleItems = lstLog.ClientSize.Height / lstLog.ItemHeight;
            lstLog.TopIndex = Math.Max(lstLog.Items.Count - visibleItems + 1, 0);
        }

        public List<string> GetAllMessages()
        {
            List<string> messages = new List<string>();
            var items = lstLog.Items.OfType<MyListBoxItem>();
            if (items.Any())
            {
                foreach (var item in items)
                {
                    messages.Add(item.Message);
                }
            }

            return messages;
        }
    }

    public class MyListBoxItem
    {
        public MyListBoxItem(Color c, string m)
        {
            ItemColor = c;
            Message = m;
        }
        public Color ItemColor { get; set; }
        public string Message { get; set; }

        public Point LocationToDraw { get; set; }
    }
}
