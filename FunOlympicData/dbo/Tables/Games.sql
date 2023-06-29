﻿CREATE TABLE [dbo].[Games]
(
	[Id] INT NOT NULL PRIMARY KEY,
	GameDate Datetime Not null default GetDate(),
	Team1 nvarchar(10) not null,
	team1Score int not null default 0,
	team2 nvarchar(10) not null,
	team2Score int not null default 0,
	GameID int Foreign Key References Gamelist(Id) NOT NULL,
	LinktoGame Nvarchar(250) not null,
	[Livestatus] bit default 0 not null
)
