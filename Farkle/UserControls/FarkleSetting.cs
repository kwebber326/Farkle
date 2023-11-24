using Farkle.Entities.CustomEventArgs;
using Farkle.Entities.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Farkle.UserControls
{
    public partial class FarkleSetting : UserControl
    {
        private const int HORIZONTAL_MARGIN = 32;
        public event EventHandler<SettingValueEventArgs> ValueChanged;
        public FarkleSetting(string settingName, FarkleSettingType settingType, object defaultValue)
        {
            SettingName = settingName;
            this.SettingType = settingType;
            this.SettingValue = defaultValue;
            InitializeComponent();
        }

        public string SettingName { get; private set; }

        public FarkleSettingType SettingType { get; private set; }
        public object SettingValue { get; private set; }

        public bool ValidateSetting(out string errorMessage)
        {
            errorMessage = string.Empty;
            switch (this.SettingType)
            {
                case FarkleSettingType.BOOLEAN:
                    errorMessage = $"{this.SettingName} must be a valid boolean";
                    return bool.TryParse(this.SettingValue?.ToString(), out bool val);
                case FarkleSettingType.INTEGER:
                    errorMessage = $"{this.SettingName} must be a valid integer";
                    return int.TryParse(this.SettingValue?.ToString(), out int val2);
            }
            return false;
        }

        private void FarkleSetting_Load(object sender, EventArgs e)
        {
            switch (this.SettingType)
            {
                case FarkleSettingType.BOOLEAN:
                    BuildBooleanSettingControl();
                    break;
                case FarkleSettingType.INTEGER:
                    BuildIntegerSettingControl();
                    break;
            }
        }

        private void BuildBooleanSettingControl()
        {
            CheckBox chkBoxSettingVal = new CheckBox();
            chkBoxSettingVal.Checked = bool.TryParse(this.SettingValue?.ToString(), out bool val) ? val : false;
            chkBoxSettingVal.CheckedChanged += ChkBoxSettingVal_CheckedChanged;
            Label lblSettingName = new Label();
            lblSettingName.Text = this.SettingName;
            lblSettingName.Location = new Point(chkBoxSettingVal.Right + HORIZONTAL_MARGIN, chkBoxSettingVal.Location.Y);

            this.Controls.Add(chkBoxSettingVal);
            this.Controls.Add(lblSettingName);
        }

        private void ChkBoxSettingVal_CheckedChanged(object sender, EventArgs e)
        {
            var chkBox = sender as CheckBox;
            if (chkBox != null)
            {
                this.SettingValue = chkBox.Checked;
                OnValueChanged();
            }
        }

        private void BuildIntegerSettingControl()
        {
            Label lblSettingName = new Label();
            lblSettingName.Text = this.SettingName;

            TextBox txtValue = new TextBox();
            txtValue.Text = int.TryParse(this.SettingValue?.ToString(), out int val) ? val.ToString() : "0";
            txtValue.Location = new Point(lblSettingName.Right + HORIZONTAL_MARGIN, lblSettingName.Location.Y);
            txtValue.TextChanged += TxtValue_TextChanged;

            this.Controls.Add(lblSettingName);
            this.Controls.Add(txtValue);
        }

        private void TxtValue_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox != null && int.TryParse(txtBox.Text, out int val))
            {
                this.SettingValue = val;
                OnValueChanged();
            }
        }

        protected virtual void OnValueChanged()
        {
            SettingValueEventArgs e = new SettingValueEventArgs()
            {
                SettingType = this.SettingType,
                SettingValue = this.SettingValue,
                SettingName = this.SettingName
            };
            this.ValueChanged?.Invoke(this, e);
        }
    }
}
