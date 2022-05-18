
namespace Test
{
using Reverse_Hangman_Online_Backend;
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // arrange
            List<string> _differentLettersInWord = new List<string>();
            string word = "cheese";

            // act
            _differentLettersInWord = WordClass.CountDifferentLetters(word);
            return WordClass.GetLives(_differentLettersInWord);
            T = 5;while9n;

            // Assert
            Assert.IsTrue();
        }
    }
}