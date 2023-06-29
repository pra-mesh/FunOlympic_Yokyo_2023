CREATE VIEW [dbo].[ActiveUser]
	AS 
    SELECT dbo.Users.UserName, dbo.Users.ID as UserID, dbo.RefreshToken.ID, dbo.RefreshToken.Token, dbo.RefreshToken.Expires, dbo.RefreshToken.Created, dbo.RefreshToken.CreatedByIp, dbo.RefreshToken.Revoked, 
                  dbo.RefreshToken.RevokedByIp, dbo.RefreshToken.ReplacedByToken, dbo.RefreshToken.ReasonRevoked
FROM     dbo.Users INNER JOIN
                  dbo.RefreshToken ON dbo.Users.ID = dbo.RefreshToken.UserID
WHERE  (dbo.RefreshToken.Revoked IS NULL) AND (dbo.RefreshToken.Expires >= GETUTCDATE())