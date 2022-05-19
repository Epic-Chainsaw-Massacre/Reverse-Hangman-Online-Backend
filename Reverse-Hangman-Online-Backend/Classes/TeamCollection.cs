namespace Reverse_Hangman_Online_Backend.Classes
{
    public class TeamCollection
    {
        // Fields
        List<Team> _teamList = new List<Team>();

        // Methods
        public void AddTeam(Team team)
        {
            _teamList.Add(team);
        }

        public List<Team> GetTeamList()
        {
            return _teamList;
        }
    }
}
