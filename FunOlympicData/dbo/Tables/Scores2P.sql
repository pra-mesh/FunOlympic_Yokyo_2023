CREATE TABLE [dbo].[Scores2P]
(
	[Id] INT NOT NULL PRIMARY KEY  IDENTITY (1, 1), 
	GameDate Datetime Not null default GetDate(),
	Team1 nvarchar(10) not null,
	team1Score int not null default 0,
	team2 nvarchar(10) not null,
	team2Score int not null default 0,
	GameID int Foreign Key References Gamelist(Id) NOT NULL, 
    [Description] NVARCHAR(50) NULL

)
