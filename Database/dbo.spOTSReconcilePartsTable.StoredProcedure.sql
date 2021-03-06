USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSReconcilePartsTable]    Script Date: 2/7/2017 11:15:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 2/5/2017
-- Description:	Reconciles otsParts with the changes specified by the user.
-- If @changed is -2, that indicates the Part Type did not exist in otsParts, and
-- therefore was not imported
-- =============================================
CREATE PROCEDURE [dbo].[spOTSReconcilePartsTable]
	@ID bigint,
	@strDescription varchar(300),
	@strDescription2 varchar(300),
	@strType varchar(150), 
	@strName varchar(50), --used to get keyRequestedBy
	@changed bit OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--temp table to hold changes
	DECLARE @tmpTbl table(olddesc varchar(300),
						  olddesc2 varchar(300),
						  oldkeyType int)
	--Old Values
	DECLARE @olddesc varchar(300);
	DECLARE @olddesc2 varchar(300);
	DECLARE @oldkeyType int;
	--Values that go in the history table
	DECLARE @descChange varchar(600);
	DECLARE @descChange2 varchar(600);
	DECLARE @keyTypeChange varchar(100);

	DECLARE @newVersion int;
	DECLARE @keyType int;
	DECLARE @keyRequestedBy bigint;

	SET @changed = 0;
	SET @keyType = -1;
    -- Insert statements for procedure here
	--Do an Upsert to get keyType, keyRequestedBy
	DECLARE @tblType TABLE (ID int)
	DECLARE @s TABLE (strText varchar(MAX))
	INSERT @s VALUES (@strType)
	MERGE otsPartType AS t
	USING @s as s
	ON s.strText = t.strType
	WHEN MATCHED THEN
		UPDATE SET @keyType = t.ID;
	--WHEN NOT MATCHED THEN
	--	INSERT (strType)
	--	VALUES (@strType)
	--	OUTPUT INSERTED.ID INTO @tblType;
	--	SELECT TOP 1 @keyType = ID FROM @tblType
	IF @keyType=-1
		BEGIN
			SET @changed=-2
			RETURN
		END
	--We have keyType; next keyRequestedBy
	DECLARE @tblReq TABLE (ID bigint)
	DELETE FROM @s
	INSERT @s VALUES (@strName)
	MERGE otsUsers AS t
	USING @s as s
	ON s.strText = t.strName
	WHEN MATCHED THEN
		UPDATE SET @keyRequestedBy = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strName)
		VALUES (@strName)
		OUTPUT INSERTED.ID INTO @tblType;
		SELECT TOP 1 @keyRequestedBy = ID FROM @tblReq
	--We have keyRequestedBy;
	UPDATE otsParts
	SET strDescription = @strDescription,
		strDescription2 = @strDescription2,
		keyType = @keyType,
		keyRequestedBy = @keyRequestedBy
	OUTPUT
		deleted.strDescription,
		deleted.strDescription2,
		deleted.keyType INTO @tmpTbl
	WHERE ID = @ID;

	SELECT TOP 1 
		@olddesc = olddesc,
		@olddesc2 = olddesc2,
		@oldkeyType = oldkeyType
	 FROM @tmpTbl;

	 SELECT * FROM @tmpTbl;

	 IF @strDescription = @olddesc AND @strDescription2 = @olddesc2 AND
	    @keyType = @oldkeyType
		--No change.
		BEGIN
			SELECT * FROM otsPNHistory WHERE 0=1
			RETURN
		END


	 --Find the change
	 IF @strDescription != @olddesc AND (@olddesc IS NOT NULL)
			SET @descChange = 'FROM[' + @olddesc + ']TO[' + @strDescription +']';
	 IF @strDescription2 != @olddesc2 AND (@olddesc2 IS NOT NULL)
			SET @descChange2 = 'FROM[' + @olddesc2 + ']TO[' + @strDescription2 +']';
	 IF @keyType != @oldkeyType AND (@oldkeyType IS NOT NULL)
		BEGIN
		 DECLARE @old VARCHAR(20);
		 DECLARE @new VARCHAR(20);
		 SELECT @old = strType FROM otsPartType WHERE ID = @oldkeyType;
		 SELECT @new = strType FROM otsPartType WHERE ID = @keyType;
		 SET @keyTypeChange = 'FROM[' + @old + ']TO[' + @new +']';
		END

	 --Since a change occurred, change the revision
	 DECLARE @intVersion int;
	 SELECT @intVersion = intVersion FROM otsParts WHERE ID = @ID;
	 SET @intVersion = @intVersion + 1;
	 --...and update database with the version change.
	 UPDATE otsParts
		SET intVersion = @intVersion WHERE ID = @ID;

	--And, finally, update the otsPNHistory Table
	INSERT INTO otsPNHistory
		(keyOTSPNID, 
		 keyUpdatedBy, 
		 Description_Change,
		 Description2_Change,
		 Part_Type_Change,
		 Part_SubType_Change)
	VALUES
		(@ID,
		7,
		@descChange,
		@descChange2,
		@keyTypeChange,
		'')

	--indicate a change occured
	set @changed = 1

	SELECT TOP 1 * FROM otsPNHistory WHERE keyOTSPNID = @ID ORDER BY Date_Updated DESC
END


GO
