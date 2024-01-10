CREATE VIEW [dbo].[AllScores]
	AS SELECT Scores2P.[Id],
	GameDate,
	Team1,
	team1Score,
	team2,
	team2Score ,
	GameName, GameID,[Description]  FROM Gamelist Right Join Scores2P on Gamelist.Id = Scores2P.GameID