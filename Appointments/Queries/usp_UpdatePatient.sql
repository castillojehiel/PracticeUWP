USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_UpdatePatient')
	DROP procedure usp_UpdatePatient
GO
CREATE procedure usp_UpdatePatient
(
	@patientID VARCHAR(30),
	@fname VARCHAR(50),
	@lname VARCHAR(50),
	@mname VARCHAR(50),
	@gender VARCHAR(30),
	@userAddress VARCHAR(MAX),
	@bday DATE,
	@email VARCHAR(50),
	@contactNum VARCHAR(50)
)
WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF

	UPDATE Patients
		SET
			FirstName = @fname,
			LastName = @lname,
			MiddleName = @mname,
			Gender = @gender,
			BirthDate = @bday,
			Address = @userAddress,
			ContactNumber = @contactNum,
			EmailAddress = @email
		WHERE
			PatientID = @patientID
	--SELECT SCOPE_IDENTITY()

IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO