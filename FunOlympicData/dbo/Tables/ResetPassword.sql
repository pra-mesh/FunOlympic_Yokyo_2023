CREATE TABLE [dbo].[ResetPassword]
(
	[Id] INT IDENTITY (1, 1)  NOT NULL PRIMARY KEY,
	[EmailID] nvarchar(256)  Foreign Key References Users(Email) not null,
	[ResetCode] nvarchar(6) NOT NULL,
	[Expireson] Datetime NOT NULL DEFAULT GetDate()
)
