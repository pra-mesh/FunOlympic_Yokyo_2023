CREATE VIEW [dbo].[ALlGames]
	AS
SELECT dbo.Games.Id, dbo.Games.GameDate, dbo.Games.Team1, dbo.Games.team1Score, dbo.Games.team2, dbo.Games.team2Score, dbo.Games.GameID, dbo.Games.Livestatus, dbo.Games.LinktoGame, dbo.Games.GameEndDate, 
                  dbo.Games.Description, dbo.Gamelist.GameName, dbo.Games.photoloc,dbo.Games.StreamID, dbo.StreamingChannel.Channel
FROM     dbo.Games INNER JOIN
                  dbo.Gamelist ON dbo.Games.GameID = dbo.Gamelist.Id Left Join dbo.StreamingChannel on dbo.StreamingChannel.id = dbo.games.StreamID 
