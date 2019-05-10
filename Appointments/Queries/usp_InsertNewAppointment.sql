USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_InsertNewAppointment')
	DROP procedure usp_InsertNewAppointment
GO
CREATE procedure usp_InsertNewAppointment
(
	@type VARCHAR(50),
	@name VARCHAR(150),
	@dateStart DATE,
	@dateEnd DATE,
	@timeStart TIME,
	@timeEnd TIME,
	@note VARCHAR(MAX),
	@location VARCHAR(MAX)
)
WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF

	INSERT INTO Appointments(
				AppType,
				DateStart,
				DateEnd,
				TimeStart,
				TimeEnd,
				AppNote,
				Appointee,
				AppointmentStatus,
				AppLocation
		)VALUES(
				@type,
				@dateStart,
				@dateEnd,
				@timeStart,
				@timeEnd,
				@note,
				@name,
				1,
				@location
			)
	SELECT SCOPE_IDENTITY()
	
IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO