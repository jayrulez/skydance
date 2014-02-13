USE [Billbox]
GO


/***********************************************************Parish*********************************************************************/

INSERT INTO Parish(Name) VALUES ('Hanover')
GO
INSERT INTO Parish(Name) VALUES ('Saint Elizabeth')
GO
INSERT INTO Parish(Name) VALUES ('Saint James')
GO
INSERT INTO Parish(Name) VALUES ('Trelawny')
GO
INSERT INTO Parish(Name) VALUES ('Westmoreland')
GO
INSERT INTO Parish(Name) VALUES ('Clarendon')
GO
INSERT INTO Parish(Name) VALUES ('Manchester')
GO
INSERT INTO Parish(Name) VALUES ('Saint Ann')
GO
INSERT INTO Parish(Name) VALUES ('Saint Catherine')
GO
INSERT INTO Parish(Name) VALUES ('Saint Mary')
GO
INSERT INTO Parish(Name) VALUES ('Kingston')
GO
INSERT INTO Parish(Name) VALUES ('Portland')
GO
INSERT INTO Parish(Name) VALUES ('Saint Andrew')
GO
INSERT INTO Parish(Name) VALUES ('Saint Thomas')
GO

/***********************************************************User Level*********************************************************************/
INSERT INTO UserLevel (LevelName) VALUES ('administrator')
GO

INSERT INTO UserLevel (LevelName) VALUES ('agentadmin')
GO

INSERT INTO UserLevel (LevelName) VALUES ('agentuser')
GO

/***********************************************************User*********************************************************************/
INSERT INTO [User](UserLevelId, Name, Username, Password, PasswordExpireAt, LoginStatus, Designation, AddressStreet, AddressCity, ParishId, ContactNumber, EmailAddress) 
VALUES (1, 'Admin', 'admin', 'admin', '', 1, 'Admin', 'Kingston', 'Kingston', 1, '12345', 'admin@fuf.ja')
GO

INSERT INTO [User](UserLevelId, Name, Username, Password, PasswordExpireAt, LoginStatus, Designation, AddressStreet, AddressCity, ParishId, ContactNumber, EmailAddress) 
VALUES (2, 'Agent Admin', 'agentadmin', 'agentadmin', '', 1, 'Agent Admin', 'Kingston', 'Kingston', 5, '12345', 'agentadmin@fuf.ja')
GO

INSERT INTO [User](UserLevelId, Name, Username, Password, PasswordExpireAt, LoginStatus, Designation, AddressStreet, AddressCity, ParishId, ContactNumber, EmailAddress) 
VALUES (2, 'Agent User', 'agentuser', 'agentuser', '', 1, 'Agent User', 'Kingston', 'Kingston', 6, '12345', 'agentuser@fuf.ja')
GO

/***********************************************************Subscriber*********************************************************************/

INSERT INTO Subscriber (Name, OperatingName, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress, Website)
VALUES ('JPS', 'Jamaica Public Service', 'Kingston', 'Kingston', 1, '1234567', '1234567', '1234567@fu.ja', 'http://link.com')
GO

INSERT INTO Subscriber (Name, OperatingName, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress, Website)
VALUES ('NWC', 'National Water Commision', 'Kingston', 'Kingston', 1, '1234567', '1234567', '1234567@nwc.ja', 'http://link.com')
GO

INSERT INTO Subscriber (Name, OperatingName, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress, Website)
VALUES ('FLOW', 'Flow Jamaica', 'Kingston', 'Kingston', 1, '1234567', '1234567', '1234567@flow.ja', 'http://link.com')
GO

INSERT INTO Subscriber (Name, OperatingName, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress, Website)
VALUES ('LIME', 'Lime Jamaica', 'Kingston', 'Kingston', 1, '1234567', '1234567', '1234567@lime.ja', 'http://link.com')
GO

INSERT INTO Subscriber (Name, OperatingName, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress, Website)
VALUES ('SLB', 'Student Loan Bureau', 'Kingston', 'Kingston', 1, '1234567', '1234567', '1234567@slb.ja', 'http://link.com')
GO

INSERT INTO Subscriber (Name, OperatingName, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress, Website)
VALUES ('WNET', 'World Net', 'Kingston', 'Kingston', 1, '1234567', '1234567', '1234567@wnet.ja', 'http://link.com')
GO

/***********************************************************Captured Field*********************************************************************/
INSERT INTO CaptureField (SubscriberId, Name, DisplayName, Type, OrderNum) VALUES (1, 'accountnumber', 'Account Number', 1, 1)
GO

INSERT INTO CaptureField (SubscriberId, Name, DisplayName, Type, OrderNum) VALUES (1, 'premisesnumber', 'Premises Number', 1, 2)
GO

/***********************************************************Payment Type*********************************************************************/
INSERT INTO PaymentType (Name) VALUES ('Cash')
GO

INSERT INTO PaymentType (Name) VALUES ('Cheque')
GO

INSERT INTO PaymentType (Name) VALUES ('Visa')
GO

INSERT INTO PaymentType (Name) VALUES ('DebitCard')
GO

/***********************************************************Payment Type Captured Field*********************************************************************/
INSERT INTO PaymentTypeCaptureField (PaymentTypeId, Name, DisplayName, Type, OrderNum) VALUES (1, 'test', 'Test Field', 1, 1)
GO

INSERT INTO PaymentTypeCaptureField (PaymentTypeId, Name, DisplayName, Type, OrderNum) VALUES (2, 'accountnumber', 'Account Number', 1, 2)
GO

INSERT INTO PaymentTypeCaptureField (PaymentTypeId, Name, DisplayName, Type, OrderNum) VALUES (2, 'bank', 'Bank', 1, 1)
GO

INSERT INTO PaymentTypeCaptureField (PaymentTypeId, Name, DisplayName, Type, OrderNum) VALUES (3, 'cardnumber', 'Card Nnumber', 1, 1)
GO

/***********************************************************Agent*********************************************************************/
INSERT INTO Agent (Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES ('Union One', '2 Cymanthia Ave', 'Kingston', 11, '8761234567', '8761234567', 'union@one.com')
GO

INSERT INTO Agent (Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES ('Union Two', '2 Cashville Ave', 'Kingston', 11, '8761234567', '8761234567', 'union@two.com')
GO

/***********************************************************Agent Branch*********************************************************************/
INSERT INTO AgentBranch (AgentId, Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES (1, 'Cassia Park', '10 Verene Ave', 'Kingston', 11, '8761234657', '8761234657', 'union@one.com')
GO

INSERT INTO AgentBranch (AgentId, Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES (1, 'New Kingston', '12 Trafalger Rd', 'Kingston', 11, '8761234657', '8761234657', 'union2@one.com')
GO

INSERT INTO AgentBranch (AgentId, Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES (2, 'Cedar Park', '10 Verene Ave', 'Kingston', 11, '8761234657', '8761234657', 'union1@two.com')
GO

INSERT INTO AgentBranch (AgentId, Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES (2, 'Cassia Ave', '10 Verene Ave', 'Kingston', 11, '8761234657', '8761234657', 'union2@two.com')


/***********************************************************User Right*********************************************************************/
INSERT INTO UserRight (RightName) VALUES('Create User')
INSERT INTO UserRight (RightName) VALUES('Edit User')
INSERT INTO UserRight (RightName) VALUES('Delete User')
INSERT INTO UserRight (RightName) VALUES('Reset Password')
INSERT INTO UserRight (RightName) VALUES('Process Payment')
INSERT INTO UserRight (RightName) VALUES('Print Report')
GO


/***********************************************************User Right User Level*********************************************************************/
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (1, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (2, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (3, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (4, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (5, 3)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (6, 3)


/***********************************************************Payment*********************************************************************/
INSERT INTO Payment (SubscriberId, InvoiceNumber, AgentId, AgentBranchId, UserId, Date, Status)
VALUES(1, 100, 1, 1, 3, GETDATE(), 1)

INSERT INTO Payment (SubscriberId, InvoiceNumber, AgentId, AgentBranchId, UserId, Date, Time, Status)
VALUES(2, 101, 2, 1, 3, GETDATE(), 1)

INSERT INTO Payment (SubscriberId, InvoiceNumber, AgentId, AgentBranchId, UserId, Date, Status)
VALUES(2, 102, 2, 1, 3, GETDATE(), 1)
GO

/***********************************************************Payment Info*********************************************************************/
INSERT INTO PaymentInfo (PaymentId, PaymentTypeId, Amount)
VALUES (1, 1, 2500.00)

INSERT INTO PaymentInfo (PaymentId, PaymentTypeId, Amount)
VALUES (2, 3, 2500.98)

INSERT INTO PaymentInfo (PaymentId, PaymentTypeId, Amount)
VALUES (2, 2, 2700.00)

/***********************************************************Payment Captured Field*********************************************************************/
INSERT INTO PaymentCaptureField (PaymentId, CaptureFieldId, Value)
VALUES (1, 1, '2500.00')

INSERT INTO PaymentCaptureField (PaymentId, CaptureFieldId, Value)
VALUES (2, 2, '1000.00')

/***********************************************************Payment PaymentType Captured Field*********************************************************************/
INSERT INTO PaymentPaymentTypeCaptureField (PaymentId, PaymentTypeCaptureFieldId, Value)
VALUES (1, 1, '2500.00')



