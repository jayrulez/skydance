USE [Billbox]
GO
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
INSERT INTO UserLevel (LevelName) VALUES ('administrator')
GO

GO
INSERT INTO [User](UserLevelId, Name, Username, Password, PasswordExpireAt, LoginStatus, Designation, AddressStreet, AddressCity, ParishId, ContactNumber, EmailAddress) 
VALUES (1, 'Admin', 'admin', 'admin', '', 1, 'Admin', 'Kingston', 'Kingston', 1, '12345', 'admin@fuf.ja')
GO

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

INSERT INTO CaptureField (SubscriberId, Name, DisplayName, Type, OrderNum) VALUES (1, 'accountnumber', 'Account Number', 1, 1)
GO

INSERT INTO PaymentType (Name) VALUES ('Cash')
GO

INSERT INTO PaymentTypeCaptureField (PaymentTypeId, Name, DisplayName, Type, OrderNum) VALUES (1, 'test', 'Test Field', 1, 1)
GO

INSERT INTO Agent (Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES ('Union One', '2 Cymanthia Ave', 'Kingston', 11, '8761234567', '8761234567', 'union@one.com')

INSERT INTO AgentBranch (AgentId, Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES (1, 'Cassia Park', '10 Verene Ave', 'Kingston', 11, '8761234657', '8761234657', 'union@one.com')


INSERT INTO AgentBranch (AgentId, Name, AddressStreet, AddressCity, ParishId, ContactNumber, FaxNumber, EmailAddress)
VALUES (1, 'New Kingston', '12 Trafalger Rd', 'Kingston', 11, '8761234657', '8761234657', 'union2@one.com')