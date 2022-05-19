namespace Reverse_Hangman_Online_Backend.Classes
{
    public class Team
    {
        // Fields
        static Random _rnd = new Random();
        GuessCollection _guessCollection;
        int _maxLives;
        bool _continueAfterSweep = false;
        int _pointsYouWillGain = 0;

        // Properties
        public GuessCollection GuessCollection { get { return _guessCollection; } }
        public bool EndRound { get; set; }
        public bool WonTieBreaker { get; set; }
        public string Name { get; private set; }
        public int Score { get; private set; }
        public int Lives { get; private set; }
        public Roles Role { get; set; }

        // Methods
        public Team(string name, GuessCollection guessCollection)
        {
            Name = name;
            Score = 0;
            _guessCollection = guessCollection;
        }

        public static string CreateRandomTeamName()
        {
            string randomTeamName = "";
            randomTeamName += AddRandomAdjective(_rnd);
            randomTeamName += AddBlankSpace();
            randomTeamName += AddRandomNoun(_rnd);
            return randomTeamName;
        }

        static string AddRandomAdjective(Random rnd)
        {
            Array values = Enum.GetValues(typeof(Adjectives));
            string randomAdjective = "" + (Adjectives)values.GetValue(rnd.Next(values.Length));
            return randomAdjective;
        }

        static string AddBlankSpace()
        {
            string blankSpace = " ";
            return blankSpace;
        }

        static string AddRandomNoun(Random rnd)
        {
            Array values = Enum.GetValues(typeof(Nouns));
            string randomNoun = "" + (Nouns)values.GetValue(rnd.Next(values.Length));
            return randomNoun;
        }

        public void LoseLife()
        {
            Lives--;
        }

        public void CalculateLives(List<string> differentLetters)
        {
            Lives = Convert.ToInt32(Math.Floor(differentLetters.Count / 2.0) + 1);
            _maxLives = Lives;
        }

        public void EndOfGuess()
        {
            if (_continueAfterSweep == false)
            {
                if (Lives == 0 && _guessCollection.UnderGoal())
                {
                    //MessageBox.Show("Tiebreaker, choose a microgame");
                    //
                    //
                    //
                    // TIEBREAKER
                    //
                    //
                    //
                    //DialogResult dialogResult = MessageBox.Show("Did guesser win the tiebreaker?", "Thinking moment", MessageBoxButtons.YesNo);

                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    //MessageBox.Show("epic moment +1 point");
                    //    _pointsYouWillGain = 1;
                    //    EndRound = true;
                    //}
                    //else if (dialogResult == DialogResult.No)
                    //{
                    //    //MessageBox.Show("Sadge +0 points");
                    //    _pointsYouWillGain = 0;
                    //    EndRound = true;
                    //}
                }
                else if (Lives == 0)
                {
                    //MessageBox.Show("Loser");
                    _pointsYouWillGain = 0;
                    EndRound = true;
                }
                else if (Lives == _maxLives && _guessCollection.UnderGoal())
                {
                    //DialogResult dialogResult = MessageBox.Show("Continue for double or nothing?", "Hardest decision ever", MessageBoxButtons.YesNo);

                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    //MessageBox.Show("Lets continue then");
                    //    _continueAfterSweep = true;
                    //}
                    //else if (dialogResult == DialogResult.No)
                    //{
                    //    //MessageBox.Show("Bitch");
                    //    //MessageBox.Show("Noble Sweep +2 points?");
                    //    EndRound = true;
                    //}
                    _pointsYouWillGain = 2;
                }
                else if (_guessCollection.UnderGoal())
                {
                    //DialogResult dialogResult = MessageBox.Show("Continue?", "Some Title", MessageBoxButtons.YesNo);

                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    //MessageBox.Show("Lets continue then double 'r nothing");
                    //    _continueAfterSweep = true;
                    //}
                    //else if (dialogResult == DialogResult.No)
                    //{
                    //    //MessageBox.Show("Bitch");
                    //    //MessageBox.Show("Its just a sweep +1");
                    //    EndRound = true;
                    //}
                    _pointsYouWillGain = 1;
                }
            }
            if (_continueAfterSweep == true)
            {
                if (Lives == 0)
                {
                    //MessageBox.Show("Failed clean sweep noob"); // randomize some funny fail messages
                    // failed clean sweep +0
                    _pointsYouWillGain *= 0;
                    EndRound = true;
                }
                else if (Lives == _maxLives && _guessCollection.GuessedOutAllLetters())
                {
                    //MessageBox.Show("Royal Sweep +6");
                    _pointsYouWillGain = 6;
                    EndRound = true;
                }
                else if (Lives > 0 && _guessCollection.GuessedOutAllLetters())
                {
                    //MessageBox.Show("Clean Sweep +2");
                    _pointsYouWillGain *= 2;
                    EndRound = true;
                }
            }
            if (EndRound == true)
            {
                AddScore(_pointsYouWillGain);
            }
        }

        public void ResetAllValues()
        {
            ResetGuessCollection();
            _continueAfterSweep = false;
        }

        void ResetGuessCollection()
        {
            _guessCollection = new GuessCollection();
        }

        void AddScore(int gainedPointsThisRound)
        {
            Score += gainedPointsThisRound;
        }
    }
}
