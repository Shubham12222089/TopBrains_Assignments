using System;
using System.Collections.Generic;
using System.Linq;

// Team class with IComparable
public class Team : IComparable<Team>
{
    public string Name { get; set; }
    public int Points { get; set; }
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }

    public int GoalDifference => GoalsFor - GoalsAgainst;

    public int CompareTo(Team other)
    {
        // Compare by points descending
        int pointsCompare = other.Points.CompareTo(Points);
        if (pointsCompare != 0)
        {
            return pointsCompare;
        }

        // Then by goal difference descending
        int goalDiffCompare = other.GoalDifference.CompareTo(GoalDifference);
        if (goalDiffCompare != 0)
        {
            return goalDiffCompare;
        }

        // Then by name ascending
        return Name.CompareTo(other.Name);
    }

    public override string ToString()
    {
        return $"{Name} - Points: {Points}, W: {Wins}, D: {Draws}, L: {Losses}, GD: {GoalDifference}";
    }

    public Team Clone()
    {
        return new Team
        {
            Name = Name,
            Points = Points,
            GamesPlayed = GamesPlayed,
            Wins = Wins,
            Draws = Draws,
            Losses = Losses,
            GoalsFor = GoalsFor,
            GoalsAgainst = GoalsAgainst
        };
    }
}

// Match class
public class Match
{
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
    public DateTime MatchDate { get; set; }
    public bool IsPlayed { get; set; }

    // Store original state for undo
    public int Team1OriginalPoints { get; set; }
    public int Team2OriginalPoints { get; set; }
    public int Team1OriginalWins { get; set; }
    public int Team2OriginalWins { get; set; }
    public int Team1OriginalDraws { get; set; }
    public int Team2OriginalDraws { get; set; }
    public int Team1OriginalLosses { get; set; }
    public int Team2OriginalLosses { get; set; }
    public int Team1OriginalGoalsFor { get; set; }
    public int Team2OriginalGoalsFor { get; set; }
    public int Team1OriginalGoalsAgainst { get; set; }
    public int Team2OriginalGoalsAgainst { get; set; }
    public int Team1OriginalGamesPlayed { get; set; }
    public int Team2OriginalGamesPlayed { get; set; }

    public Match(Team team1, Team team2)
    {
        Team1 = team1;
        Team2 = team2;
        MatchDate = DateTime.Now;
        IsPlayed = false;
    }

    public void SaveOriginalState()
    {
        Team1OriginalPoints = Team1.Points;
        Team2OriginalPoints = Team2.Points;
        Team1OriginalWins = Team1.Wins;
        Team2OriginalWins = Team2.Wins;
        Team1OriginalDraws = Team1.Draws;
        Team2OriginalDraws = Team2.Draws;
        Team1OriginalLosses = Team1.Losses;
        Team2OriginalLosses = Team2.Losses;
        Team1OriginalGoalsFor = Team1.GoalsFor;
        Team2OriginalGoalsFor = Team2.GoalsFor;
        Team1OriginalGoalsAgainst = Team1.GoalsAgainst;
        Team2OriginalGoalsAgainst = Team2.GoalsAgainst;
        Team1OriginalGamesPlayed = Team1.GamesPlayed;
        Team2OriginalGamesPlayed = Team2.GamesPlayed;
    }

    public void RestoreOriginalState()
    {
        Team1.Points = Team1OriginalPoints;
        Team2.Points = Team2OriginalPoints;
        Team1.Wins = Team1OriginalWins;
        Team2.Wins = Team2OriginalWins;
        Team1.Draws = Team1OriginalDraws;
        Team2.Draws = Team2OriginalDraws;
        Team1.Losses = Team1OriginalLosses;
        Team2.Losses = Team2OriginalLosses;
        Team1.GoalsFor = Team1OriginalGoalsFor;
        Team2.GoalsFor = Team2OriginalGoalsFor;
        Team1.GoalsAgainst = Team1OriginalGoalsAgainst;
        Team2.GoalsAgainst = Team2OriginalGoalsAgainst;
        Team1.GamesPlayed = Team1OriginalGamesPlayed;
        Team2.GamesPlayed = Team2OriginalGamesPlayed;
    }

    public override string ToString()
    {
        if (IsPlayed)
        {
            return $"{Team1.Name} {Team1Score} - {Team2Score} {Team2.Name}";
        }
        return $"{Team1.Name} vs {Team2.Name} (Scheduled)";
    }
}

// Tournament class
public class Tournament
{
    private List<Team> teams = new List<Team>();
    private LinkedList<Match> schedule = new LinkedList<Match>();
    private Stack<Match> undoStack = new Stack<Match>();
    private List<Match> playedMatches = new List<Match>();

    // Add team
    public void AddTeam(Team team)
    {
        if (!teams.Any(t => t.Name == team.Name))
        {
            teams.Add(team);
        }
    }

    // Add match to schedule
    public void ScheduleMatch(Match match)
    {
        schedule.AddLast(match);
    }

    // Record match result and update rankings
    public void RecordMatchResult(Match match, int team1Score, int team2Score)
    {
        // Save original state for undo
        match.SaveOriginalState();
        undoStack.Push(match);

        // Set scores
        match.Team1Score = team1Score;
        match.Team2Score = team2Score;
        match.IsPlayed = true;

        // Update team statistics
        match.Team1.GamesPlayed++;
        match.Team2.GamesPlayed++;
        match.Team1.GoalsFor += team1Score;
        match.Team1.GoalsAgainst += team2Score;
        match.Team2.GoalsFor += team2Score;
        match.Team2.GoalsAgainst += team1Score;

        // Update points based on result
        if (team1Score > team2Score)
        {
            // Team 1 wins
            match.Team1.Points += 3;
            match.Team1.Wins++;
            match.Team2.Losses++;
        }
        else if (team2Score > team1Score)
        {
            // Team 2 wins
            match.Team2.Points += 3;
            match.Team2.Wins++;
            match.Team1.Losses++;
        }
        else
        {
            // Draw
            match.Team1.Points += 1;
            match.Team2.Points += 1;
            match.Team1.Draws++;
            match.Team2.Draws++;
        }

        playedMatches.Add(match);
    }

    // Undo last match
    public bool UndoLastMatch()
    {
        if (undoStack.Count == 0)
        {
            return false;
        }

        Match match = undoStack.Pop();
        match.RestoreOriginalState();
        match.IsPlayed = false;
        match.Team1Score = 0;
        match.Team2Score = 0;
        playedMatches.Remove(match);

        return true;
    }

    // Get rankings sorted by points
    public List<Team> GetRankings()
    {
        return teams.OrderBy(t => t).ToList();
    }

    // Get team ranking position
    public int GetTeamRanking(Team team)
    {
        List<Team> rankings = GetRankings();
        for (int i = 0; i < rankings.Count; i++)
        {
            if (rankings[i].Name == team.Name)
            {
                return i + 1;
            }
        }
        return -1;
    }

    // Get scheduled matches
    public LinkedList<Match> GetSchedule()
    {
        return schedule;
    }

    // Get played matches
    public List<Match> GetPlayedMatches()
    {
        return playedMatches;
    }

    // Get upcoming matches
    public List<Match> GetUpcomingMatches()
    {
        return schedule.Where(m => !m.IsPlayed).ToList();
    }

    // Get team matches
    public List<Match> GetTeamMatches(Team team)
    {
        return playedMatches.Where(m => m.Team1.Name == team.Name || m.Team2.Name == team.Name).ToList();
    }

    // Display standings
    public void DisplayStandings()
    {
        Console.WriteLine("\n=== Tournament Standings ===");
        Console.WriteLine($"{"Pos",-4}{"Team",-20}{"P",-4}{"W",-4}{"D",-4}{"L",-4}{"GF",-4}{"GA",-4}{"GD",-5}{"Pts",-4}");
        Console.WriteLine(new string('-', 55));

        List<Team> rankings = GetRankings();
        for (int i = 0; i < rankings.Count; i++)
        {
            Team t = rankings[i];
            Console.WriteLine($"{i + 1,-4}{t.Name,-20}{t.GamesPlayed,-4}{t.Wins,-4}{t.Draws,-4}{t.Losses,-4}{t.GoalsFor,-4}{t.GoalsAgainst,-4}{t.GoalDifference,-5}{t.Points,-4}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Tournament Ranking System ===\n");

        Tournament tournament = new Tournament();

        // Create teams
        Team teamA = new Team { Name = "Team Alpha" };
        Team teamB = new Team { Name = "Team Beta" };
        Team teamC = new Team { Name = "Team Charlie" };
        Team teamD = new Team { Name = "Team Delta" };

        // Add teams to tournament
        tournament.AddTeam(teamA);
        tournament.AddTeam(teamB);
        tournament.AddTeam(teamC);
        tournament.AddTeam(teamD);

        // Schedule matches
        Console.WriteLine("--- Scheduling Matches ---");
        Match match1 = new Match(teamA, teamB);
        Match match2 = new Match(teamC, teamD);
        Match match3 = new Match(teamA, teamC);
        Match match4 = new Match(teamB, teamD);
        Match match5 = new Match(teamA, teamD);
        Match match6 = new Match(teamB, teamC);

        tournament.ScheduleMatch(match1);
        tournament.ScheduleMatch(match2);
        tournament.ScheduleMatch(match3);
        tournament.ScheduleMatch(match4);
        tournament.ScheduleMatch(match5);
        tournament.ScheduleMatch(match6);

        Console.WriteLine("Scheduled 6 matches");

        // Display initial standings
        tournament.DisplayStandings();

        // Record match results
        Console.WriteLine("\n--- Recording Match Results ---");

        Console.WriteLine($"Match 1: {teamA.Name} vs {teamB.Name}");
        tournament.RecordMatchResult(match1, 3, 1);
        Console.WriteLine($"Result: {match1}");

        Console.WriteLine($"Match 2: {teamC.Name} vs {teamD.Name}");
        tournament.RecordMatchResult(match2, 2, 2);
        Console.WriteLine($"Result: {match2}");

        Console.WriteLine($"Match 3: {teamA.Name} vs {teamC.Name}");
        tournament.RecordMatchResult(match3, 1, 0);
        Console.WriteLine($"Result: {match3}");

        // Display standings after 3 matches
        tournament.DisplayStandings();

        // Check rankings
        Console.WriteLine("\n--- Team Rankings ---");
        List<Team> rankings = tournament.GetRankings();
        Console.WriteLine($"1st Place: {rankings[0].Name}");
        Console.WriteLine($"Team Alpha's ranking: {tournament.GetTeamRanking(teamA)}");

        // Test undo functionality
        Console.WriteLine("\n--- Testing Undo ---");
        Console.WriteLine($"Team Alpha points before undo: {teamA.Points}");

        tournament.UndoLastMatch();
        Console.WriteLine("Undid last match (Alpha vs Charlie)");
        Console.WriteLine($"Team Alpha points after undo: {teamA.Points}");

        // Display standings after undo
        tournament.DisplayStandings();

        // Record more matches
        Console.WriteLine("\n--- Recording More Matches ---");
        tournament.RecordMatchResult(match3, 2, 1);
        Console.WriteLine($"Match 3 (replayed): {match3}");

        tournament.RecordMatchResult(match4, 0, 3);
        Console.WriteLine($"Match 4: {match4}");

        tournament.RecordMatchResult(match5, 1, 1);
        Console.WriteLine($"Match 5: {match5}");

        tournament.RecordMatchResult(match6, 2, 0);
        Console.WriteLine($"Match 6: {match6}");

        // Final standings
        Console.WriteLine("\n--- Final Tournament Standings ---");
        tournament.DisplayStandings();

        // Get upcoming matches
        Console.WriteLine("\n--- Upcoming Matches ---");
        List<Match> upcoming = tournament.GetUpcomingMatches();
        Console.WriteLine($"Upcoming matches: {upcoming.Count}");

        // Get Team Alpha's matches
        Console.WriteLine("\n--- Team Alpha's Matches ---");
        List<Match> alphaMatches = tournament.GetTeamMatches(teamA);
        foreach (Match m in alphaMatches)
        {
            Console.WriteLine($"  {m}");
        }
    }
}
