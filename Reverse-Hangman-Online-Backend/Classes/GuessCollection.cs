namespace Reverse_Hangman_Online_Backend.Classes
{
    public class GuessCollection
    {
        // Fields
        public List<string> notGuessedLetters = new List<string>();
        public List<string> guessedLetters = new List<string>();
        WordClass _wordClass;

        // Properties
        public int Goal { get; private set; }

        // Methods
        public GuessCollection()
        {
            AddAllLettersOfTheAlfabet(notGuessedLetters);
        }

        public void SetWord(WordClass wordClass)
        {
            _wordClass = wordClass;
        }

        public void SaveGuess(Guess guess)
        {
            guessedLetters.Add(guess.Letter);
            notGuessedLetters.Remove(guess.Letter);
        }

        public int CalculateGoal(List<string> differentLetters)
        {
            Goal = differentLetters.Count * 2;
            return Goal;
        }

        public bool UnderGoal()
        {
            return (26 - guessedLetters.Count < Goal);
        }

        public bool GuessedOutAllLetters()
        {
            int AmountOfNotGuessedLetters = notGuessedLetters.Count;
            int AmountOfNotGuessedLettersThatTheWordContains = 0;

            // Counts how many letters remaining are actually in the word, if its the same amount of remaining letters that means you are done with guessing
            foreach (string letter in notGuessedLetters)
            {
                for (int i = 0; i < _wordClass.Word.Length; i++)
                {
                    if (letter == _wordClass.Word.Substring(i, 1))
                    {
                        AmountOfNotGuessedLettersThatTheWordContains++;
                        break;
                    }
                }
            }

            if (AmountOfNotGuessedLettersThatTheWordContains == AmountOfNotGuessedLetters)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void AddAllLettersOfTheAlfabet(List<string> someList)
        {
            someList.Add("A");
            someList.Add("B");
            someList.Add("C");
            someList.Add("D");
            someList.Add("E");
            someList.Add("F");
            someList.Add("G");
            someList.Add("H");
            someList.Add("I");
            someList.Add("J");
            someList.Add("K");
            someList.Add("L");
            someList.Add("M");
            someList.Add("N");
            someList.Add("O");
            someList.Add("P");
            someList.Add("Q");
            someList.Add("R");
            someList.Add("S");
            someList.Add("T");
            someList.Add("U");
            someList.Add("V");
            someList.Add("W");
            someList.Add("X");
            someList.Add("Y");
            someList.Add("Z");
        }
    }
}
