/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'OfferAppUser')
DROP USER [OfferAppUser]
GO

/****** Object:  Login [OfferAppUser]    Script Date: 08/25/2010 10:31:45 ******/
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'OfferAppUser')
DROP LOGIN [OfferAppUser]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [OfferAppUser]   Script Date: 08/25/2010 10:31:45 ******/
CREATE LOGIN [OfferAppUser] WITH PASSWORD='OfferAppUserPassword', DEFAULT_DATABASE=[OfferApp], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

 
CREATE USER [OfferAppUser] FOR LOGIN [OfferAppUser] 
GO


EXEC sp_addrolemember N'db_owner', N'OfferAppUser'
go
