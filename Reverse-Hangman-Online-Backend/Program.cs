using Microsoft.AspNetCore.Mvc;
using Reverse_Hangman_Online_Backend.Classes;
using System.Web.Http;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "https://localhost:7054")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// SETUP
GameSetup gameSetup = new GameSetup();


// ROUTING -> https://localhost:7054
app.MapGet("/Lives", ([FromUri] string word) =>
{
    gameSetup.differentLettersInWord = WordClass.CountDifferentLetters(word);
    gameSetup.game.teamCollection.GetTeamList()[0].CalculateLives(gameSetup.differentLettersInWord);
    return gameSetup.game.teamCollection.GetTeamList()[0].Lives;
}).WithName("GetLives");

app.MapGet("/Goal", ([FromUri] string word) =>
{
    int goal = gameSetup.game.teamCollection.GetTeamList()[0].GuessCollection.CalculateGoal(gameSetup.differentLettersInWord);
    return goal;
}).WithName("GetGoal");

app.MapGet("/GuessLine", ([FromUri] string word) =>
{
    gameSetup.game.SetWord(word);
    string guessline = gameSetup.game._wordClass.CalculateWordStripes();
    return guessline.Length / 2;
}).WithName("GuessLine");

//app.MapGet("/GuessLetter", ([FromUri] string letter) =>
//{
//    gameSetup.game.LetterClickHandler(letter);
//    return "notimplementedexception";
//}).WithName("GetGoal");

app.Run();


public class GameSetup
{
    // Properties
    public List<string> differentLettersInWord = new List<string>();
    public Game game;
    Team _team1 = new Team("team1", new GuessCollection());
    Team _team2 = new Team("team2", new GuessCollection());
    TeamCollection _teamCollection = new TeamCollection();

    public GameSetup()
    {
        _teamCollection.AddTeam(new Team("team1", new GuessCollection()));
        _teamCollection.AddTeam(new Team("team2", new GuessCollection()));
        game = new Game(_teamCollection);
    }
}

// Class
public class Game
{
    // Fields
    public TeamCollection teamCollection;
    public Team guesser;
    public Team wordMaster;
    List<string> _differentLettersInWord = new List<string>();
    public WordClass _wordClass;
    AlphabetClass _alphabetClass;

    // Methods
    public Game(TeamCollection importTeamCollection)
    {
        this.teamCollection = importTeamCollection;
    }

    public void SetWord(string word)
    {
        _wordClass = new WordClass(word);
    }

    public void Guess(string myLetter)
    {
        new Guess(myLetter, guesser, _wordClass);
        CheckIfDead(guesser.Lives);
        guesser.EndOfGuess();
        CheckIfRoundIsOver();
    }
    void StartNextRound()
    {
        ResetAllValues();
        SwitchTurns();
    }
    void ResetAllValues()
    {
        guesser.ResetAllValues();
        _differentLettersInWord.Clear();
    }
    void CheckIfRoundIsOver()
    {
        if (guesser.EndRound)
        {
            guesser.EndRound = false;
            StartNextRound();
        }
    }
    void SwitchTurns()
    {
        if (teamCollection.GetTeamList()[0].Role == Roles.Wordmaster)
        {
            teamCollection.GetTeamList()[0].Role = Roles.Guesser;
            teamCollection.GetTeamList()[1].Role = Roles.Wordmaster;
        }
        else if (teamCollection.GetTeamList()[0].Role == Roles.Guesser)
        {
            teamCollection.GetTeamList()[0].Role = Roles.Wordmaster;
            teamCollection.GetTeamList()[1].Role = Roles.Guesser;
        }
    }
    void CheckIfDead(int lives)
    {
        DisplayLives();
        if (lives == 0)
        {
            //MessageBox.Show("No lives left"); frontend
        }
    } // not done
    void SetGuesserAndWordMaster()
    {
        if (teamCollection.GetTeamList()[0].Role == Roles.Guesser)
        {
            guesser = teamCollection.GetTeamList()[0];
            wordMaster = teamCollection.GetTeamList()[1];
        }
        else
        {
            wordMaster = teamCollection.GetTeamList()[0];
            guesser = teamCollection.GetTeamList()[1];
        }
    }


    // Methods - Display
    void DisplayLives()
    {
        //LBL_Lives.Text = "";
        for (int i = 0; i < guesser.Lives; i++)
        {
            //LBL_Lives.Text += "♥"; frontend
        }
    }
    void DisplayGoal()
    {
        int goal = guesser.GuessCollection.CalculateGoal(_differentLettersInWord);
        //LBL_Goal.Text = "Goal < " + goal.ToString(); frontend
    }

    // Events
    private void BTN_Submit_Click(object sender, EventArgs e)
    {
        string word = "placeholder"; /*TB_Word.Text.ToUpper();*/
        _wordClass = new WordClass(word, guesser.GuessCollection);

        _differentLettersInWord = WordClass.CountDifferentLetters(_wordClass.Word);
        //LB_Test.Items.Clear();
        foreach (var letter in _differentLettersInWord)
        {
            //LB_Test.Items.Add(letter);
        }

        if (_differentLettersInWord.Count > 3)
        {
            guesser.CalculateLives(_differentLettersInWord);
            DisplayGoal();
            DisplayLives();
            guesser.GuessCollection.SetWord(_wordClass);
            //GB_CreateWord.Enabled = false;
            //GB_Guess.Enabled = true;
        }
        else
        {
            //MessageBox.Show("Woord moet meer dan 3 verschillende letters hebben");
        }
    }

    public void LetterClickHandler(string letter)
    {
        letter = letter.ToUpper();
        if (letter == "A")
        {
            Guess(letter);
        }
        else if (letter == "B")
        {
            Guess(letter);
        }
        else if (letter == "C")
        {
            Guess(letter);
        }
        else if (letter == "D")
        {
            Guess(letter);
        }
        else if (letter == "E")
        {
            Guess(letter);
        }
        else if (letter == "F")
        {
            Guess(letter);
        }
        else if (letter == "G")
        {
            Guess(letter);
        }
        else if (letter == "H")
        {
            Guess(letter);
        }
        else if (letter == "I")
        {
            Guess(letter);
        }
        else if (letter == "J")
        {
            Guess(letter);
        }
        else if (letter == "K")
        {
            Guess(letter);
        }
        else if (letter == "L")
        {
            Guess(letter);
        }
        else if (letter == "M")
        {
            Guess(letter);
        }
        else if (letter == "N")
        {
            Guess(letter);
        }
        else if (letter == "O")
        {
            Guess(letter);
        }
        else if (letter == "P")
        {
            Guess(letter);
        }
        else if (letter == "Q")
        {
            Guess(letter);
        }
        else if (letter == "R")
        {
            Guess(letter);
        }
        else if (letter == "S")
        {
            Guess(letter);
        }
        else if (letter == "T")
        {
            Guess(letter);
        }
        else if (letter == "U")
        {
            Guess(letter);
        }
        else if (letter == "V")
        {
            Guess(letter);
        }
        else if (letter == "W")
        {
            Guess(letter);
        }
        else if (letter == "X")
        {
            Guess(letter);
        }
        else if (letter == "Y")
        {
            Guess(letter);
        }
        else if (letter == "Z")
        {
            Guess(letter);
        }
        else
        {
            Guess("jdsngksvjeirunjdflzjbnjsiec-=-24359");
        }
    }

    // Events - Letter buttons
    private void BTN_A_Click()
    {
        string myLetter = "A";
        Guess(myLetter);
    }
    private void BTN_B_Click(object sender, EventArgs e)
    {
        //BTN_B.Enabled = false;
        string myLetter = "B";
        Guess(myLetter);
    }
    private void BTN_C_Click(object sender, EventArgs e)
    {
        //BTN_C.Enabled = false;
        string myLetter = "C";
        Guess(myLetter);
    }
    private void BTN_D_Click(object sender, EventArgs e)
    {
        //BTN_D.Enabled = false;
        string myLetter = "D";
        Guess(myLetter);
    }
    private void BTN_E_Click(object sender, EventArgs e)
    {
        //BTN_E.Enabled = false;
        string myLetter = "E";
        Guess(myLetter);
    }
    private void BTN_F_Click(object sender, EventArgs e)
    {
        //BTN_F.Enabled = false;
        string myLetter = "F";
        Guess(myLetter);
    }
    private void BTN_G_Click(object sender, EventArgs e)
    {
        //BTN_G.Enabled = false;
        string myLetter = "G";
        Guess(myLetter);
    }
    private void BTN_H_Click(object sender, EventArgs e)
    {
        //BTN_H.Enabled = false;
        string myLetter = "H";
        Guess(myLetter);
    }
    private void BTN_I_Click(object sender, EventArgs e)
    {
        //BTN_I.Enabled = false;
        string myLetter = "I";
        Guess(myLetter);
    }
    private void BTN_J_Click(object sender, EventArgs e)
    {
        //BTN_J.Enabled = false;
        string myLetter = "J";
        Guess(myLetter);
    }
    private void BTN_K_Click(object sender, EventArgs e)
    {
        //BTN_K.Enabled = false;
        string myLetter = "K";
        Guess(myLetter);
    }
    private void BTN_L_Click(object sender, EventArgs e)
    {
        //BTN_L.Enabled = false;
        string myLetter = "L";
        Guess(myLetter);
    }
    private void BTN_M_Click(object sender, EventArgs e)
    {
        //BTN_M.Enabled = false;
        string myLetter = "M";
        Guess(myLetter);
    }
    private void BTN_N_Click(object sender, EventArgs e)
    {
        //BTN_N.Enabled = false;
        string myLetter = "N";
        Guess(myLetter);
    }
    private void BTN_O_Click(object sender, EventArgs e)
    {
        //BTN_O.Enabled = false;
        string myLetter = "O";
        Guess(myLetter);
    }
    private void BTN_P_Click(object sender, EventArgs e)
    {
        //BTN_P.Enabled = false;
        string myLetter = "P";
        Guess(myLetter);
    }
    private void BTN_Q_Click(object sender, EventArgs e)
    {
        //BTN_Q.Enabled = false;
        string myLetter = "Q";
        Guess(myLetter);
    }
    private void BTN_R_Click(object sender, EventArgs e)
    {
        //BTN_R.Enabled = false;
        string myLetter = "R";
        Guess(myLetter);
    }
    private void BTN_S_Click(object sender, EventArgs e)
    {
        //BTN_S.Enabled = false;
        string myLetter = "S";
        Guess(myLetter);
    }
    private void BTN_T_Click(object sender, EventArgs e)
    {
        //BTN_T.Enabled = false;
        string myLetter = "T";
        Guess(myLetter);
    }
    private void BTN_U_Click(object sender, EventArgs e)
    {
        //BTN_U.Enabled = false;
        string myLetter = "U";
        Guess(myLetter);
    }
    private void BTN_V_Click(object sender, EventArgs e)
    {
        //BTN_V.Enabled = false;
        string myLetter = "V";
        Guess(myLetter);
    }
    private void BTN_W_Click(object sender, EventArgs e)
    {
        //BTN_W.Enabled = false;
        string myLetter = "W";
        Guess(myLetter);
    }
    private void BTN_X_Click(object sender, EventArgs e)
    {
        //BTN_X.Enabled = false;
        string myLetter = "X";
        Guess(myLetter);
    }
    private void BTN_Y_Click(object sender, EventArgs e)
    {
        //BTN_Y.Enabled = false;
        string myLetter = "Y";
        Guess(myLetter);
    }
    private void BTN_Z_Click(object sender, EventArgs e)
    {
        //BTN_Z.Enabled = false;
        string myLetter = "Z";
        Guess(myLetter);
    }
}

