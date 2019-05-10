USE mockupAppoinments
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_SearchPatients')
	DROP procedure usp_SearchPatients
GO
CREATE procedure usp_SearchPatients
(
	@keyWord VARCHAR(100)
)
WITH ENCRYPTION
AS
BEGIN TRANSACTION
SET NOCOUNT OFF

	SET NOCOUNT OFF
	Select *
	FROM Patients 
	where (FirstName like '%' + @keyWord + '%' OR LastName like '%' + @keyWord + '%' OR PatientID = @keyWord) 
			OR (CONCAT (FirstName, ' ' , LastName ) Like '%' + @keyWord + '%') 
			OR (CONCAT (LastName, ', ' , FirstName ) Like '%' + @keyWord + '%')
			OR (CONCAT (LastName, ',' , FirstName ) Like '%' + @keyWord + '%') 
			--OR (PatientID = @keyword ) patientId is INT, could not convert varchar to int ERROR
	--SELECT SCOPE_IDENTITY()

IF @@ERROR <> 0 
	BEGIN
		ROLLBACK TRANSACTION
		RAISERROR ('Error',16,1)
		END
	COMMIT TRANSACTION
GO