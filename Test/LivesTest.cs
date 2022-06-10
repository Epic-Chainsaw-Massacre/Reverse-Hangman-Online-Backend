namespace Test
{
    using Reverse_Hangman_Online_Backend.Classes;


    // unit test
    // integration test

    public class LivesTest
    {
        /*
        In this test you see a word as input and a number as output, the number gets calculated as follows
        example1:
        word = help
        differentLetters = 4 (h=1, e=2, l=3, p=4)
        lives = Math.Floor(differentLetters / 2) + 1
        lives = Math.Floor(4/2) + 1
        Lives = Math.Floor(2) + 1
        Lives = 2 + 1
        Lives = 3

        example2:
        word = horse
        differentLetters = 5 (h=1, o=2, r=3, s=4, e=5)
        lives = Math.Floor(differentLetters / 2) + 1
        lives = Math.Floor(5/2) + 1
        Lives = Math.Floor(2.5) + 1
        Lives = 2 + 1
        Lives = 3
        */
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