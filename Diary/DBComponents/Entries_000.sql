USE Diary
GO


IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Entries')
BEGIN
	CREATE TABLE dbo.Entries
	(
		EntryId INT PRIMARY KEY IDENTITY(1,1),
		UserId INT FOREIGN KEY REFERENCES dbo.Users(UserId),
		Title VARCHAR(255),
		Content NVARCHAR(MAX),
		CreationTime DATETIME,
		IsDeleted BIT DEFAULT(0),
		DeletedDate DATETIME
	)
END
