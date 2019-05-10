use mockupAppoinments
go

CREATE TABLE [dbo].[Appointments]
(
	[AppointmentID] INT IDENTITY (1,1) NOT NULL,
	[AppType] VARCHAR (30) NOT NULL,
	[DateStart] DATE NOT NULL,
	[DateEnd] DATE NOT NULL,
	[TimeStart] TIME NOT NULL,
	[TimeEnd] TIME NOT NUll,
	[AppNote] VARCHAR(MAX),
	[Appointee] VARCHAR(100) NOT NULL,
	CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED (AppointmentID ASC) 
)

CREATE TABLE [dbo].[Patients]
(
	[PatientID] INT IDENTITY(1,1) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[MiddleName] NVARCHAR(50),
	[Gender] NVARCHAR(30) NOT NULL,
	[BirthDate] DATE NOT NULL,
	[Address] NVARCHAR(MAX),
	[ContactNumber] NVARCHAR(30),
	[EmailAddress] NVARCHAR(50)
	CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED (PatientID ASC)
)
