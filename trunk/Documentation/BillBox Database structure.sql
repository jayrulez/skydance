create table Settings (
	Name VARCHAR(64) PRIMARY KEY NOT NULL, 
	Value TEXT DEFAULT NULL
);

GO

create table LoginSession (
	SessionId UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	UserType VARCHAR(15) NOT NULL,
	UserUniqueIdentifier INT NOT NULL,
	LastActiveAt DATETIME NOT NULL,
	ExpireAt DATETIME NOT NULL,
	CONSTRAINT UK_LoginSession UNIQUE (UserType, UserUniqueIdentifier)
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
	FaxNumber VARCHAR(15) NOT NULL, 
	EmailAddress VARCHAR(50) NOT NULL,
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
	FaxNumber VARCHAR(15) NOT NULL, 
	EmailAddress VARCHAR(50) NOT NULL,
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
	RightName VARCHAR(40) NOT NULL
	CONSTRAINT UK_UserRight_RightName UNIQUE (RightName)
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
	PasswordExpireAt DATE NOT NULL, 
	LoginStatus INT NOT NULL, 
	Designation VARCHAR(40) NOT NULL, 
	AddressStreet VARCHAR(50) NOT NULL, 
	AddressCity VARCHAR(30) NOT NULL, 
	ParishId INT NOT NULL, 
	ContactNumber VARCHAR(15) NOT NULL,
	EmailAddress VARCHAR(50) NOT NULL,
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
	OperatingName VARCHAR(50) NOT NULL, 
	AddressStreet VARCHAR(50) NOT NULL, 
	AddressCity VARCHAR(30) NOT NULL, 
	ParishId INT NOT NULL, 
	ContactNumber VARCHAR(15) NOT NULL, 
	FaxNumber VARCHAR(15) NOT NULL, 
	EmailAddress VARCHAR(50) NOT NULL,
	Website VARCHAR(50) NOT NULL,
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
	Type INT NOT NULL,  
	OrderNum INT NOT NULL
	CONSTRAINT UK_CaptureField UNIQUE (SubscriberId, Name)
);

GO

ALTER TABLE CaptureField
ADD CONSTRAINT FK_CaptureField_Subscriber FOREIGN KEY (SubscriberId) REFERENCES Subscriber (SubscriberId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table PaymentType (
	PaymentTypeId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,  
	Name VARCHAR(40) NOT NULL, 
	CONSTRAINT UK_PaymentType_Name UNIQUE (Name)
);

GO

create table Payment (
	PaymentId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	SubscriberId INT NOT NULL,
	InvoiceNumber INT NOT NULL, 
	AgentId INT NOT NULL, 
	AgentBranchId INT NOT NULL, 
	UserId INT NOT NULL, 
	Date DATETIME NOT NULL, 
	Time TIMESTAMP NOT NULL, 
	Status INT NOT NULL,
	CONSTRAINT UK_Payment_InvoiceNumber UNIQUE (InvoiceNumber)
);

GO

ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_Subscriber FOREIGN KEY (SubscriberId) REFERENCES Subscriber (SubscriberId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_Agent FOREIGN KEY (AgentId) REFERENCES Agent (AgentId) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_AgentBranch FOREIGN KEY (AgentBranchId) REFERENCES AgentBranch (BranchId) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE Payment
ADD CONSTRAINT FK_Payment_User FOREIGN KEY (UserId) REFERENCES [User] (UserId) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO

create table PaymentCaptureField (
	PaymentId INT NOT NULL, 
	CaptureFieldId INT NOT NULL, 
	Value TEXT NOT NULL,
	CONSTRAINT PK_PaymentCaptureField PRIMARY KEY (PaymentId, CaptureFieldId)
);

GO

ALTER TABLE PaymentCaptureField
ADD CONSTRAINT FK_PaymentCaptureField_Payment FOREIGN KEY (PaymentId) REFERENCES Payment (PaymentId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE PaymentCaptureField
ADD CONSTRAINT FK_PaymentCaptureField_CaptureField FOREIGN KEY (CaptureFieldId) REFERENCES CaptureField (CaptureFieldId) ON DELETE NO ACTION ON UPDATE NO ACTION;

GO

create table PaymentInfo (
	PaymentInfoId INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	PaymentId INT NOT NULL, 
	PaymentTypeId INT NOT NULL,
	Amount FLOAT NOT NULL
);

GO

ALTER TABLE PaymentInfo
ADD CONSTRAINT FK_PaymentInfo_Payment FOREIGN KEY (PaymentId) REFERENCES Payment (PaymentId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE PaymentInfo
ADD CONSTRAINT FK_PaymentInfo_PaymentType FOREIGN KEY (PaymentTypeId) REFERENCES PaymentType (PaymentTypeId) ON DELETE CASCADE ON UPDATE CASCADE;

GO

create table PaymentTypeCaptureField
(
	PaymentTypeCaptureFieldId INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
	PaymentTypeId INT NOT NULL, 
	Name VARCHAR(40) NOT NULL, 
	DisplayName VARCHAR(60) NOT NULL,
	Type INT NOT NULL,  
	OrderNum INT NOT NULL
	CONSTRAINT UK_PaymentTypeCaptureField UNIQUE (PaymentTypeId, Name)
);

ALTER TABLE PaymentTypeCaptureField
ADD CONSTRAINT FK_PaymentTypeCaptureField_PaymentType FOREIGN KEY (PaymentTypeId) REFERENCES PaymentType (PaymentTypeId) ON DELETE CASCADE ON UPDATE CASCADE;

create table PaymentPaymentTypeCaptureField (
	PaymentId INT NOT NULL, 
	PaymentTypeCaptureFieldId INT NOT NULL, 
	Value TEXT NOT NULL,
	CONSTRAINT PK_PaymentPaymentTypeCaptureField PRIMARY KEY (PaymentId, PaymentTypeCaptureFieldId)
);

ALTER TABLE PaymentPaymentTypeCaptureField
ADD CONSTRAINT FK_PaymentPaymentTypeCaptureField_Payment FOREIGN KEY (PaymentId) REFERENCES Payment (PaymentId) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE PaymentPaymentTypeCaptureField
ADD CONSTRAINT FK_PaymentPaymentTypeCaptureField_PaymentCaptureField FOREIGN KEY (PaymentTypeCaptureFieldId) REFERENCES PaymentTypeCaptureField (PaymentTypeCaptureFieldId) ON DELETE CASCADE ON UPDATE CASCADE;