namespace Test
{
    using Reverse_Hangman_Online_Backend.Classes;


    // unit test
    // integration test

    public class LivesTest
    {
        [Test]
        [TestCase("help", 3)]
        [TestCase("horse", 3)]
        [TestCase("clover", 4)]
        public void WordWith4DifferentLetters(string word, int expectedLives)
        {
            // arrange
            GameSetup _gameSetup = new GameSetup();
            string _word = word;

            // act
            _gameSetup.differentLettersInWord = WordClass.CountDifferentLetters(_word);
            _gameSetup.game.teamCollection.GetTeamList()[0].CalculateLives(_gameSetup.differentLettersInWord);

            // assert
            Assert.IsTrue(_gameSetup.game.teamCollection.GetTeamList()[0].Lives == expectedLives);
        }
    }
}