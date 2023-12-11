using Farkle.Entities;
using Farkle.File_IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farkle
{
    public partial class PlayerStatsForm : Form
    {
        private List<PlayerStats> _playerStatList = new List<PlayerStats>();
        private List<PlayerStats> _filterStatList = new List<PlayerStats>();
        public PlayerStatsForm()
        {
            InitializeComponent();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _filterStatList = _playerStatList.Where(p => p.PlayerName.Contains(txtSearch.Text)).ToList();
            SetFilteredPlayerList();
        }

        private void PlayerStatsForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            _playerStatList = StatsIO.LoadPlayerStats();
            _filterStatList = _playerStatList;

            SetFilteredPlayerList();
        }

        private void SetFilteredPlayerList()
        {
            lstPlayers.Items.Clear();
            foreach (var stat in _filterStatList)
            {
                lstPlayers.Items.Add(stat.PlayerName);
            }
        }

        private void LstPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var stat = _filterStatList.FirstOrDefault(p => p.PlayerName == lstPlayers.SelectedItem?.ToString());
            if (stat != null)
            {
                playerStatWindow1.SetWindow(stat);
            }
            else
            {
                playerStatWindow1.ClearWindow();
            }
        }
    }
}
