using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities
{
    public class FarkleRuleSet
    {
        public int BreakInMin { get; set; } = 500;

        public int ScoreMin { get; set; } = 0;

        public int PointsToWin { get; set; } = 10000;

        public int Single5Score { get; set; } = 50;

        public int SingleOneScore { get; set; } = 100;

        public int OnesScore { get; set; } = 300;

        public int TwosScore { get; set; } = 200;

        public int ThreesScore { get; set; } = 300;

        public int FoursScore { get; set; } = 400;

        public int FivesScore { get; set; } = 500;

        public int SixesScore { get; set; } = 600;

        public int FourOfAKindScore { get; set; } = 1000;

        public int ThreePairScore { get; set; } = 1500;

        public int StraightScore { get; set; } = 1500;

        public int FullHouseScore { get; set; } = 1500;

        public int DualTripletsScore { get; set; } = 2500;

        public int FiveOfAKindScore { get; set; } = 2000;

        public int SixOfAKindScore { get; set; } = 3000;
    }
}
