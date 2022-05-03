namespace Reverse_Hangman_Online_Backend.Classes
{
    public class Team
    {
        int _maxLives;


        // Properties
        public int Lives { get; private set; }

        public void CalculateLives(List<string> differentLetters)
        {
            Lives = Convert.ToInt32(Math.Floor(differentLetters.Count / 2.0) + 1);
            _maxLives = Lives;
        }
    }
}
