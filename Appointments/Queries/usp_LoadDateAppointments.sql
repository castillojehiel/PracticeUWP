USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_LoadDateAppointments')
	DROP procedure usp_LoadDateAppointments
GO
CREATE procedure usp_LoadDateAppointments
(
	@date DATE
)
WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF

	SELECT 
			app.AppointmentID,
			app.AppType,
			app.DateStart,
			app.DateEnd,
			app.TimeStart,
			app.TimeEnd,
			app.AppNote,
			app.AppLocation,
			pat.PatientID,
			case	
				when app.AppointmentStatus = 1 then
					'Active'
				else
					'Accomplished'
				end as[AppointmentStatus],
			case
				when AppType = 'consultation' then
					CONCAT(pat.FirstName,' ',pat.LastName) 
				else
					app.Appointee
				end as [name],
			case
				when AppType = 'consultation' then
					pat.PatientID
				else
					0
				end as [patientID]
		FROM	Appointments as app
				LEFT JOIN
				Patients as pat
					ON app.Appointee = CONVERT(VARCHAR, pat.PatientID)
		WHERE @date between DateStart and DateEnd

	--SELECT SCOPE_IDENTITY()
	
IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO