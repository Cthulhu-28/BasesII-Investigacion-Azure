CREATE procedure [dbo].[COUNTRY_INFO]
	@page bigint,
	@size bigint
AS

	SELECT C.COUNTRYID,C.COUNTRYNAME,AVG(YEAR(GETDATE())-YEAR(P.BIRTH_DATE)) AGE,COUNT(P.[RESIDENCE_COUNTRY]) POPULATION
	from COUNTRY C LEFT JOIN PERSON P ON (P.[RESIDENCE_COUNTRY]=C.COUNTRYID)
	GROUP BY C.COUNTRYID,C.COUNTRYNAME
	order by c.COUNTRYNAME
	OFFSET (@page-1)*@size rows
	fetch next (@page)*@size row only

GO
CREATE procedure [dbo].[INSERT_PERSON_fast]
	@prefix varchar(4),
	@count numeric(10),
	@photo varbinary(max),
	@country numeric(10)
as
	declare @id bigint, @max bigint,@_max int
	begin transaction
	begin try
		select @id = next value for sqn_people
		set @max=@id+@count
		set @_max=@id+@count;
		with cte_n as (
			select       @id as contador,
					     cast(CONCAT('PERSON-',@prefix,'-',@id) as varchar(100)) name,
						 DATEADD(DAY, ABS(CHECKSUM(NEWID()) % datediff(day,'1960-01-01',GETDATE())), '1960-01-01') date,
						 @country bc,
						 @country rc,
						 cast(CONCAT('per.',@id,'@mail.',@prefix) as varchar(50)) email,
						 --@photo photo,
						 CAST('video' as varchar(100)) video,
						 NEWID() f
			union all
			select       contador+1,
					     cast(CONCAT('PERSON-',@prefix,'-',contador+1) as varchar(100)) name,
						 DATEADD(DAY, ABS(CHECKSUM(NEWID()) % datediff(day,'1960-01-01',GETDATE())), '1960-01-01') date,
						 @country bc,
						 @country rc,
						 cast(CONCAT('per.',contador+1,'@mail.',@prefix) as varchar(50)) email,
						 --@photo photo,
						 CAST('video' as varchar(100)) video,
						 NEWID() f
			from cte_n 
			where contador <(@max-1)
		) insert into PERSON(IDENTIFICATION,NAME,BIRTH_DATE,BIRTH_COUNTRY,RESIDENCE_COUNTRY,EMAIL,VIDEO,FILEID) 
					select * from cte_n option (maxrecursion 0)
		--update PERSON set PHOTO=@photo where IDENTIFICATION>=@id and IDENTIFICATION<=(@_max-1)
		declare @q nvarchar(100)
		set @q=concat('alter sequence sqn_people restart with ', @_max)
		exec sp_executesql @q
		commit
	end try
	begin catch
		print(Error_message())
		rollback
	end catch
GO

CREATE procedure [dbo].[GENERATE_PEOPLE_fast]
	@countries numeric(10),
	@population numeric(10)
as

	declare @SELECTED_COUNTRIES table (IDX NUMERIC(10) identity(1,1),country numeric(10), population numeric(10),prefix varchar(4))
	declare @i numeric(10)
	insert into @SELECTED_COUNTRIES
	select top (CAST(@countries as int))COUNTRYID,0,left(COUNTRYNAME,4) from COUNTRY  order by NEWID()
	set @i=1
	while(@i<=@countries)
	begin
		declare @count numeric(10),@photo varbinary(max),@prefix varchar(4),@country numeric(10),@t int
		set @count=0
		select @country = country from @SELECTED_COUNTRIES where IDX=@i
		select @prefix = prefix from @SELECTED_COUNTRIES where IDX=@i
		select @t = COUNT(*) from [dbo].[traning_data]

			select @photo = photo from traning_data where number = (ABS(CHECKSUM(newid()))%@t)+1
			exec INSERT_PERSON_fast @prefix,@population,@photo,@country
		set @i=@i+1
	end

	select 1
GO
create procedure [dbo].[GET_PAGE_COUNTRY]
	@page int
as
	select ceiling(cast(COUNT(*) as float)/cast(@page as float)) N from COUNTRY
GO

create procedure [dbo].[GET_PAGE_PERSON]
	@page int
as
	select ceiling(cast(COUNT(*) as float)/cast(@page as float)) N from PERSON
GO

create procedure [dbo].[GET_PAISES]

AS
	SELECT COUNTRYID,COUNTRYNAME
	FROM COUNTRY

GO

CREATE procedure [dbo].[PEOPLE_YEAR_ALL_COUNTRIES]
	@page bigint,
	@size bigint
as
	select YEAR(P.BIRTH_DATE) YEAR, COUNT(*) BORN
	from PERSON P
	GROUP BY YEAR(P.BIRTH_DATE)
	ORDER BY YEAR(P.BIRTH_DATE)
	OFFSET (@page-1)*@size rows
	FETCH NEXT @size row only
GO

CREATE procedure [dbo].[PEOPLE_YEAR_COUNTRY]
	@id NUMERIC(10),
	@page bigint,
	@size bigint
as
	select YEAR(P.BIRTH_DATE) YEAR, COUNT(*) BORN
	from PERSON P
	WHERE P.BIRTH_COUNTRY=@id
	GROUP BY YEAR(P.BIRTH_DATE)
	ORDER BY YEAR(P.BIRTH_DATE)
	OFFSET (@page-1)*@size rows
	FETCH NEXT @size row only
GO

CREATE procedure [dbo].[SELECT_PIECE_PERSON]
	@n_page numeric(10) ,
	@page_size numeric(10)
as

	SELECT T.BIRTH_COUNTRY,T.BIRTH_DATE,T.BNAME ,T.EMAIL,T.IDENTIFICATION,T.NAME,T.RESIDENCE_COUNTRY,T.RNAME
	FROM (select P.BIRTH_COUNTRY,C.COUNTRYNAME BNAME ,P.BIRTH_DATE,P.EMAIL,P.IDENTIFICATION,
				 P.NAME,P.RESIDENCE_COUNTRY,C1.COUNTRYNAME RNAME,ROW_NUMBER() OVER(ORDER BY P.NAME) as number
	from PERSON P INNER JOIN COUNTRY C ON (p.BIRTH_COUNTRY=c.COUNTRYID)
				  inner join COUNTRY c1 on(p.RESIDENCE_COUNTRY=c1.COUNTRYID) ) T
	WHERE (T.number) <= @n_page*@page_size and (T.number) > (@n_page-1)*@page_size 

	
GO


CREATE procedure [dbo].[SELECT_PIECE]
	@n_page numeric(10) 
as
	declare @size int
	set @size=10

	SELECT T.COUNTRYID,T.COUNTRYNAME,T.AREA,T.POPULATION,T.PRESIDENT
	FROM (select P.[COUNTRYID],P.[COUNTRYNAME],P.[AREA],P.[POPULATION],P.[PRESIDENT], ROW_NUMBER() OVER(ORDER BY P.[COUNTRYNAME]) as number
	from COUNTRY P) T
	WHERE (T.number) <= @n_page*@size and (T.number) > (@n_page-1)*@size 

	
GO


create SEQUENCE [dbo].[sqn_people] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 9223372036854775807
 CACHE 
GO


CREATE PROCEDURE QUERY_II
AS
	select C.COUNTRYID,C.COUNTRYNAME,YEAR(P.BIRTH_DATE) "YEAR",COUNT(*) "COUNT"
	from PERSON p INNER join COUNTRY C ON (P.BIRTH_COUNTRY=C.COUNTRYID)
	GROUP BY C.COUNTRYID,C.COUNTRYNAME,YEAR(P.BIRTH_DATE) WITH ROLLUP
	UNION ALL
	SELECT NULL "COUNTRYID", NULL "COUNTRYNAME",YEAR(P.BIRTH_DATE) "YEAR",COUNT(*) "COUNT"
	FROM PERSON P INNER JOIN COUNTRY C ON (C.COUNTRYID=P.BIRTH_COUNTRY)	
	GROUP BY YEAR(P.BIRTH_DATE) WITH ROLLUP
go


create procedure MAX_DATES
as
	select max(YEAR(BIRTH_DATE)) maxi, min(year(BIRTH_DATE)) mini
	from PERSON
go