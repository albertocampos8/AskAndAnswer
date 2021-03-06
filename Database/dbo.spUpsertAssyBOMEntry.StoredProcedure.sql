USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spUpsertAssyBOMEntry]    Script Date: 12/30/2016 10:15:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Alberto Campos
-- Create date: 12/12/16
-- Description:	Makes an entry in asyBOM if keyTopLevelName,
-- keyAssyPN, keyAssyRev, and intBOMRev do not yet exist in the table;
-- otherwise updates the table.
-- Returns the ID of the inserted/updated Assembly BOM
-- =============================================
CREATE PROCEDURE [dbo].[spUpsertAssyBOMEntry]
	-- The following four parameters determine whether a new entry is made in asyBOM 
	@strName varchar(30),			-- Assy Name; used to get asyNames.ID = asyBOM.keyTopLevelName
	@strAssyPartNumber varchar(20), -- Assy PN; used to get asyPNs.ID = asyBOM.keyAssyPN
	@strRevision varchar(4),		-- Assy Rev; use to get asyRevs.ID = asyBOMkeyAssyRev
	@intBOMRev smallint,			-- Revision of this Assembly BOM
	
	@strDescription varchar(100),	-- Description of the Assembly BOM; this is only used if
									-- we need to insert @strName into asyNames.
	
	@intMajor tinyint,				-- Major and
	@intMinor tinyint,				-- Minor
									-- revision.  Only needed if we need to insert @strRevision into asyRevs.
	
	@keyUploadedBy bigint,			-- ID of the Uploader
	@keyAssyStatus tinyint OUTPUT,	-- ID of the Assy Status: note keyAssyStatus is an OUTPUT.
									-- Do not use this procedure to change the status of an EXISTING Assy.
									-- Use this to query whether an assy exists, and if it does, to find its status.
	@keyAssyBU tinyint,				-- ID of the Assy BU
	
	@keyReasonForRev bigint = 1 OUTPUT,	-- the default value for asyBOM.keyReasonForRev
	@strReason varchar(MAX) = '',	-- if this is <> '', then an entry is made in asyChangeReasons,
									-- and the inserted ID is given to @keyReasonForRev.
	@ID BIGINT OUTPUT				--The inserted/exsiting ID of this Assembly BOM name/pn/revision/bom revision combo

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--tmpTable to hold inserted IDs that are bigints
	DECLARE @tBigintVar TABLE (ID bigint)
	DECLARE @tIntVar TABLE (ID bigint)

	--Get asyNames.ID == @keyTopLevelName
	DECLARE @keyTopLevelName bigint

	DECLARE @srcTblAsyNames TABLE (strName varchar(30))

	INSERT @srcTblAsyNames VALUES (@strName)
	
	MERGE asyNames AS t
	USING @srcTblAsyNames AS s
	ON t.strName = s.strName
	WHEN MATCHED THEN
		UPDATE SET @keyTopLevelName = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strName, strDescription)
		VALUES (@strName, @strDescription)
		OUTPUT INSERTED.ID INTO @tBigintVar;
		SELECT TOP 1 @keyTopLevelName = ID FROM @tBigintVar;
		
	--************ END ID FOR asyNames

	--Get asyPNs.ID == @keyAssyPN
	DELETE FROM @tBigintVar;
	DECLARE @keyAssyPN bigint

	DECLARE @srcTblAsyPNs TABLE (strAssyPartNumber varchar(20))

	INSERT @srcTblAsyPns VALUES (@strAssyPartNumber)
	
	MERGE asyPNs AS t
	USING @srcTblAsyPNs AS s
	ON t.strAssyPartNumber = s.strAssyPartNumber
	WHEN MATCHED THEN
		UPDATE SET @keyAssyPN = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strAssyPartNumber)
		VALUES (@strAssyPartNumber)
		OUTPUT INSERTED.ID INTO @tBigintVar;
		SELECT TOP 1 @keyAssyPN = ID FROM @tBigintVar;
	
	--************ END ID FOR asyPNs

	--Get asyRevs.ID == @keyAssyRev
	DECLARE @keyAssyRev int

	DECLARE @srcTblAsyRevs TABLE (strRevision varchar(4))

	INSERT @srcTblAsyRevs VALUES (@strRevision)
	
	MERGE asyRevs AS t
	USING @srcTblAsyRevs AS s
	ON t.strRevision = s.strRevision
	WHEN MATCHED THEN
		UPDATE SET @keyAssyRev = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strRevision, intMajor, intMinor)
		VALUES (@strRevision, @intMajor, @intMinor)
		OUTPUT INSERTED.ID INTO @tIntVar;
		SELECT TOP 1 @keyAssyRev = ID FROM @tIntVar;
	
	--************ END ID FOR asyRevs

	--Get asyChangeReasons.ID
	IF @strReason != '' 
		BEGIN
			INSERT INTO
				asyChangeReasons (strReason)
				VALUES (@strReason);
			SET @keyReasonForRev = @@IDENTITY;
		END
	-- END id for asyChangeReasons

	--Get the ID for the inserted/updated record in asyBOM
	DELETE FROM @tBigintVar

	DECLARE @tmp TABLE (keyTopLevelName bigint,
					keyAssyPN bigint,
					keyAssyRev int,
					intbomRev int)
	INSERT @tmp VALUES (@keyTopLevelName, @keyAssyPN, @keyAssyRev, @intbomRev)

	MERGE asyBOM AS t
	USING @tmp AS s
	ON
		t.keyTopLevelName = s.keyTopLevelName
		AND
		t.keyAssyPN = s.keyAssyPN
		AND
		t.keyAssyRev = s.keyAssyRev
		AND
		t.intbomRev = s.intbomrev
	WHEN MATCHED THEN
		UPDATE SET @ID = t.ID, 
					t.keyUploadedBy = @keyUploadedBy, 
					t.dtUploaded = GETDATE(), 
					@keyAssyStatus = t.keyAssyStatus, 
					t.keyAssyBU = @keyAssyBU, 
					t.keyReasonForRev = @keyReasonForRev
	WHEN NOT MATCHED THEN
		INSERT (keyTopLevelName, 
				keyAssyPN,
				keyAssyRev,
				keyUploadedBy,
				keyAssyStatus,
				keyAssyBU,
				intbomRev,
				keyReasonForRev)
		VALUES (@keyTopLevelName,
				@keyAssyPN,
				@keyAssyRev,
				@keyUploadedBy,
				@keyAssyStatus,
				@keyAssyBU,
				@intbomRev,
				@keyReasonForRev)
		OUTPUT INSERTED.ID INTO @tBigintVar;
		SELECT TOP 1 @ID = ID FROM @tBigintVar;
END

GO
