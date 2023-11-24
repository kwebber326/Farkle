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
using Farkle.Entities.GameEvents;

namespace Farkle.UserControls
{
    public partial class DiceTray : UserControl
    {
        private const int HORIZONTAL_MARGIN = 8;
        private const int VERTICAL_MARGIN = 16;
        private int _turnScore;
        private bool _canToggle = false;
        private bool _firstRoll = true;
        private HandValidator _handValidator;
        private FarkleRuleSet _ruleSet;
        private int _rolledDice = 0;
        public event EventHandler<NextTurnEventArgs> TurnOver;
        public event EventHandler<GameLogEntry> ActionOccured;

        private Player _currentPlayer;

        public int TurnScore
        {
            get
            {
                return _turnScore;
            }
            set
            {
                _turnScore = value;
                lblTurnScore.Text = $"Turn Score: {_turnScore}";
            }
        }

        public int TopScore
        {
            get; set;
        }

        public FarkleRuleSet RuleSet
        {
            get
            {
                return _ruleSet;
            }
            set
            {
                _ruleSet = value;
                _handValidator = new HandValidator(_ruleSet);
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
            set
            {
                _currentPlayer = value;
                if (_currentPlayer is CPUPlayer)
                {
                    var cpu = (CPUPlayer)_currentPlayer;
                    if (cpu.BankAction == null)
                    {
                        cpu.BankAction = CPUBankAction;
                    }
                    if (cpu.DiceClickAction == null)
                    {
                        cpu.DiceClickAction = (d) => CPUDiceClickAction(d);
                    }
                    if (cpu.SetAsideAction == null)
                    {
                        cpu.SetAsideAction = CPUSetAsideAction;
                    }
                    if (cpu.RollAction == null)
                    {
                        cpu.RollAction = CPURollAction;
                    }
                }
            }
        }

        public DiceTray()
        {
            InitializeComponent();
        }
        #region CPU Actions
        private void CPUBankAction()
        {
            BtnBank_Click(this, EventArgs.Empty);
        }

        private void CPUSetAsideAction()
        {
            BtnSetAside_Click(this, EventArgs.Empty);
        }

        private void CPURollAction()
        {
            BtnRoll_Click(this, EventArgs.Empty);
        }

        private void CPUDiceClickAction(Dice dice)
        {
            ExecuteDiceShift(dice);
        }
        #endregion

        private void BtnSetAside_Click(object sender, EventArgs e)
        {
            var setAsideDice = GetSetAsideDice();
            var liveDice = GetLiveDice();
            int score = 0;
            if (setAsideDice.Any() && (_handValidator?.ValidateHand(setAsideDice.ToList(), out score) ?? false))
            {
                WriteSetAsideMessage();
                this.TurnScore += score;
                AddDiceToListView();
                pnlSetAsideDice.Controls.Clear();//Clear scored dice
                btnSetAside.Enabled = false;
                //bank enabled only if higher than breakin min or already broken in (needs non-zero value)
                btnBank.Enabled = BankButtonEnabledLogic();

                if (!liveDice.Any())
                {
                    //hot dice condition
                    WriteHotDiceMessage();
                    animationUserControl1.RunHotDiceAnimation(this.CurrentPlayer);
                    this.Reset();
                    btnBank.Enabled = BankButtonEnabledLogic();
                }
                else
                {
                    btnRoll.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("One or more dice selected are not part of hand.  Please correct set-aside selection and try again");
            }

            DisabledButtonsForCPUPlayer();
        }

        private bool BankButtonEnabledLogic()
        {
            return this.CurrentPlayer != null && this.TurnScore > 0 && (this.TurnScore >= _ruleSet.BreakInMin || this.CurrentPlayer.IsBrokenIn);
        }

        private void AddDiceToListView()
        {
            var setAsideDice = GetSetAsideDice();
            if (setAsideDice.Any())
            {
                diceContainer1.InsertDiceToRow(setAsideDice.ToList());
            }
        }

        private void OnTurnOver()
        {
            NextTurnEventArgs e = new NextTurnEventArgs()
            {
                Score = _turnScore
            };
            diceContainer1.ClearContainer();
            this.TurnOver?.Invoke(this, e);
            _canToggle = false;
            _firstRoll = true;
        }

        private void BtnRoll_Click(object sender, EventArgs e)
        {
            _canToggle = true;
            btnRoll.Enabled = false;
            btnBank.Enabled = false;
            var dice = GetLiveDice();
            var setAsideDice = GetSetAsideDice();
            if (setAsideDice.Any() && !(_handValidator?.ValidateHand(setAsideDice.ToList(), out int score) ?? false))
            {
                MessageBox.Show("Cannot roll with invalid board");
                btnRoll.Enabled = false;
                return;
            }
            if (dice.Any())
            {
                RollAllLiveDice(dice);
                WriteRollMessage(dice.Count());
                _firstRoll = false;
            }

            DisabledButtonsForCPUPlayer();
        }

        #region Message Types

        private void WriteRollMessage(int diceRollingCount)
        {
            string message = _firstRoll ? $"{_currentPlayer.Name} begins their turn..."
                : $"{_currentPlayer.Name} Elects to roll with {diceRollingCount} dice remaining...";
            string actionType = CommonConstants.EVENT_ROLL;
            OnActionOccured(message, actionType);
        }

        private void WriteHotDiceMessage()
        {
            string message = $"{_currentPlayer.Name} got hot dice!!!";
            string actionType = CommonConstants.EVENT_HOT_DICE;
            OnActionOccured(message, actionType);
        }

        private void WriteSetAsideMessage()
        {
            var setAsideDice = GetSetAsideDice();

            if (!setAsideDice.Any())
                return;

            var handValid = _handValidator.ValidateHand(setAsideDice.ToList(), out int score);
            if (!handValid)
                return;

            List<string> hands = new List<string>();
            foreach (var dice in setAsideDice)
            {
                var hand = _handValidator.ValidateIndividualDice
                    (dice.FaceValue, setAsideDice.ToList(), out List<string> handsHit);
                foreach (var handHit in handsHit)
                {
                    if ((handHit == CommonConstants.SINGLE_FIVE || handHit == CommonConstants.SINGLE_ONE) || !hands.Contains(handHit))
                        hands.Add(handHit);
                }
            }
            string eventType = CommonConstants.EVENT_SET_ASIDE;
            StringBuilder builder = new StringBuilder();
            foreach (var hand in hands)
            {
                string message = $"{_currentPlayer.Name} sets aside the {hand}";
                //builder.AppendLine(message);
                OnActionOccured(message, eventType);
            }
            builder.AppendLine($"Total Value Added: {score}");
            string logMsg = builder.ToString();

            OnActionOccured(logMsg, eventType);
        }

        private void WriteBankMessage()
        {
            string message = $"{_currentPlayer.Name} has elected to bank {this.TurnScore} points";
            string eventType = CommonConstants.EVENT_BANK;
            OnActionOccured(message, eventType);
        }

        private void WriteFarkleMessage(int diceRemaining)
        {
            string message = $"{_currentPlayer.Name} Farkled with {diceRemaining} dice remaining and lost {this.TurnScore} points!";
            string eventType = CommonConstants.EVENT_FARKLE;
            OnActionOccured(message, eventType);
        }

        #endregion

        private void DisabledButtonsForCPUPlayer()
        {
            if (this.CurrentPlayer is CPUPlayer)
            {
                btnBank.Enabled = false;
                btnRoll.Enabled = false;
                btnSetAside.Enabled = false;
            }
        }

        private void RollAllLiveDice(IEnumerable<Dice> dice)
        {
            _rolledDice = dice.Count();
            btnSetAside.Enabled = false;
            foreach (var d in dice)
            {
                d.Roll();
            }
        }

        private void BtnBank_Click(object sender, EventArgs e)
        {
            WriteBankMessage();
            OnTurnOver();
            this.TurnScore = 0;
            this.Reset();
        }

        private void DiceTray_Load(object sender, EventArgs e)
        {
            this.TurnScore = 0;
            this.Reset();
            btnBank.Enabled = false;
        }

        private IEnumerable<Dice> GetLiveDice()
        {
            return pnlLiveDice.Controls.OfType<Dice>();
        }

        private IEnumerable<Dice> GetSetAsideDice()
        {
            return pnlSetAsideDice.Controls.OfType<Dice>();
        }

        public void Reset()
        {
            DestroyCurrentDice();
            AddNewDiceToTray();
            btnSetAside.Enabled = false;
            if (!(this.CurrentPlayer is CPUPlayer))
            {
                btnRoll.Enabled = true;
                btnBank.Enabled = this.TurnScore > 0;
            }
            else
            {
                var liveDice = GetLiveDice();
                RollAllLiveDice(liveDice);
                WriteRollMessage(liveDice.Count());
                _firstRoll = false;
            }

            DisabledButtonsForCPUPlayer();
        }

        private void AddNewDiceToTray()
        {
            int x = HORIZONTAL_MARGIN;
            int y = VERTICAL_MARGIN;
            for (int i = 0; i < 6; i++)
            {
                Dice dice = new Dice();
                dice.Location = new Point(x, y);
                pnlLiveDice.Controls.Add(dice);
                dice.DiceRolled += Dice_DiceRolled;
                dice.Click += Dice_Click;

                x = dice.Right + HORIZONTAL_MARGIN;
            }
        }

        private void Dice_Click(object sender, EventArgs e)
        {
            if (!_canToggle || this.CurrentPlayer is CPUPlayer)
                return;

            IEnumerable<Dice> setAsideDice = ExecuteDiceShift(sender);

            btnSetAside.Enabled = setAsideDice.Any();
        }

        private IEnumerable<Dice> ExecuteDiceShift(object sender)
        {
            var dice = sender as Dice;
            var liveDice = GetLiveDice();
            var setAsideDice = GetSetAsideDice();
            if (dice != null)
            {
                if (liveDice.Any() && liveDice.Contains(dice))
                {
                    pnlLiveDice.Controls.Remove(dice);
                    pnlSetAsideDice.Controls.Add(dice);
                }
                else if (setAsideDice.Any() && setAsideDice.Contains(dice))
                {
                    pnlSetAsideDice.Controls.Remove(dice);
                    pnlLiveDice.Controls.Add(dice);
                }
            }

            return setAsideDice;
        }

        private void DestroyCurrentDice()
        {
            var liveDice = GetLiveDice();
            var setAsideDice = GetSetAsideDice();
            if (liveDice.Any())
            {
                foreach (var dice in liveDice)
                {
                    dice.DiceRolled -= Dice_DiceRolled;
                }
            }
            if (setAsideDice.Any())
            {
                foreach (var dice in setAsideDice)
                {
                    dice.DiceRolled -= Dice_DiceRolled;
                }
            }
            pnlLiveDice.Controls.Clear();
            pnlSetAsideDice.Controls.Clear();
        }

        private void OnActionOccured(string message, string eventType)
        {
            GameLogEntry gameLogEntry = new GameLogEntry()
            {
                TimeStamp = DateTime.Now,
                Message = message,
                EventType = eventType
            };
            this.ActionOccured?.Invoke(this, gameLogEntry);
        }

        private void Dice_DiceRolled(object sender, Entities.GameEvents.DiceRollEventArgs e)
        {
            var dice = sender as Dice;
            if (dice != null)
            {
                btnRoll.Enabled = false;
                if (--_rolledDice == 0)//All dice rolled, check for farkle here
                {
                    btnSetAside.Enabled = true;

                    var liveDice = GetLiveDice().ToList();
                    if (!(_handValidator?.HasAnyThing(liveDice) ?? false))
                    {
                        //Farkled
                        WriteFarkleMessage(liveDice.Count);
                        animationUserControl1.RunFarkleAnimation(this.CurrentPlayer);
                        this.TurnScore = 0;
                        OnTurnOver();
                        this.Reset();
                        if (this.CurrentPlayer is CPUPlayer)
                        {
                            RollAllLiveDice(GetLiveDice());
                        }
                        else
                        {
                            btnRoll.Enabled = true;
                        }
                    }
                    else if (this.CurrentPlayer is CPUPlayer)
                    {
                        ((CPUPlayer)this.CurrentPlayer).OnRolledNoFarkle(liveDice, _turnScore, this.TopScore);
                    }
                }
            }

            DisabledButtonsForCPUPlayer();
        }
    }
}
