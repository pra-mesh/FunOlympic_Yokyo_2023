using System.ComponentModel.DataAnnotations;

namespace FunOlympicDataManager.Library.Models;
public class ScoresInsertModel
{
    [Required]
    
    public DateTime GameDate { get; set; }
    [Required]
    public string? Team1 { get; set; }
    [Required]
    public int team1Score { get; set; }
    public string? team2 { get; set; }
    public int team2Score { get; set; }
    [Required]
    public int GameID { get; set; }
    public string? Description { get; set; }

    public ScoresInsertModel( DateTime GameDate_, string Team1_, int team1Score_, string team2_, int team2Score_, int GameID_)
    {
       
        this.GameDate = GameDate_;
        this.Team1 = Team1_;
        this.team1Score = team1Score_;
        this.team2 = team2_;
        this.team2Score = team2Score_;
        this.GameID = GameID_;
    }
    public ScoresInsertModel()
    {
            
    }
}

public class ScoreModel
{
    public int Id { get; set; }
    public DateTime GameDate { get; set; }
    public string? Team1 { get; set; }
    public int team1Score { get; set; }
    public string? team2 { get; set; }
    public int team2Score { get; set; }
    public string? GameName { get; set; }
    public int GameID { get; set; }
    public string? Description { get; set; }

}
