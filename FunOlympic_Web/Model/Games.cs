using Microsoft.AspNetCore.Http;
using System.IO;

namespace FunOlympic_Web.Model;
public class GamesInsert
{
    public string id { get; set; } = "";
    public DateTime GameDate { get; set; }
    public string Team1 { get; set; }
    public int team1Score { get; set; }
    public string team2 { get; set; }
    public int team2Score { get; set; }
    public int GameID { get; set; }
    public string LinktoGame { get; set; }
    public bool Livestatus { get; set; }
    public DateTime GameEndDate { get; set; }
    public string Description { get; set; } = "";
    public string photoloc { get; set; } = "";
    public int streamID { get; set; } = 0;

    public GamesInsert( string ID,DateTime GameDate_, string Team1_, int team1Score_, string team2_, int team2Score_,
        int GameID_, string LinktoGame_, bool Livestatus_, DateTime GameEndDate_, string Description_, string photoloc_, int StreamID)
    {
        this.streamID = StreamID;
        this.id = ID;
        this.GameDate = GameDate_;
        this.Team1 = Team1_;
        this.team1Score = team1Score_;
        this.team2 = team2_;
        this.team2Score = team2Score_;
        this.GameID = GameID_;
        this.LinktoGame = LinktoGame_;
        this.Livestatus = Livestatus_;
        this.GameEndDate = GameEndDate_;
        this.Description = Description_;
        this.photoloc = photoloc_;
    }
    public GamesInsert()
    {
        
    }
}

public class GameImg : GamesInsert
{
    public IFormFile img { get; set; }
    public IFormFile video { get; set; }
    public GameImg(int StreamID, string ID , IFormFile Video, IFormFile Img, DateTime GameDate_, string Team1_, int team1Score_, string team2_, int team2Score_, int GameID_, string LinktoGame_, bool Livestatus_, 
        DateTime GameEndDate_, string Description_, string PhotoLoc) : base
        (ID, GameDate_, Team1_, team1Score_, team2_, team2Score_, GameID_, LinktoGame_, Livestatus_, GameEndDate_, Description_, PhotoLoc, StreamID)
    {
        this.streamID = StreamID;
        this.img = Img;
        this.video = Video;
        this.id = ID;
        this.photoloc = PhotoLoc;
      
        this.GameDate = GameDate_;
        this.Team1 = Team1_;
        this.team1Score = team1Score_;
        this.team2 = team2_;
        this.team2Score = team2Score_;
        this.GameID = GameID_;
        this.LinktoGame = LinktoGame_;
        this.Livestatus = Livestatus_;
        this.GameEndDate = GameEndDate_;
        this.Description = Description_;
    }
    public GameImg()
    {
    }
}
public class GameModel : GamesInsert
{
    public string channel { get; set; }

    public string gameName { get; set; }
    public GameModel(string Id_,string GameName,DateTime GameDate_, string Team1_, int team1Score_, string team2_,
        int team2Score_, int GameID_, string LinktoGame_, bool Livestatus_, DateTime GameEndDate_, string Description_,string photoLoc , string channel,int StreamID) :base
        (Id_,GameDate_,Team1_,team1Score_,team2_,team2Score_,GameID_,LinktoGame_,Livestatus_, GameEndDate_, Description_, photoLoc, StreamID)
    {
        this.streamID = StreamID;
        this.channel = channel;
        this.photoloc = photoLoc;
        this.id = Id_;
        this.gameName = GameName;
        this.GameDate = GameDate_;
        this.Team1 = Team1_;
        this.team1Score = team1Score_;
        this.team2 = team2_;
        this.team2Score = team2Score_;
        this.GameID = GameID_;
        this.LinktoGame = LinktoGame_;
        this.Livestatus = Livestatus_;
        this.GameEndDate = GameEndDate_;
        this.Description = Description_;
    }
    public GameModel()
    {
    }
}