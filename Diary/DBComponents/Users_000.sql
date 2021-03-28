USE Diary
GO


IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Users')
BEGIN
	CREATE TABLE dbo.Users
	(
		UserId INT PRIMARY KEY IDENTITY(1,1),
		UserName VARCHAR(255)
	)
END

--add Dummy
IF NOT EXISTS (SELECT 1 FROM dbo.Users)
BEGIN
	INSERT INTO dbo.Users
	(
		UserName
	)
	VALUES ('mike'), ('mae'), ('mark')
END

