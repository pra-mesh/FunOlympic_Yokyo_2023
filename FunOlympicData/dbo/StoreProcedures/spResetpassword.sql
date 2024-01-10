CREATE PROCEDURE [dbo].[spResetpassword](
	@userName nvarchar(256),
	@password nvarchar(256)
	)
AS
	update dbo.[Users] set PasswordHash= HASHBYTES('SHA2_512',@password) Where Email=
		   @userName
RETURN 0
