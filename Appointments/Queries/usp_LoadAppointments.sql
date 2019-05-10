USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_LoadAppointments')
	DROP procedure usp_LoadAppointments
GO
CREATE procedure usp_LoadAppointments

WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF

	SELECT * 
			FROM Appointments
			WHERE
				AppointmentStatus = 1 ORDER BY DateStart ASC

	--SELECT SCOPE_IDENTITY()
	
IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO