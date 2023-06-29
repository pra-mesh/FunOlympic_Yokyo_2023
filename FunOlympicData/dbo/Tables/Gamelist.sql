CREATE TABLE [dbo].[Gamelist]
(
	[Id] INT NOT NULL PRIMARY KEY,
	GameName NVarchar(100) NOT NULL,
	GameType NVarchar(10) Not Null DEFAULT 'Out-door'
)
