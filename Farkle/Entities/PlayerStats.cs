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
        [DisplayOrder(Order = 1)]
        public string PlayerName { get; set; }

        [WriteData(WriteToGameStats =false, WriteToPlayerStats =true)]
        [DisplayOrder(Order = 2)]
        public int GamesPlayed { get; set; }

        [WriteData(WriteToGameStats = false, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 3)]
        public int Wins { get; set; }
        [WriteData(WriteToGameStats = false, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 4)]
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
        [DisplayOrder(Order = 20)]
        public long Farkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 21)]
        public long SixDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 22)]
        public long FiveDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 23)]
        public long FourDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 24)]
        public long ThreeDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 25)]
        public long TwoDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 26)]
        public long OneDiceFarkles { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 27)]
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
        [DisplayOrder(Order = 5)]
        public long TurnsTaken { get; set; }

        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 28)]
        public long Rolls { get; set; }

        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 6)]
        public long TotalPointsScored { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 7)]
        public decimal AverageScorePerTurn
        {
            get
            {
                if (TurnsTaken == 0)
                    return 0;

                return Math.Round((decimal)TotalPointsScored / (decimal)TurnsTaken, 2);
            }
        }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 8)]
        public int HotDiceCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 9)]
        public int ThreeOfAkindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 10)]
        public int FourOfAKindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 11)]
        public int FiveOfAKindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 12)]
        public int SixOfAKindCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 13)]
        public int TripletsCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 14)]
        public int ThreePairCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 15)]
        public int FullHouseCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 16)]
        public int StraightCount { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 17)]
        public int LargestBankedScore { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 18)]
        public int LargestFarkledScore { get; set; }
        [WriteData(WriteToGameStats = true, WriteToPlayerStats = true)]
        [DisplayOrder(Order = 19)]
        public double AggresionIndex { get; set; }

        public void CalculateRunningAverageAgressiveIndex(int turnScore, double expectedValueOfLastRoll)
        {
            var tScore = (double)(turnScore == 0 ? 1 : turnScore);
            var agressionIndex = ((double)turnScore - expectedValueOfLastRoll) / tScore;
            var weightedIndex = (1.0 / ((double)this.Rolls + 1.0)) * agressionIndex;
            var oldWeightedIndex = (double)(this.Rolls) / (double)(this.Rolls + 1.0) * this.AggresionIndex;

            this.AggresionIndex = oldWeightedIndex + weightedIndex;
            this.Rolls++;
        }
    }
}
