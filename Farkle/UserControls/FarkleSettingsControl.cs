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
using Farkle.Entities.Enums;
using System.Diagnostics;

namespace Farkle.UserControls
{
    public partial class FarkleSettingsControl : UserControl
    {
        private const int VERTICAL_MARGIN = 2;
        private int _playerCount;
        private FarkleRuleSet _defaultRuleSet = new FarkleRuleSet();
        private FarkleRuleSet _currentRuleSet;

        public FarkleRuleSet CurrentRuleSet
        {
            get
            {
                return _currentRuleSet;
            }
            set
            {
                _currentRuleSet = value;
                BuildSettingsPanelFromCurrentRuleSet();
            }
        }

        public List<Player> Players
        {
            get
            {
                var playerNameControls = this.Controls.OfType<PlayerNameControl>();
                var players = playerNameControls.Any() ? 
                    playerNameControls
                    .Where(p => _playerCount >= p.Number)
                    .OrderBy(player => player.Number)
                    .Select(p1 =>
                        p1.IsCpu ?
                            new CPUPlayer(new HandValidator(_currentRuleSet))
                            {
                                Name = p1.PlayerName,
                                Score = 0,
                                Position = 1
                            } :
                            new Player()
                            {
                                Name = p1.PlayerName,
                                Score = 0,
                                Position = 1
                            }
                    ).ToList() 
                    : new List<Player>();

                if (players.Any())
                {
                    players[0].IsMyTurn = true;
                }

                return players;
            }
        }

        public FarkleSettingsControl()
        {
            InitializeComponent();
        }

        public bool ValidateAllSettings(out List<string> errorMessages)
        {
            errorMessages = new List<string>();
            bool validationSuccess = true;
            foreach (var control in pnlSettings.Controls)
            {
                FarkleSetting setting = control as FarkleSetting;
                if (setting != null && !setting.ValidateSetting(out string message))
                {
                    validationSuccess = false;
                    errorMessages.Add(message);
                }
            }
            return validationSuccess;
        }

        public void SetEnabledStatusOfSettings(bool enabled)
        {
            pnlSettings.Enabled = enabled;
        }

        private void FarkleSettingsControl_Load(object sender, EventArgs e)
        {
            cmbPlayers.SelectedIndex = 1;
            SetPlayerCountBySelection();
            _currentRuleSet = _defaultRuleSet;
            BuildSettingsPanelFromCurrentRuleSet();
        }

        private void SetPlayerCountBySelection()
        {
            if (int.TryParse(cmbPlayers.SelectedItem?.ToString(), out int val))
            {
                _playerCount = val;
            }
        }

        private void BuildSettingsPanelFromCurrentRuleSet()
        {
            pnlSettings.Controls.Clear();
            var propertyList = _currentRuleSet.GetType().GetProperties();
            int currentY = 0;
            foreach (var prop in propertyList)
            {
                FarkleSettingType settingType = FarkleSettingType.BOOLEAN;
                if (prop.PropertyType == typeof(bool))
                {
                    settingType = FarkleSettingType.BOOLEAN;
                }
                else if (prop.PropertyType == typeof(int))
                {
                    settingType = FarkleSettingType.INTEGER;
                }
                FarkleSetting setting = new FarkleSetting(prop.Name, settingType, prop.GetValue(_currentRuleSet));
                pnlSettings.Controls.Add(setting);
                setting.ValueChanged += Setting_ValueChanged;
                setting.Location = new Point(0, currentY);
                currentY = setting.Bottom + VERTICAL_MARGIN;
            }
        }

        private void Setting_ValueChanged(object sender, Entities.CustomEventArgs.SettingValueEventArgs e)
        {
            try
            {
                _currentRuleSet.GetType().GetProperty(e.SettingName)?.SetValue(_currentRuleSet, e.SettingValue);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void CmbPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPlayerCountBySelection();
            SetPlayerNameControlsVisibilty();
        }

        private void SetPlayerNameControlsVisibilty()
        {
            var playerNameControls = this.Controls.OfType<PlayerNameControl>();
            if (playerNameControls.Any())
            {
                foreach (var control in playerNameControls)
                {
                    control.Visible = _playerCount >= control.Number;
                }
            }
        }

        public int PlayerCount
        {
            get
            {
                return _playerCount;
            }
        }
    }
}
