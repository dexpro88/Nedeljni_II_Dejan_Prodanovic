--we create database  
CREATE DATABASE MyClinicDB;

GO

use MyClinicDB;

GO

 --we delete tables in case they exist
DROP TABLE IF EXISTS tblUser;
DROP TABLE IF EXISTS tblClinicMaintenace;
DROP TABLE IF EXISTS tblClinicManager;
DROP TABLE IF EXISTS tblClinicDoctor;
DROP TABLE IF EXISTS tblClinicPatient;
DROP TABLE IF EXISTS tblClinicAdmin;
DROP TABLE IF EXISTS tblClinicInstitution;
 




--we create table tblUser
 CREATE TABLE tblUser (
    UserID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FullName varchar(50),
	IDCardNumber varchar(50),
	Gender varchar(50),
	DateOfBirth date,
    Nationality varchar(50),
	Username varchar(50),
	Password varchar(50)
 
);
 

 --we create table tblManager
 CREATE TABLE tblClinicManager (
    ManagerID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    MaxNumberOfDoctors int,
	MinNumberOdRooms int,
	NumberOfOmissions int,
    ManagerFloor varchar(50),
 	UserID int FOREIGN KEY REFERENCES tblUser(UserID)  

);

--we create table tblClinicDoctor
 CREATE TABLE tblClinicDoctor (
    DoctorID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    UniqueNumber varchar(50),
	AccountNumber varchar(50),
	Sector varchar(50),
	DoctorShift varchar(50),
	RecievesPatients bit,
	ManagerID int FOREIGN KEY REFERENCES tblClinicManager(ManagerID),  
 	UserID int FOREIGN KEY REFERENCES tblUser(UserID)  

);
  
  --we create table tblClinicPatient
 CREATE TABLE tblClinicPatient (
    PatientID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    HealthCardNumber varchar(50),
	HealthAssuranceExpiryDate date,
	DoctorUniqueNumber varchar(50), 
	DoctorId int FOREIGN KEY REFERENCES tblClinicDoctor(DoctorId),
 	UserID int FOREIGN KEY REFERENCES tblUser(UserID)  

);
  

--we create table tblClinicMaintenace
 CREATE TABLE tblClinicMaintenace (
    ClinicMaintenaceID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    CanChooseInvalidAccess bit,
	CanChooseClinicExpansionPermission bit,
 	UserID int FOREIGN KEY REFERENCES tblUser(UserID)  

);

--we create table tblClinicAdmin
 CREATE TABLE tblClinicAdmin (
    AdminID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    HasCreatedClinic bit,
	UserID int FOREIGN KEY REFERENCES tblUser(UserID)  

);


--we create table tblClinicInstitution
 CREATE TABLE tblClinicInstitution (
    InstitutionID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	InstitutionName varchar(50),
	CreationDate date,
	InstitutionOwner varchar(50),
    InstitutionAddress varchar(50),
	NumberOdFloors int,
	NumberOfRoomsPerFloor int,
	HasTerrace bit,
	HasYard bit,
	NumberOfAccessPointsForAmbulance int,
	NumberOfAccessPointsForInvalids int
 
);

GO
CREATE VIEW vwClinicDoctor 
AS

SELECT   dbo.tblClinicDoctor.UniqueNumber ,dbo.tblClinicDoctor.AccountNumber,
         dbo.tblClinicDoctor.DoctorShift ,dbo.tblClinicDoctor.RecievesPatients,
		 dbo.tblClinicDoctor.Sector ,dbo.tblClinicDoctor.ManagerID ,
         dbo.tblClinicDoctor.UserID, dbo.tblClinicDoctor.DoctorID,  
		 dbo.tblUser.IDCardNumber,dbo.tblUser.FullName,
         dbo.tblUser.Gender, dbo.tblUser.DateOfBirth,dbo.tblUser.Nationality,
		 dbo.tblUser.Username 
FROM            dbo.tblUser INNER JOIN
            dbo.tblClinicDoctor ON dbo.tblClinicDoctor.UserID = dbo.tblUser.UserID  
			 
           
GO


GO
CREATE VIEW vwClinicPatient
AS

SELECT   dbo.tblClinicPatient.PatientID ,dbo.tblClinicPatient.HealthCardNumber,
         dbo.tblClinicPatient.HealthAssuranceExpiryDate ,dbo.tblClinicPatient.DoctorUniqueNumber,
		 dbo.tblClinicPatient.DoctorId ,dbo.tblClinicPatient.UserID ,
		 dbo.tblUser.IDCardNumber,dbo.tblUser.FullName,
         dbo.tblUser.Gender, dbo.tblUser.DateOfBirth,dbo.tblUser.Nationality,
		 dbo.tblUser.Username 
FROM            dbo.tblUser INNER JOIN
            dbo.tblClinicPatient ON dbo.tblClinicPatient.UserID = dbo.tblUser.UserID  
			 
           
GO

GO
CREATE VIEW vwClinicManager
AS

SELECT   dbo.tblClinicManager.ManagerID ,dbo.tblClinicManager.MaxNumberOfDoctors,
         dbo.tblClinicManager.MinNumberOdRooms ,dbo.tblClinicManager.NumberOfOmissions,
		 dbo.tblClinicManager.ManagerFloor ,dbo.tblClinicManager.UserID ,
		 dbo.tblUser.IDCardNumber,dbo.tblUser.FullName,
         dbo.tblUser.Gender, dbo.tblUser.DateOfBirth,dbo.tblUser.Nationality,
		 dbo.tblUser.Username 
FROM            dbo.tblUser INNER JOIN
            dbo.tblClinicManager ON dbo.tblClinicManager.UserID = dbo.tblUser.UserID  
			 
           
GO

GO
CREATE VIEW vwClinicMaintenace
AS

SELECT   dbo.tblClinicMaintenace.ClinicMaintenaceID ,dbo.tblClinicMaintenace.CanChooseInvalidAccess,
         dbo.tblClinicMaintenace.CanChooseClinicExpansionPermission,
		 dbo.tblClinicMaintenace.UserID ,
		 dbo.tblUser.IDCardNumber,dbo.tblUser.FullName,
         dbo.tblUser.Gender, dbo.tblUser.DateOfBirth,dbo.tblUser.Nationality,
		 dbo.tblUser.Username 
FROM            dbo.tblUser INNER JOIN
            dbo.tblClinicMaintenace ON dbo.tblClinicMaintenace.UserID = dbo.tblUser.UserID  
			 
           
GO
