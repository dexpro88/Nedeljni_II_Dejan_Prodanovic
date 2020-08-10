--we create database  
CREATE DATABASE CompanyData;

GO

use CompanyData;

GO

 --we delete tables in case they exist
DROP TABLE IF EXISTS tblUser;
DROP TABLE IF EXISTS tblEmployee;
DROP TABLE IF EXISTS tblManager;
DROP TABLE IF EXISTS tblAdmin;
DROP TABLE IF EXISTS tblSector;
DROP TABLE IF EXISTS tblPosition;
DROP TABLE IF EXISTS tblProject;
DROP TABLE IF EXISTS tblRequestForChange;
DROP TABLE IF EXISTS tblDailyReport;
DROP TABLE IF EXISTS tblReport;




--we create table tblUser
 CREATE TABLE tblUser (
    UserID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(50),
	LastName varchar(50),
    JMBG varchar(50),
	Gender varchar(50),
	Residence varchar(50),
    MaritalStatus varchar(50),
	Username varchar(50),
	Password varchar(50)
 
);
--we create table tblSector 
 CREATE TABLE tblSector (
    SectorID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	SectorName varchar(50),
	SectorDescription varchar(200)
);

--we create table tblSector 
 CREATE TABLE tblPosition (
   PositionID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
   PositionName varchar(50),
   PositionDescription varchar(200)
 
 
);

 --we create table tblManager
 CREATE TABLE tblManager (
    ManagerID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Email varchar(50),
	ReservePassword varchar(50),
    ResponsibilityLevel varchar(50),
	NumberOfSuccesfullProjects int,
	Salary decimal,
	OfficeNumber varchar(40),  
	UserID int FOREIGN KEY REFERENCES tblUser(UserID)  

);
 --we create table tblEmployee
 CREATE TABLE tblEmployee (
    EmployeeID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    YearsOfService varchar(50),
	Salary decimal,
	ProfessionalQualifications varchar(50),
	SectorID int FOREIGN KEY REFERENCES tblSector(SectorID) ON DELETE CASCADE, 
	PositionID int FOREIGN KEY REFERENCES tblPosition(PositionID) ON DELETE CASCADE,   
	UserID int FOREIGN KEY REFERENCES tblUser(UserID),
    ManagerID int FOREIGN KEY REFERENCES tblManager(ManagerID),
	 

);

ALTER TABLE tblEmployee
ADD IsNewRequest bit;

--we create table tblAdmin
 CREATE TABLE tblAdmin (
    AdminID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    ExpiryDate date,
	AdministratorType varchar(20),
	UserID int FOREIGN KEY REFERENCES tblUser(UserID)  

);

--we create table tblRequestForChange
 CREATE TABLE tblRequestForChange (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CreationDate date,
    OldEmployeeID int FOREIGN KEY REFERENCES tblEmployee(EmployeeID),
	NewEmployeeID int FOREIGN KEY REFERENCES tblEmployee(EmployeeID)
);

--we create table tblProject
 CREATE TABLE tblProject (
    ProjectName varchar(50),
	ClientName varchar(50),
	ProjectDescription varchar(200),
	CreationDate date,
	StartDate date,
	AgreedFinishingDate date,
	HourPayment decimal,
	Realization varchar(20),
	ContractManagerID int FOREIGN KEY REFERENCES tblManager(ManagerID),
	ProjectLeaderID int FOREIGN KEY REFERENCES tblManager(ManagerID),
    PRIMARY KEY (ProjectName, ClientName)
);

--we create table tblReport
 CREATE TABLE tblReport (
    ReportID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CreationDate date,
	ProjectName varchar(50),
	ClientName varchar(50),
   	EmployeeID int FOREIGN KEY REFERENCES tblEmployee(EmployeeID) ON DELETE CASCADE,
	  
);

--we create table tblDailyReport
 CREATE TABLE tblDailyReport (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CreationDate date,
	ProjectName varchar(50),
	ClientName varchar(50),
   
	CONSTRAINT Project FOREIGN KEY
    (ProjectName, ClientName)
     REFERENCES tblProject (ProjectName, ClientName),
	 ReportID int FOREIGN KEY REFERENCES tblReport(ReportID) ON DELETE CASCADE
	 
);

GO
CREATE VIEW vwEmployee
AS

SELECT   dbo.tblEmployee.YearsOfService ,dbo.tblEmployee.Salary,
         dbo.tblEmployee.ManagerID ,dbo.tblEmployee.ProfessionalQualifications,
		 dbo.tblEmployee.PositionID ,dbo.tblEmployee.SectorID ,
         dbo.tblUser.UserID, dbo.tblUser.FirstName, dbo.tblUser.LastName, dbo.tblUser.Gender,
         dbo.tblUser.JMBG, dbo.tblUser.Residence,dbo.tblUser.MaritalStatus,
		 dbo.tblUser.Username 
FROM            dbo.tblUser INNER JOIN
            dbo.tblEmployee ON dbo.tblEmployee.UserID = dbo.tblUser.UserID  
			INNER JOIN
            dbo.tblSector ON dbo.tblSector.SectorID = dbo.tblEmployee.SectorID  
           
GO

GO
CREATE VIEW vwEmployee1
AS

SELECT   dbo.tblEmployee.YearsOfService ,dbo.tblEmployee.Salary,
         dbo.tblEmployee.ManagerID ,dbo.tblEmployee.ProfessionalQualifications,
		 dbo.tblEmployee.PositionID ,dbo.tblEmployee.SectorID ,
         dbo.tblUser.UserID, dbo.tblUser.FirstName, dbo.tblUser.LastName, dbo.tblUser.Gender,
         dbo.tblUser.JMBG, dbo.tblUser.Residence,dbo.tblUser.MaritalStatus,
		 dbo.tblUser.Username 
FROM            dbo.tblUser INNER JOIN
            dbo.tblEmployee ON dbo.tblEmployee.UserID = dbo.tblUser.UserID  
			INNER JOIN
            dbo.tblSector ON dbo.tblSector.SectorID = dbo.tblEmployee.SectorID  
           
GO

GO
CREATE VIEW vwEmployee2
AS

SELECT   dbo.tblEmployee.YearsOfService ,dbo.tblEmployee.Salary,
         dbo.tblEmployee.ManagerID ,dbo.tblEmployee.ProfessionalQualifications,
		 dbo.tblEmployee.PositionID ,dbo.tblEmployee.SectorID , dbo.tblEmployee.EmployeeID,
         dbo.tblUser.UserID, dbo.tblUser.FirstName, dbo.tblUser.LastName, dbo.tblUser.Gender,
         dbo.tblUser.JMBG, dbo.tblUser.Residence,dbo.tblUser.MaritalStatus, 
		 dbo.tblEmployee.IsNewRequest,
		 dbo.tblUser.Username 
FROM            dbo.tblUser INNER JOIN
            dbo.tblEmployee ON dbo.tblEmployee.UserID = dbo.tblUser.UserID  
			INNER JOIN
            dbo.tblSector ON dbo.tblSector.SectorID = dbo.tblEmployee.SectorID  
           
GO

 

insert into tblSector(SectorName)values('default');