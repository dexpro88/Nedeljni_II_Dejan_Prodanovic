--we create database  
CREATE DATABASE ClinicDB;

GO

use ClinicDB;

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
	CreationDate varchar(50),
	InstitutionOwner varchar(50),
    InstitutionAddress varchar(50),
	NumberOdFloors int,
	NumberOfRoomsPerFloor int,
	HasTerrace bit,
	HasYard bit,
	NumberOfAccessPointsForAmbulance int,
	NumberOfAccessPointsForInvalids int
 
);
