USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spLOCAddLocation]    Script Date: 4/3/2017 10:20:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 3/28/2017
-- Description:	Inserts a location in the database and returns the ID of the locations
-- as represented in locLocation
-- TWO locations are inserted: one with the location details (floor, details), and one without
-- =============================================
CREATE PROCEDURE [dbo].[spLOCAddLocation]
	-- Add the parameters for the stored procedure here
	@strAddress varchar(200),
	@strCity varchar(50),
	@strStateProvince varchar(50),
	@strPostalCode varchar(30),
	@strCountry varchar(50),
	@strFloor varchar(50),
	@strDetail varchar(500),
	@keyDefinedBy bigint,
	@ID int OUTPUT

AS
BEGIN
	--DECLARE ID keys
	DECLARE @keyAddress int
	DECLARE @keyCity int
	DECLARE @keyStateProvince int
	DECLARE @keyPostalCode int
	DECLARE @keyCountry int
	DECLARE @keyDetail int
	
	--Temp table to hold IDs
	DECLARE @tmpTbl TABLE (ID int)
	
	--Temp table to hold text entries
	DECLARE @tmpS TABLE (text varchar(500))

	--Address:
	INSERT @tmpS VALUES (@strAddress)
	MERGE locAddress AS t
	USING @tmpS AS s
	ON t.strAddress = s.text
	WHEN MATCHED THEN
		UPDATE SET @keyAddress = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strAddress)
		VALUES (@strAddress)
		OUTPUT INSERTED.ID INTO @tmpTbl;
		SELECT TOP 1 @keyAddress = ID FROM @tmpTbl
	DELETE FROM @tmpS
	DELETE FROM @tmpTbl
	--city
	INSERT @tmpS VALUES (@strCity)
	MERGE loccity AS t
	USING @tmpS AS s
	ON t.strcity = s.text
	WHEN MATCHED THEN
		UPDATE SET @keycity = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strcity)
		VALUES (@strCity)
		OUTPUT INSERTED.ID INTO @tmpTbl;
		SELECT TOP 1 @keycity = ID FROM @tmpTbl
	DELETE FROM @tmpS
	DELETE FROM @tmpTbl
	--state/province
	INSERT @tmpS VALUES (@strStateProvince)
	MERGE locStateProvince AS t
	USING @tmpS AS s
	ON t.strStateProvince = s.text
	WHEN MATCHED THEN
		UPDATE SET @keyStateProvince = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strStateProvince)
		VALUES (@strStateProvince)
		OUTPUT INSERTED.ID INTO @tmpTbl;
		SELECT TOP 1 @keyStateProvince = ID FROM @tmpTbl
	DELETE FROM @tmpS
	DELETE FROM @tmpTbl
	--postal code
	INSERT @tmpS VALUES (@strPostalCode)
	MERGE locPostalCode AS t
	USING @tmpS AS s
	ON t.strPostalCode = s.text
	WHEN MATCHED THEN
		UPDATE SET @keyPostalCode = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strPostalCode)
		VALUES (@strPostalCode)
		OUTPUT INSERTED.ID INTO @tmpTbl;
		SELECT TOP 1 @keyPostalCode = ID FROM @tmpTbl
	DELETE FROM @tmpS
	DELETE FROM @tmpTbl
	--country
	INSERT @tmpS VALUES (@strCountry)
	MERGE locCountry AS t
	USING @tmpS AS s
	ON t.strCountry = s.text
	WHEN MATCHED THEN
		UPDATE SET @keyCountry = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strCountry)
		VALUES (@strCountry)
		OUTPUT INSERTED.ID INTO @tmpTbl;
		SELECT TOP 1 @keyCountry = ID FROM @tmpTbl
	DELETE FROM @tmpS
	DELETE FROM @tmpTbl
	--detail
	INSERT @tmpS VALUES (@strDetail)
	MERGE locDetail AS t
	USING @tmpS AS s
	ON t.strDetail = s.text
	WHEN MATCHED THEN
		UPDATE SET @keyDetail = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strDetail)
		VALUES (@strDetail)
		OUTPUT INSERTED.ID INTO @tmpTbl;
		SELECT TOP 1 @keyDetail = ID FROM @tmpTbl
	DELETE FROM @tmpS
	DELETE FROM @tmpTbl

	--DO FINAL MERGE FOR locLocation
	--Temp table to hold text entries
	DECLARE @tmpLoc TABLE (keyCountry int,
						   keyPostalCode int,
						   keyStateProvince int,
						   keyCity int, 
						   keyAddress int, 
						   keyDetail int,
						   strFloor varchar(50))
	DECLARE @outputTable TABLE (ID bigint)

	--Include a generic location, one where the Floor and Detail values are intentionally left blank
	--(the ID of this insertion will be discarded)
	DECLARE @throwAwayID int
	INSERT @tmpLoc VALUES (@keyCountry,
						   @keyPostalCode,
						   @keyStateProvince,
						   @keyCity,
						   @keyAddress,
						   1,
						   '')
	--*Note that locDetail.ID = 1 == blank entry!
	MERGE locLocation AS t
	USING @tmpLoc AS s
	ON t.keyCountry = s.keyCountry AND
	   t.keyPostalCode = s.keyPostalCode AND
	   t.keyStateProvince = s.keyStateProvince AND
	   t.keyCity = s.keyCity AND
	   t.keyAddress = s.keyAddress AND
	   t.keyDetail = 1 AND
	   t.strFloor = ''
	WHEN MATCHED THEN
		UPDATE SET @throwAwayID = t.ID
	WHEN NOT MATCHED THEN
		INSERT (keyCountry,
		        keyPostalCode,
		        keyStateProvince,
				keyCity,
				keyAddress,
				keyDetail,
				keyDefinedBy,
				strFloor)
		VALUES (@keyCountry,
				@keyPostalCode,
				@keyStateProvince,
				@keyCity,
				@keyAddress,
				1,
				@keyDefinedBy,
				'')
		OUTPUT INSERTED.ID INTO @outputTable;
		SELECT TOP 1 @throwAwayID = ID FROM @outputTable
	--RESET...
	DELETE FROM @tmpLoc
	DELETE FROM @outputTable
	--...and do the REAL insertion
	INSERT @tmpLoc VALUES (@keyCountry,
						   @keyPostalCode,
						   @keyStateProvince,
						   @keyCity,
						   @keyAddress,
						   @keyDetail,
						   @strFloor)
	MERGE locLocation AS t
	USING @tmpLoc AS s
	ON t.keyCountry = s.keyCountry AND
	   t.keyPostalCode = s.keyPostalCode AND
	   t.keyStateProvince = s.keyStateProvince AND
	   t.keyCity = s.keyCity AND
	   t.keyAddress = s.keyAddress AND
	   t.keyDetail = s.keyDetail AND
	   t.strFloor = s.strFloor
	WHEN MATCHED THEN
		UPDATE SET @ID = t.ID
	WHEN NOT MATCHED THEN
		INSERT (keyCountry,
		        keyPostalCode,
		        keyStateProvince,
				keyCity,
				keyAddress,
				keyDetail,
				keyDefinedBy,
				strFloor)
		VALUES (@keyCountry,
				@keyPostalCode,
				@keyStateProvince,
				@keyCity,
				@keyAddress,
				@keyDetail,
				@keyDefinedBy,
				@strFloor)
		OUTPUT INSERTED.ID INTO @outputTable;
		SELECT TOP 1 @ID = ID FROM @outputTable

END

GO
