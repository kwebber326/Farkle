using Farkle.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public class PlayerStats
    {
        public string PlayerName { get; set; }

        [WriteData(WriteToGameStats =false, WriteToPlayerStats =true)]
        public int GamesPlayed { get; set; }

        [WriteData(WriteToGameStats = false, WriteToPlayerStats = true)]
        public int Wins { get; set; }
        [WriteData(WriteToGameStats = false, WriteToPlayerStats = true)]
        public decimal WinPct
        {
            get
            {
                if (GamesPlayed == 0)
                    return 0;

                return Math.Round(((decimal)Wins / (decimal)GamesPlayed) * 100, 2);
            }
        }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long Farkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long SixDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long FiveDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long FourDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long ThreeDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long TwoDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long OneDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public decimal FarklePct
        {
            get
            {
                if (TurnsTaken == 0)
                    return 0;

                return Math.Round(((decimal)Farkles / (decimal)TurnsTaken) * 100, 2);
            }
        }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long TurnsTaken { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public long TotalScore { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public decimal AverageScorePerTurn
        {
            get
            {
                if (TurnsTaken == 0)
                    return 0;

                return Math.Round((decimal)TotalScore / (decimal)TurnsTaken, 2);
            }
        }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int HotDiceCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int ThreeOfAkindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int FourOfAKindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int FiveOfAKindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int SixOfAKindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int TripletsCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int ThreePairCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int FullHouseCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int StraightCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int LargestBankedScore { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public int LargestFarkledScore { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        public double AggresionIndex { get; set; }

        public void CalculateRunningAverageAgressiveIndex(int turnScore, double expectedValueOfLastRoll)
        {
            var tScore = (double)(turnScore == 0 ? 1 : turnScore);
            var agressionIndex = (turnScore - expectedValueOfLastRoll) / tScore;
            var weightedIndex = (1 / (this.TurnsTaken + 1)) * agressionIndex;
            var oldWeightedIndex = (this.TurnsTaken) / (this.TurnsTaken + 1) * this.AggresionIndex;

            this.AggresionIndex = oldWeightedIndex + weightedIndex;
            this.TurnsTaken++;
        }
    }
}
