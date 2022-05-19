namespace Reverse_Hangman_Online_Backend.Classes
{
    public class WordClass
    {
        // Fields
        GuessCollection _guessCollection;
        string _stripes = "_ x ?";

        // Properties
        public string Word { get; private set; }
        public string Stripes { get { return _stripes; } }

        // Methods
        public WordClass(string word)
        {
            Word = word;
        }
        public WordClass(string word, GuessCollection guessCollection)
        {
            Word = word;
            _guessCollection = guessCollection;
        }

        public string CalculateLifeStripes(int lives)
        {
            string livesText = "";
            for (int i = 0; i < lives; i++)
            {
                livesText += "♥";
            }
            return livesText;
        }

        public string CalculateWordStripes()
        {
            string stripes = "";
            foreach (char letter in Word)
            {
                stripes += "_ ";
            }
            return stripes;
        }

        public bool CheckIfWordContainsLetter(string myLetter)
        {
            _stripes = "";
            bool guessIsInWord = false;
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word.Substring(i, 1).ToUpper() == myLetter)
                {
                    _stripes += myLetter + " ";
                    guessIsInWord = true;
                }
                else if (_guessCollection.guessedLetters.Contains(Word.Substring(i, 1).ToUpper()))
                {
                    for (int j = 0; j < _guessCollection.guessedLetters.Count; j++)
                    {
                        AddGuessedLettersToWord(i, j);
                    }
                }
                else
                {
                    _stripes += "_ ";
                }
            }
            return guessIsInWord;
        }

        static public List<string> CountDifferentLetters(string word)
        {
            List<string> differentLetters = new List<string>();
            for (int i = 0; i < word.Length; i++)
            {
                string letter = word.Substring(i, 1).ToUpper();

                if (i == 0) // first time dont check since its empty
                {
                    differentLetters.Add(letter);
                }
                else
                {
                    if (!differentLetters.Contains(letter))
                    {
                        differentLetters.Add(letter);
                    }
                }
            }
            return differentLetters;
        }

        void AddGuessedLettersToWord(int i, int j)
        {
            if (_guessCollection.guessedLetters[j] == Word.Substring(i, 1).ToUpper())
            {
                _stripes += _guessCollection.guessedLetters[j] + " ";
            }
        }
    }
}
