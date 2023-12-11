using Farkle.Entities;
using Farkle.Entities.Utilities;
using Farkle.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Farkle.File_IO;
using Farkle.Entities.CustomEventArgs;

namespace Farkle
{
    public partial class FarkleGame : Form
    {
        private readonly List<Player> _players;
        private Dictionary<string, PlayerStats> _playerStats;
        private const int MARGIN = 8;
        private int _currentPlayerIndex = 0;
        private bool _equalTurns = true;


        public FarkleGame(List<Player> players)
        {
            InitializeComponent();
            _players = players;
            diceTray1.CurrentPlayer = _players.FirstOrDefault();
            diceTray1.ActionOccured += DiceTray1_ActionOccured;
        }

        private void DiceTray1_ActionOccured(object sender, GameActionEventArgs e)
        {
            gameLogUserControl1.LogEntry(e.LogEntryData);

            if (!_playerStats.TryGetValue(diceTray1.CurrentPlayer.Name, out PlayerStats stat))
                return;

            switch (e.LogEntryData?.EventType)
            {
                case CommonConstants.EVENT_FARKLE:
                    ManageTurnEndStatistics(e, stat);
                    ProcessFarkleStatistics(e, stat);
                    break;
                case CommonConstants.EVENT_BANK:
                    ManageTurnEndStatistics(e, stat);
                    break;
                case CommonConstants.EVENT_HOT_DICE:
                    stat.HotDiceCount++;
                    break;
            }
        }

        private static void ProcessFarkleStatistics(GameActionEventArgs e, PlayerStats stat)
        {
            switch (e.DiceRemaining)
            {
                case 1:
                    stat.OneDiceFarkles++;
                    break;
                case 2:
                    stat.TwoDiceFarkles++;
                    break;
                case 3:
                    stat.ThreeDiceFarkles++;
                    break;
                case 4:
                    stat.FourDiceFarkles++;
                    break;
                case 5:
                    stat.FiveDiceFarkles++;
                    break;
                case 6:
                    stat.SixDiceFarkles++;
                    break;
            }

            stat.Farkles++;
        }

        private static void ManageTurnEndStatistics(GameActionEventArgs e, PlayerStats stat)
        {
            stat.TurnsTaken++;
            if (stat.LargestBankedScore < e.TurnScore)
            {
                stat.LargestBankedScore = e.TurnScore;
            }
        }

        public SavedFarkleSetting GameSettings
        {
            get
            {
                return scoringDisplay1.FarkleSetting;
            }
            set
            {
                scoringDisplay1.FarkleSetting = value;
                diceTray1.RuleSet = value.RuleSet;
                if (value.RuleSet.BreakInMin <= 0)
                {
                    foreach (var player in _players)
                    {
                        player.IsBrokenIn = true;
                    }
                }
            }
        }


        private void FarkleGame_Load(object sender, EventArgs e)
        {
            int currentY = scoringDisplay1.Bottom + MARGIN;
            int currentX = scoringDisplay1.Location.X;

            foreach (var player in _players)
            {
                PlayerControl pc = new PlayerControl(player);
                pc.Location = new Point(currentX, currentY);
                currentX = pc.Right + MARGIN;
                this.Controls.Add(pc);
            }

            diceTray1.TurnOver += DiceTray1_TurnOver;
            saveFileDialogGameLog.InitialDirectory = Environment.CurrentDirectory + @"/" + "GameLogs";
            saveFileDialogGameLog.Title = "Save Game Log:";

            InitializePlayerStats();
        }

        private void DiceTray1_TurnOver(object sender, Entities.GameEvents.NextTurnEventArgs e)
        {
            var players = this.Controls.OfType<PlayerControl>().ToList();
            players[_currentPlayerIndex].Score += e.Score;
            players[_currentPlayerIndex].IsMyTurn = false;
            if (players[_currentPlayerIndex].Score >= this.GameSettings.RuleSet.BreakInMin)
            {
                players[_currentPlayerIndex].IsBrokenIn = true;
            }
            if (++_currentPlayerIndex >= _players.Count)
            {
                _currentPlayerIndex = 0;
                _equalTurns = true;
            }
            else
            {
                _equalTurns = false;
            }
            players[_currentPlayerIndex].IsMyTurn = true;
            diceTray1.CurrentPlayer = players[_currentPlayerIndex].Player;

            var playersByScore = players.OrderByDescending(p => p.Score).ToList();
            diceTray1.TopScore = playersByScore.First().Score;
            foreach (var player in players)
            {
                player.Position = playersByScore.IndexOf(player) + 1;

            }

            if (_equalTurns && _players.Any(p => p.Score >= this.GameSettings.RuleSet.PointsToWin))
            {
                //Game Over: show standings
                this.ShowEndGameDisplay();

                //TODO: compile statistics for Player Names
                var winningPlayer = playersByScore.FirstOrDefault().Player;
                var winningStatSheet = _playerStats[winningPlayer.Name];
                winningStatSheet.Wins++;

                ProcessGameStatistics();
                StatsIO.SavePlayerStats(_playerStats.Values.ToList());

                //Turn off CPU events
                _players.Clear();

                this.Close();
            }
        }

        private void ProcessGameStatistics()
        {
            foreach (var player in _players)
            {
                string playerName = player.Name;
                if (_playerStats.ContainsKey(playerName))
                {
                    var stat = _playerStats[playerName];
                    stat.GamesPlayed++;
                    stat.TotalPointsScored += player.Score;
                }
            }
        }

        private void InitializePlayerStats()
        {
            var stats = StatsIO.LoadPlayerStats();
            _playerStats = stats.ToDictionary((s) => s.PlayerName);
            Dictionary<string, PlayerStats> filteredList = new Dictionary<string, PlayerStats>();
            foreach (var player in _players)
            {
                var playerStat = _playerStats.TryGetValue(player.Name, out PlayerStats stat) ? stat : null;
                if (playerStat == null)
                {
                    playerStat = new PlayerStats()
                    {
                        PlayerName = player.Name
                    };
                }
                if (!filteredList.ContainsKey(player.Name))
                    filteredList.Add(player.Name, playerStat);
            }
            _playerStats = filteredList;
        }

        private void WritePlayerWinnerMessage(string winningPlayer, int winningScore)
        {
            GameLogEntry winnerEntry = new GameLogEntry()
            {
                Message = $"{winningPlayer} has won the game with {winningScore} points!!!",
                EventType = CommonConstants.EVENT_GAME_OVER
            };

            gameLogUserControl1.LogEntry(winnerEntry);
        }

        private void WritePlayerParticipantMessage(string player, int score)
        {
            GameLogEntry winnerEntry = new GameLogEntry()
            {
                Message = $"{player} scored {score} points.",
                EventType = CommonConstants.EVENT_GAME_OVER
            };

            gameLogUserControl1.LogEntry(winnerEntry);
        }



        private void ShowEndGameDisplay()
        {
            StringBuilder builder = new StringBuilder();
            var scores = _players.Select(p => p.Score).Distinct().OrderByDescending(s => s).ToList();
            int playerCount = _players.Count;
            int playersAccountedFor = 0;
            int position = 1;

            builder.AppendLine("The game is over! Here are the results:\n");
            for (int i = 0; i < scores.Count; i++)
            {
                string prefix = string.Empty;
                var playersWithThisScore = _players.Where(p => p.Score == scores[i]).ToList();
                if (playersWithThisScore.Count > 1)
                {
                    prefix += "T-";
                }
                prefix += FarkleUtilities.GetPositionText(position).Replace("Position: ", "");
                string playersText = string.Join(", ", playersWithThisScore.Select(p => p.Name).ToArray());
                if (i == 0)
                {
                    WritePlayerWinnerMessage(playersText, scores[i]);
                }
                else
                {
                    WritePlayerParticipantMessage(playersText, scores[i]);
                }
                string lineItem = $"{prefix}: {playersText} - {scores[i]} points";
                builder.AppendLine(lineItem);
                playersAccountedFor += playersWithThisScore.Count;
                position += playersWithThisScore.Count;

            }
            string message = builder.ToString();
            MessageBox.Show(builder.ToString());
            if (gameLogUserControl1.ExportOnGameOver && saveFileDialogGameLog.ShowDialog() == DialogResult.OK)
            {
                WriteGameLogToFile();
            }
        }

        private void WriteGameLogToFile()
        {
            string fileName = saveFileDialogGameLog.FileName;

            using (FileStream fs = File.OpenWrite(fileName))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                var messages = gameLogUserControl1.GetAllMessages();
                foreach (string msg in messages)
                {
                    writer.WriteLine(msg);
                }
            }
        }
    }
}
