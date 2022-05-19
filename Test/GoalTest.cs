namespace Test
{
    using Reverse_Hangman_Online_Backend.Classes;


    // unit test
    // integration test

    public class GoalTest
    {
        [Test]
        [TestCase("help", "goal < 8")]
        [TestCase("helps", "goal < 10")]
        [TestCase("helping", "goal < 14")]
        public void CalculateGoalTest(string word, string expectedValue)
        {
            // arrange
            GameSetup _gameSetup = new GameSetup();
            string _word = word;

            // act
            _gameSetup.differentLettersInWord = WordClass.CountDifferentLetters(_word);
            int goal = _gameSetup.game.teamCollection.GetTeamList()[0].GuessCollection.CalculateGoal(_gameSetup.differentLettersInWord);
            string goalString = "goal < " + goal;

            // assert
            Assert.IsTrue(goalString == expectedValue);
        }
    }
}