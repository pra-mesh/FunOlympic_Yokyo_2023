CREATE TABLE [dbo].[RefreshToken] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [Token]           NVARCHAR (255) NOT NULL,
    [Expires]         DATETIME       NOT NULL,
    [Created]         DATETIME       NOT NULL,
    [CreatedByIp]     NVARCHAR (50)  NOT NULL,
    [Revoked]         DATETIME       NULL,
    [RevokedByIp]     NVARCHAR (50)  NULL,
    [ReplacedByToken] NVARCHAR (255) NULL,
    [ReasonRevoked]   NVARCHAR (255) NULL,
    [UserID]          NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RefreshToken_CoopUsers] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([Id])
);