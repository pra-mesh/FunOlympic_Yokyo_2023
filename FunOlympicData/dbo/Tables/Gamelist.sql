CREATE TABLE [dbo].[Gamelist]
(
	[Id] INT NOT NULL PRIMARY KEY  IDENTITY (1, 1),
	GameName NVarchar(100) NOT NULL,
	GameType NVarchar(10) Not Null DEFAULT 'Out-door'
)
