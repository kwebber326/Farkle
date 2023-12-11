using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Farkle.Entities;
using Farkle.File_IO;
using Farkle.UserControls;

namespace Farkle
{
    public partial class MainMenu : Form
    {
        private List<SavedFarkleSetting> _savedSettings = new List<SavedFarkleSetting>();
        public MainMenu()
        {
            InitializeComponent();
        }

        private void FarkleSettingsControl1_Load(object sender, EventArgs e)
        {
           
        }

        private void InitializeSettings()
        {
            if (SettingsUtility.LoadAllSettings(out List<SavedFarkleSetting> settings))
            {
                _savedSettings = settings;
                SavedFarkleSetting defaultRuleSet = new SavedFarkleSetting()
                {
                    SettingName = "Default",
                    RuleSet = new FarkleRuleSet()
                };
                _savedSettings.Insert(0, defaultRuleSet);
                var settingNames = settings.Select(s => s.SettingName).ToArray();
                cmbSettings.Items.AddRange(settingNames);
                cmbSettings.SelectedIndex = 0;
            }
        }

        private void CmbSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            string settingName = cmbSettings.Text;
            var ruleSet = _savedSettings.FirstOrDefault(s => s.SettingName == settingName)?.RuleSet;
            if (ruleSet != null)
            {
                farkleSettingsControl1.CurrentRuleSet = ruleSet;
                txtSettingName.Text = settingName;
            }
            UpdateControlUsabilityFromSelectedSetting(settingName);
        }

        private void UpdateControlUsabilityFromSelectedSetting(string settingName)
        {
            bool isDefault = settingName == "Default";
            farkleSettingsControl1.SetEnabledStatusOfSettings(!isDefault);
            txtSettingName.Enabled = !isDefault;
            btnDelete.Enabled = !isDefault;
            btnSave.Enabled = !isDefault;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            InitializeSettings();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string settingName = txtSettingName.Text;
            var ruleSet = farkleSettingsControl1.CurrentRuleSet;

            if (string.IsNullOrWhiteSpace(settingName))
            {
                MessageBox.Show($"Enter a valid setting name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!farkleSettingsControl1.ValidateAllSettings(out List<string> errorMessages))
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Please correct the following errors before saving:\n");
                foreach (var message in errorMessages)
                {
                    builder.AppendLine(message);
                }
                var validationMessage = builder.ToString();
                MessageBox.Show(validationMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SettingsUtility.SaveSettings(settingName, ruleSet))
            {
                MessageBox.Show($"Setting {settingName} saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!_savedSettings.Any(s => s.SettingName == settingName))
                {
                    _savedSettings.Add(new SavedFarkleSetting() { SettingName = settingName, RuleSet = ruleSet });
                    cmbSettings.Items.Add(settingName);
                    cmbSettings.SelectedIndex = cmbSettings.Items.Count - 1;
                }
            }
            else
            {
                MessageBox.Show($"Setting {settingName} failed to save.  Check the settings and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string settingName = cmbSettings.Text;
            if (MessageBox.Show($"Are you sure you want to delete setting {settingName}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SettingsUtility.DeleteSetting(settingName))
                {
                    MessageBox.Show($"Setting {settingName} successfully deleted!");
                    cmbSettings.Items.Remove(cmbSettings.SelectedItem);
                    cmbSettings.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show($"{settingName} deletion failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            FarkleGame fg = new FarkleGame(farkleSettingsControl1.Players);
            fg.GameSettings = new SavedFarkleSetting()
            {
                SettingName = cmbSettings.Text,
                RuleSet = farkleSettingsControl1.CurrentRuleSet
            };
            if (fg.ShowDialog() == DialogResult.OK)
            {
                //TODO: write game statistics
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            farkleSettingsControl1.CurrentRuleSet = new FarkleRuleSet();
            if (string.IsNullOrWhiteSpace(txtSettingName.Text) || txtSettingName.Text == "Default")
            {
                txtSettingName.Text = "<New Setting>";
                UpdateControlUsabilityFromSelectedSetting(txtSettingName.Text);
                btnDelete.Enabled = false;
            }
        }

        private void BtnViewStats_Click(object sender, EventArgs e)
        {
            PlayerStatsForm playerStatsForm = new PlayerStatsForm();
            playerStatsForm.ShowDialog();
        }
    }
}
