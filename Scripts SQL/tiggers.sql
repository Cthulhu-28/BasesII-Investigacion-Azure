create trigger trg_udpate_country on country
instead of update
as
	if(update(countryid))
	begin
		alter table person
		nocheck CONSTRAINT BIRTH_PK
		ALTER TABLE PERSON
		NOCHECK CONSTRAINT RESIDENCE_PK
		declare cursor1 cursor for select COUNTRYID from deleted
		declare cursor2 cursor for select COUNTRYID from inserted
		declare @old numeric(10),@new numeric(10)
		open cursor1
		open cursor2
		fetch cursor1 into @old
		fetch cursor2 into @new
		while(@@FETCH_STATUS=0)
		begin
			UPDATE PERSON SET BIRTH_COUNTRY=@new WHERE BIRTH_COUNTRY=@old
			UPDATE PERSON SET RESIDENCE_COUNTRY=@new WHERE RESIDENCE_COUNTRY=@old
			fetch cursor1 into @old
			fetch cursor2 into @new
		end
		CLOSE cursor1
		close cursor2
		alter table person
		check CONSTRAINT BIRTH_PK
		ALTER TABLE PERSON
		CHECK CONSTRAINT RESIDENCE_PK
	end
	update COUNTRY
		set COUNTRYID=ins.COUNTRYID,COUNTRYNAME=ins.COUNTRYNAME,AREA=ins.AREA,
			POPULATION=ins.POPULATION,FLAG=ins.FLAG,ANTHEM=ins.ANTHEM,PRESIDENT=ins.PRESIDENT
	from(
			select *, ROW_NUMBER() over (order by (select 0)) as row_num
			from deleted
		) del
	join
		(
			select *,ROW_NUMBER() over (order by (select 0)) as row_num
			from inserted
		) ins
	on del.row_num=ins.row_num
	where COUNTRY.COUNTRYID=del.COUNTRYID

go
create trigger [dbo].[trg_delete_country] on [dbo].[COUNTRY]
instead of delete
as
	delete from person where BIRTH_COUNTRY in (select COUNTRYID from deleted)
						  or RESIDENCE_COUNTRY in (select COUNTRYID from deleted)
	delete from COUNTRY where COUNTRYID in (select COUNTRYID from deleted)
GO

ALTER TABLE [dbo].[COUNTRY] ENABLE TRIGGER [trg_delete_country]
GO
