namespace Test
{
    using Reverse_Hangman_Online_Backend.Classes;


    // unit test
    // integration test

    public class GuessLineTest
    {
        [Test]
        [TestCase("help", "_ _ _ _ ")]
        [TestCase("horse", "_ _ _ _ _ ")]
        [TestCase("clover", "_ _ _ _ _ _ ")]
        public void CalculateGoalTest(string word, string expectedValue)
        {
            // arrange
            WordClass _wordClass = new WordClass(word);
            string _guessLine;

            // act
            _guessLine = _wordClass.CalculateWordStripes();

            // assert
            Assert.AreEqual(expectedValue, _guessLine);
        }
    }
}