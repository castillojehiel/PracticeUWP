USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_DeleteAppointment')
	DROP procedure usp_DeleteAppointment
GO
CREATE procedure usp_DeleteAppointment
(
	@appID VARCHAR(50)
)
WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF

	DELETE FROM Appointments WHERE AppointmentID = @appID
	--SELECT SCOPE_IDENTITY()

IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO