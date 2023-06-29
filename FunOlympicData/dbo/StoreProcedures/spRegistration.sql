CREATE PROCEDURE [dbo].[spRegistration](
	@Id nvarchar(128),
	@Email nvarchar(256),
	@password nvarchar(256),
	@PhoneNumber nvarchar(256),
	@firstname  nvarchar(100),
	@lastName nvarchar(100),
	@Gender nvarchar(100),
	@DOB Date
	)
AS
	INSERT INTO [dbo].[Users]
           ([Id],
		   [Email],
		   [PasswordHash],
		   [PhoneNumber],
		   [FirstName],
		   [LastName],
		   [UserName],
		   [Gender],
		   [DOB])
		   Values 
		   (@Id,
		   @Email,
		   HASHBYTES('SHA2_512',@password),
		   @PhoneNumber,
		   @firstname,
		   @lastName,
		   @Email,
		   @Gender,
		   @DOB)
RETURN 0
