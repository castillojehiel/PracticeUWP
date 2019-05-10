USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_SaveNewPatient')
	DROP procedure usp_SaveNewPatient
GO
CREATE procedure usp_SaveNewPatient
(
	@lname VARCHAR(50),
	@mname VARCHAR(50),
	@fname VARCHAR(50),
	@gender VARCHAR(50),
	@bday DATE,
	@userAddress VARCHAR(MAX),
	@contactNum VARCHAR(50),
	@email VARCHAR(50)
)
WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF
	
	INSERT INTO Patients( 
			LastName,
			FirstName,
			MiddleName,
			Gender,
			BirthDate,
			Address,
			ContactNumber,
			EmailAddress
		)
		VALUES(
			@lname,
			@fname,
			@mname,
			@gender,
			@bday,
			@userAddress,
			@contactNum,
			@email
		)
	SELECT SCOPE_IDENTITY()
	
IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO