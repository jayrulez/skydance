USE [Billbox]
GO


INSERT INTO Settings(Name, DisplayName, Value) VALUES ('ProcessingFee', 'Processing Fee ($)', '25.00')
GO
INSERT INTO Settings(Name, DisplayName, Value) VALUES ('ProcessingFeeGCT', 'Processing Fee GCT (%)', '2.5')
GO
INSERT INTO Settings(Name, DisplayName, Value) VALUES ('CommissionRate', 'Commission (%)', '2.5')
GO
INSERT INTO Settings(Name, DisplayName, Value) VALUES ('CommissionGCT', 'Commission GCT (%)', '2.5')
GO
INSERT INTO Settings(Name, DisplayName, Value, IsSystemSetting) VALUES ('NextReceiptNumber', 'ReceiptNumber', '2001411210', 1)
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
INSERT INTO [User](UserLevelId, Name, Username, Password, PasswordExpireAt, AddressStreet, AddressCity, ParishId, ContactNumber, EmailAddress) 
VALUES (1, 'Admin', 'admin', 'admin', '', 'Kingston', 'Kingston', 1, '12345', 'admin@fuf.ja')
GO

INSERT INTO [User](UserLevelId, Name, Username, Password, PasswordExpireAt, AddressStreet, AddressCity, ParishId, ContactNumber, EmailAddress) 
VALUES (2, 'Agent Admin', 'agentadmin', 'agentadmin', '', 'Kingston', 'Kingston', 5, '12345', 'agentadmin@fuf.ja')
GO

INSERT INTO [User](UserLevelId, Name, Username, Password, PasswordExpireAt, AddressStreet, AddressCity, ParishId, ContactNumber, EmailAddress) 
VALUES (2, 'Agent User', 'agentuser', 'agentuser', '', 'Kingston', 'Kingston', 6, '12345', 'agentuser@fuf.ja')
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
INSERT INTO PaymentMethod (Name) VALUES ('Cash')
GO

INSERT INTO PaymentMethod (Name) VALUES ('Cheque')
GO

INSERT INTO PaymentMethod (Name) VALUES ('Visa')
GO

INSERT INTO PaymentMethod (Name) VALUES ('DebitCard')
GO

/***********************************************************Payment Type Captured Field*********************************************************************/
INSERT INTO PaymentMethodCaptureField (PaymentMethodId, Name, DisplayName, Type, OrderNum) VALUES (3, 'expirydate', 'Expiry Date', 1, 1)
GO

INSERT INTO PaymentMethodCaptureField (PaymentMethodId, Name, DisplayName, Type, OrderNum) VALUES (2, 'accountnumber', 'Account Number', 1, 2)
GO

INSERT INTO PaymentMethodCaptureField (PaymentMethodId, Name, DisplayName, Type, OrderNum) VALUES (2, 'bank', 'Bank', 1, 1)
GO

INSERT INTO PaymentMethodCaptureField (PaymentMethodId, Name, DisplayName, Type, OrderNum) VALUES (3, 'cardnumber', 'Card Nnumber', 1, 1)
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
INSERT INTO UserRight (Name, DisplayName) VALUES('PROCESS_PAYMENT', 'Process Payment')
INSERT INTO UserRight (Name, DisplayName) VALUES('UNPOST_BILL', 'Unpost Bill')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_BILL', 'View Payment')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_PAYMENT_HISTORY', 'View Payment History')

INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_AGENTS', 'View Agents')
INSERT INTO UserRight (Name, DisplayName) VALUES('CREATE_AGENT', 'Create Agent')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_AGENT', 'View Agent')
INSERT INTO UserRight (Name, DisplayName) VALUES('EDIT_AGENT', 'Edit Agent')

INSERT INTO UserRight (Name, DisplayName) VALUES('CREATE_AGENT_BRANCH', 'Create Agent Branch')
INSERT INTO UserRight (Name, DisplayName) VALUES('EDIT_AGENT_BRANCH', 'Edit Agent Branch')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_AGENT_BRANCH', 'View Agent Branch')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_AGENT_BRANCHES', 'View Agent Branches')

INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_PAYMENT_METHODS', 'View Payment Methods')
INSERT INTO UserRight (Name, DisplayName) VALUES('CREATE_PAYMENT_METHOD', 'Create Payment Method')
INSERT INTO UserRight (Name, DisplayName) VALUES('EDIT_PAYMENT_METHOD', 'Edit Payment Method')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_PAYMENT_METHOD', 'View Payment Method')
INSERT INTO UserRight (Name, DisplayName) VALUES('CREATE_PAYMENT_METHOD_CAPTURE_FIELD', 'Create Payment Method Capture Field')
INSERT INTO UserRight (Name, DisplayName) VALUES('EDIT_PAYMENT_METHOD_CAPTURE_FIELD', 'Edit Payment Method Capture Field')
INSERT INTO UserRight (Name, DisplayName) VALUES('ORDER_PAYMENT_METHOD_CAPTURE_FIELDS', 'Order Payment Method Capture Field')

INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_SUBSCRIBER', 'View Subscriber')
INSERT INTO UserRight (Name, DisplayName) VALUES('EDIT_SUBSCRIBER', 'Edit Subscriber')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_SUBSCRIBERS', 'View Subscribers')
INSERT INTO UserRight (Name, DisplayName) VALUES('CREATE_SUBSCRIBER', 'Create Subscriber')
INSERT INTO UserRight (Name, DisplayName) VALUES('EDIT_SUBSCRIBER_CAPTURE_FIELD', 'Edit Subscriber Capture Field')
INSERT INTO UserRight (Name, DisplayName) VALUES('CREATE_SUBSCRIBER_CAPTURE_FIELD', 'Create Subscriber Capture Field')

INSERT INTO UserRight (Name, DisplayName) VALUES('CREATE_USER', 'Create User')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_USER', 'View User')
INSERT INTO UserRight (Name, DisplayName) VALUES('EDIT_USER', 'Edit User')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_USERS', 'View Users')

INSERT INTO UserRight (Name, DisplayName) VALUES('ASSIGN_USER_RIGHTS', 'Assign User Rights')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_USER_RIGHTS', 'View User Rights')

INSERT INTO UserRight (Name, DisplayName) VALUES('GENERATE_REPORT', 'Generate Report')
INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_RECEIPT', 'View Receipt')

INSERT INTO UserRight (Name, DisplayName) VALUES('VIEW_SETTINGS', 'View Settings')
INSERT INTO UserRight (Name, DisplayName) VALUES('CHANGE_SETTINGS', 'Change Settings')
INSERT INTO UserRight (Name, DisplayName) VALUES('REMOVE_PAYMENT', 'Remove Payment')

GO


/***********************************************************User Right User Level*********************************************************************/
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (1, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (2, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (3, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (4, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (5, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (6, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (7, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (8, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (9, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (10, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (11, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (12, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (13, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (14, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (15, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (16, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (17, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (18, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (19, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (20, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (21, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (22, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (23, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (24, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (25, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (26, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (27, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (28, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (29, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (30, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (31, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (32, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (33, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (34, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (35, 1)
INSERT INTO UserRight_UserLevel (RightId, LevelId) VALUES (36, 1)


/***********************************************************Bill*********************************************************************/
/*
INSERT INTO Bill (SubscriberId, InvoiceNumber, AgentId, AgentBranchId, UserId, Date, Status)
VALUES(1, 100, 1, 1, 3, GETDATE(), 1)

INSERT INTO Bill (SubscriberId, InvoiceNumber, AgentId, AgentBranchId, UserId, Date, Status)
VALUES(2, 101, 2, 1, 3, GETDATE(), 1)

INSERT INTO Bill (SubscriberId, InvoiceNumber, AgentId, AgentBranchId, UserId, Date, Status)
VALUES(2, 102, 2, 1, 3, GETDATE(), 1)
GO
*/
/***********************************************************Payment*********************************************************************/
/*
INSERT INTO Payment (PaymentMethodId, BillId, Amount)
VALUES (1, 1, 2500.00)

INSERT INTO Payment (PaymentMethodId, BillId, Amount)
VALUES (3, 2, 2500.98)

INSERT INTO Payment (PaymentMethodId, BillId, Amount)
VALUES (2, 2, 2700.00)
*/
/***********************************************************Bill Captured Field*********************************************************************/
/*
INSERT INTO BillCaptureField (BillId, CaptureFieldId, [Value])
VALUES (1, 1, '2500.00')

INSERT INTO BillCaptureField (BillId, CaptureFieldId, [Value])
VALUES (2, 2, '1000.00')
*/
/***********************************************************Payment PaymentMethod Captured Field*********************************************************************/
/*
INSERT INTO PaymentPaymentMethodCaptureField (PaymentId, PaymentMethodCaptureFieldId, [Value])
VALUES (1, 1, '2500.00')
*/



