USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_UpdateAppointment')
	DROP procedure usp_UpdateAppointment
GO
CREATE procedure usp_UpdateAppointment
(
	@id VARCHAR(30),
	@type VARCHAR(30),
	@dateStart DATE,
	@dateEnd DATE,
	@timeStart TIME,
	@timeEnd TIME,
	@note VARCHAR(MAX),
	@location VARCHAR(MAX),
	@name VARCHAR(100)
)
WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF

	UPDATE Appointments
			SET
				AppType = @type,
				DateStart = @dateStart,
				DateEnd = @dateEnd,
				TimeStart = @timeStart,
				TimeEnd = @timeEnd,
				AppNote = @note,
				AppLocation = @location,
				Appointee = @name
			WHERE
				AppointmentID = @id

	--SELECT SCOPE_IDENTITY()
	
IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO