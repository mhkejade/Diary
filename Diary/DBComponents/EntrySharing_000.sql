USE Diary
GO


IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'EntrySharing')
BEGIN
	CREATE TABLE dbo.EntrySharing
	(
		SharingId INT PRIMARY KEY IDENTITY(1,1),
		EntryId INT FOREIGN KEY REFERENCES dbo.Entries(EntryId),
		SharedToUserId INT FOREIGN KEY REFERENCES dbo.Users(UserId),
		CreatedDate DATETIME
	)
END
