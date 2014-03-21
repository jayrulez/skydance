use master
go

drop database Billbox
go

create database Billbox
go

use Billbox
go

create table Settings (
	Name VARCHAR(64) PRIMARY KEY NOT NULL, 
	DisplayName VARCHAR(255) NOT NULL,
	Type SMALLINT DEFAULT NULL,  
	Value TEXT DEFAULT NULL
);

GO

create table Parish (
	ParishId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	Name VARCHAR(30) NOT NULL,
	CONSTRAINT UK_Parish UNIQUE (Name)
);

GO

create table Agent (
	AgentId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	Name VARCHAR(40) NOT NULL,
	AddressStreet VARCHAR(50) NOT NULL, 
	AddressCity VARCHAR(30) NOT NULL, 
	ParishId INT NOT NULL, 
	ContactNumber VARCHAR(15) NOT NULL, 
	FaxNumber VARCHAR(15) DEFAULT NULL, 
	EmailAddress VARCHAR(50) NOT NULL,
	Inactive BIT DEFAULT 0,
	CONSTRAINT UK_Agent_Name UNIQUE (Name),
	CONSTRAINT UK_Agent_EmailAddress UNIQUE (EmailAddress)
);

GO

ALTER TABLE Agent
ADD CONSTRAINT FK_Agent_Parish FOREIGN KEY (ParishId) REFERENCES Parish (ParishId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table AgentBranch (
	BranchId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	AgentId INT NOT NULL, 
	Name VARCHAR(40) NOT NULL, 
	AddressStreet VARCHAR(50) NOT NULL, 
	AddressCity VARCHAR(30) NOT NULL, 
	ParishId INT NOT NULL, 
	ContactNumber VARCHAR(15) NOT NULL, 
	FaxNumber VARCHAR(15) DEFAULT NULL, 
	EmailAddress VARCHAR(50) NOT NULL,
	Inactive BIT DEFAULT 0,
	CONSTRAINT UK_AgentBranch_Name UNIQUE (Name),
	CONSTRAINT UK_AgentBranch_EmailAddress UNIQUE (EmailAddress)
);

GO

ALTER TABLE AgentBranch
ADD CONSTRAINT FK_AgentBranch_Agent FOREIGN KEY (AgentId) REFERENCES Agent (AgentId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE AgentBranch
ADD CONSTRAINT FK_AgentBranch_Parish FOREIGN KEY (ParishId) REFERENCES Parish (ParishId) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO

create table UserRight (
	RightId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	Name VARCHAR(40) NOT NULL,
	DisplayName VARCHAR(100) DEFAULT NULL,
	CONSTRAINT UK_UserRight_RightName UNIQUE (Name)
);

GO

create table UserLevel (
	LevelId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	LevelName VARCHAR(40) NOT NULL,
	CONSTRAINT UK_UserLevel_LevelName UNIQUE (LevelName)
);



create table UserRight_UserLevel (
	RightId INT NOT NULL,
	LevelId INT NOT NULL,
	CONSTRAINT PK_UserRight_UserLevel PRIMARY KEY (RightId, LevelId)
);

GO

ALTER TABLE UserRight_UserLevel
ADD CONSTRAINT FK_UserRight_UserLevel_UserRight FOREIGN KEY (RightId) REFERENCES UserRight (RightId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE UserRight_UserLevel
ADD CONSTRAINT FK_UserRight_UserLevel_UserLevel FOREIGN KEY (LevelId) REFERENCES UserLevel (LevelId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table [User] (
	UserId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	UserLevelId INT NOT NULL,
	AgentId INT DEFAULT NULL,
	AgentBranchId INT DEFAULT NULL,
	Name VARCHAR(60) NOT NULL, 
	Username VARCHAR(32) NOT NULL, 
	Password VARCHAR(128) NOT NULL, 
	PasswordExpireAt DATE DEFAULT NULL,
	AddressStreet VARCHAR(50) NOT NULL, 
	AddressCity VARCHAR(30) NOT NULL, 
	ParishId INT NOT NULL, 
	ContactNumber VARCHAR(15) DEFAULT NULL,
	EmailAddress VARCHAR(50) DEFAULT NULL,
	CONSTRAINT UK_User_Username UNIQUE (Username),
	CONSTRAINT UK_User_EmailAddress UNIQUE (EmailAddress)
);

GO

ALTER TABLE [User]
ADD CONSTRAINT FK_User_Agent FOREIGN KEY (AgentId) REFERENCES Agent (AgentId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE [User]
ADD CONSTRAINT FK_User_AgentBranch FOREIGN KEY (AgentBranchId) REFERENCES AgentBranch (BranchId) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [User]
ADD CONSTRAINT FK_User_Parish FOREIGN KEY (ParishId) REFERENCES Parish (ParishId) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE [User]
ADD CONSTRAINT FK_User_UserLevel FOREIGN KEY (UserLevelId) REFERENCES UserLevel (LevelId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table Subscriber (
	SubscriberId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	Name VARCHAR(40) NOT NULL, 
	OperatingName VARCHAR(50) DEFAULT NULL, 
	AddressStreet VARCHAR(50) NOT NULL, 
	AddressCity VARCHAR(30) NOT NULL, 
	ParishId INT NOT NULL, 
	ContactNumber VARCHAR(15) NOT NULL, 
	FaxNumber VARCHAR(15) DEFAULT NULL, 
	EmailAddress VARCHAR(50) NOT NULL,
	Website VARCHAR(50) DEFAULT NULL,
	CONSTRAINT UK_Subscriber_Name UNIQUE (Name),
	CONSTRAINT UK_Subscriber_OperatingName UNIQUE (OperatingName),
	CONSTRAINT UK_Subscriber_EmailAddress UNIQUE (EmailAddress)
);

GO

ALTER TABLE Subscriber
ADD CONSTRAINT FK_Subscriber_Parish FOREIGN KEY (ParishId) REFERENCES Parish (ParishId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table CaptureField (
	CaptureFieldId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	SubscriberId INT NOT NULL, 
	Name VARCHAR(40) NOT NULL, 
	DisplayName VARCHAR(60) NOT NULL,
	Type SMALLINT DEFAULT NULL,  
	OrderNum INT DEFAULT NULL
	CONSTRAINT UK_CaptureField UNIQUE (SubscriberId, Name)
);

GO

ALTER TABLE CaptureField
ADD CONSTRAINT FK_CaptureField_Subscriber FOREIGN KEY (SubscriberId) REFERENCES Subscriber (SubscriberId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table Bill (
	BillId INT IDENTITY(10001,1) PRIMARY KEY NOT NULL, 
	SubscriberId INT NOT NULL,
	AgentId INT NOT NULL, 
	AgentBranchId INT NOT NULL, 
	UserId INT NOT NULL, 
	Date DATETIME NOT NULL,
	Status INT NOT NULL,
	CustomerName VARCHAR(255) DEFAULT NULL,
	ProcessingFee FLOAT DEFAULT NULL,
	ProcessingFeeGCT FLOAT DEFAULT NULL,
	Commission FLOAT DEFAULT NULL,
	CommissionGCT FLOAT DEFAULT NULL
);

GO

ALTER TABLE Bill
ADD CONSTRAINT FK_Bill_Subscriber FOREIGN KEY (SubscriberId) REFERENCES Subscriber (SubscriberId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE Bill
ADD CONSTRAINT FK_Bill_Agent FOREIGN KEY (AgentId) REFERENCES Agent (AgentId) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Bill
ADD CONSTRAINT FK_Bill_AgentBranch FOREIGN KEY (AgentBranchId) REFERENCES AgentBranch (BranchId) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Bill
ADD CONSTRAINT FK_Bill_User FOREIGN KEY (UserId) REFERENCES [User] (UserId) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO

create table BillCaptureField (
	BillId INT NOT NULL, 
	CaptureFieldId INT NOT NULL, 
	Value TEXT NOT NULL,
	CONSTRAINT PK_BillCaptureField PRIMARY KEY (BillId, CaptureFieldId)
);

GO

ALTER TABLE BillCaptureField
ADD CONSTRAINT FK_BillCaptureField_Bill FOREIGN KEY (BillId) REFERENCES Bill (BillId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE BillCaptureField
ADD CONSTRAINT FK_BillCaptureField_CaptureField FOREIGN KEY (CaptureFieldId) REFERENCES CaptureField (CaptureFieldId) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO

create table PaymentMethod (
	PaymentMethodId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,  
	Name VARCHAR(40) NOT NULL, 
	CONSTRAINT UK_PaymentMethod_Name UNIQUE (Name)
);

GO

create table PaymentMethodCaptureField
(
	PaymentMethodCaptureFieldId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	PaymentMethodId INT NOT NULL, 
	Name VARCHAR(40) NOT NULL, 
	DisplayName VARCHAR(60) NOT NULL,
	Type SMALLINT DEFAULT NULL,  
	OrderNum INT DEFAULT NULL
	CONSTRAINT UK_PaymentMethodCaptureField UNIQUE (PaymentMethodId, Name)
);

ALTER TABLE PaymentMethodCaptureField
ADD CONSTRAINT FK_PaymentMethodCaptureField_PaymentMethod FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethod (PaymentMethodId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table Payment (
	PaymentId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PaymentMethodId INT NOT NULL,
	BillId INT NOT NULL, 
	Amount FLOAT NOT NULL
);

GO

ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_Bill FOREIGN KEY (BillId) REFERENCES Bill (BillId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_PaymentMethod FOREIGN KEY (PaymentMethodId) REFERENCES PaymentMethod (PaymentMethodId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table PaymentPaymentMethodCaptureField (
	PaymentId INT NOT NULL, 
	PaymentMethodCaptureFieldId INT NOT NULL, 
	Value TEXT NOT NULL,
	CONSTRAINT PK_PaymentPaymentMethodCaptureField PRIMARY KEY (PaymentId, PaymentMethodCaptureFieldId)
);

ALTER TABLE PaymentPaymentMethodCaptureField
ADD CONSTRAINT FK_PaymentPaymentMethodCaptureField_Payment FOREIGN KEY (PaymentId) REFERENCES Payment (PaymentId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE PaymentPaymentMethodCaptureField
ADD CONSTRAINT FK_PaymentPaymentMethodCaptureField_PaymentCaptureField FOREIGN KEY (PaymentMethodCaptureFieldId) REFERENCES PaymentMethodCaptureField (PaymentMethodCaptureFieldId) ON DELETE NO ACTION ON UPDATE NO ACTION;