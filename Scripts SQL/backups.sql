create procedure backup_log_2
as
	declare @date nvarchar(100),@name nvarchar(max)
	select @date = CONVERT(varchar(50),getdate(),121)
	set @name = CONCAT('log_',@date)
	BACKUP LOG [WorldDB] TO  DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\WorldDB_log.bak'
	WITH NOFORMAT, NOINIT,  NAME = @name, SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO

create procedure backup_full_2
as
	declare @date nvarchar(100),@name nvarchar(max)
	select @date = CONVERT(varchar(50),getdate(),121)
	set @name = CONCAT('full_',@date)
	BACKUP database [WorldDB] TO  DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\WorldDB_full.bak'
	WITH NOFORMAT, NOINIT,  NAME = @name, SKIP, NOREWIND, NOUNLOAD,  STATS = 10
GO