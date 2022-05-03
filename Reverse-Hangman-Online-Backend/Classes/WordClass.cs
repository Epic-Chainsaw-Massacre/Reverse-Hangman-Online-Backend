namespace Reverse_Hangman_Online_Backend.Classes
{
    public class WordClass
    {
        // Properties
        public string Word { get; private set; }
        public bool Exists { get; private set; }

        // Methods
        public WordClass(string word, bool exists)
        {
            Word = word;
            Exists = exists;
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
    }
}
