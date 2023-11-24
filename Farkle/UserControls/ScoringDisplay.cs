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
    public partial class ScoringDisplay : UserControl
    {
        private SavedFarkleSetting _farkleSetting;
        private const int VERTICAL_OFFSET = 64;
        private const int ROW_COUNT = 6;
        private const int HORIZONTAL_OFFSET = 8;
        private const int LABEL_WIDTH = 200;

        public ScoringDisplay()
        {
            InitializeComponent();
        }

        public SavedFarkleSetting FarkleSetting
        {
            get
            {
                return _farkleSetting;
            }
            set
            {
                _farkleSetting = value;
                SetScoringDisplay();
            }
        }

        private void SetScoringDisplay()
        {
            this.Controls.Clear();
            if (_farkleSetting != null && _farkleSetting.RuleSet != null)
            {
                //set header
                Label lblHeader = new Label();
                lblHeader.ForeColor = Color.White;
                lblHeader.Size = new Size(LABEL_WIDTH, lblHeader.Height);
                lblHeader.Location = new Point(this.Location.X + (this.Width / 2) - 32, this.Location.Y + 2);
                lblHeader.Text = $"Settings: {_farkleSetting.SettingName}";
                this.Controls.Add(lblHeader);

                //set settings
                var scoringProperties = _farkleSetting.RuleSet.GetType().GetProperties();
                int currentY = VERTICAL_OFFSET;
                int currentX = HORIZONTAL_OFFSET;
                int currentRow = 1;
                int longestLabelRight= 0;
                for (int i = 0; i < scoringProperties.Length; i++)
                {
                    Label lblSetting = new Label();
                    lblSetting.ForeColor = Color.White;
                    lblSetting.Location = new Point(currentX, currentY);
                    lblSetting.Size = new Size(LABEL_WIDTH, lblSetting.Height);
                    string settingName = SplitWords(scoringProperties[i].Name);
                    string settingValue = scoringProperties[i].GetValue(_farkleSetting.RuleSet)?.ToString();
                    lblSetting.Text = $"{settingName}: {settingValue}";
                    if (lblSetting.Right > longestLabelRight)
                    {
                        longestLabelRight = lblSetting.Right;
                    }
                    if (++currentRow > ROW_COUNT)
                    {
                        currentX = longestLabelRight + HORIZONTAL_OFFSET;
                        currentY = VERTICAL_OFFSET;
                        currentRow = 1;
                    }
                    else
                    {
                        currentY = lblSetting.Bottom + 2;
                    }
                    this.Controls.Add(lblSetting);
                }
            }
        }
        
        private string SplitWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            int i = 0;
            string capLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "1234567890";

            while(i < text.Length)
            {
                if (i > 0 && (capLetters.Contains(text[i]) || numbers.Contains(text[i])))
                {
                    text = text.Insert(i++, " ");
                }
                i++;
            }
            return text;
        }

        private void ScoringDisplay_Load(object sender, EventArgs e)
        {

        }
    }
}
