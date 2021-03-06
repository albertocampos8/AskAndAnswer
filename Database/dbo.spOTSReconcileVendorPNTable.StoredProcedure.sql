USE [AskAndAnswer]
GO
/****** Object:  StoredProcedure [dbo].[spOTSReconcileVendorPNTable]    Script Date: 2/8/2017 11:03:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Alberto Campos
-- Create date: 11/5/2016
-- Description:	Reconciles otsVendorPN table with data imported from a file,
-- and makes an entry in otsVendorPNHistory, if needed.
-- Values for intReturnCode:
-- -1: Error occurred when evaluating whether or not a change will occur in the table
-- 0: otsVendorPN table and otsVendorPNHIstory table updated.
-- 1: otsVendorPN table was updated, but unable to update otsVendorPNHistory.
-- 2: No change occurred, so nothing was changed in the table.
-- =============================================
CREATE PROCEDURE [dbo].[spOTSReconcileVendorPNTable]
	@ID bigint, -- this is the Vendor PN ID
	@strVendorPartNumber varchar(300),
	@decLowVolCost decimal(18,6),
	@decHighVolCost decimal(18,6),
	@decEngCost decimal(18,6),
	@strDatasheetURL varchar(300),
	@strVendor varchar(300), --
	@strECodeName varchar(15), --
	@strStatus varchar(20), --
	@decMaxHeight decimal(8,3),
	@intReturnCode int output,
	@keyUpdatedBy bigint
AS
BEGIN
	LINENO 0
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--Preserve Vendor PN ID @ID, since @ID gets repurposed later
	DECLARE @vpnID bigint;
	SET @vpnID = @ID;
	--First, declare some variables to hold the existing values for the record:
	DECLARE @OLD_ID bigint
	DECLARE @OLD_strVendorPartNumber varchar(300)
	DECLARE @OLD_decLowVolCost decimal(18,6)
	DECLARE @OLD_decHighVolCost decimal(18,6)
	DECLARE @OLD_decEngCost decimal(18,6)
	DECLARE @OLD_strDatasheetURL varchar(300)
	DECLARE @OLD_keyVendorEnvironCode tinyint
	DECLARE @OLD_keyVendorPartStatus tinyint
	DECLARE @OLD_decMaxHeight decimal(8,3)

	DECLARE @keyVendor bigint
	DECLARE @keyVendorEnvironCode int
	DECLARE @keyVendorPartStatus int
	--Do an Upserts to get:
	--keyVendor
	DECLARE @tblBigint TABLE (ID bigint)
	DECLARE @s TABLE (strText varchar(MAX))
	INSERT @s VALUES (@strVendor)
	MERGE otsVendor AS t
	USING @s as s
	ON s.strText = t.strVendor
	WHEN MATCHED THEN
		UPDATE SET @keyVendor = t.ID
	WHEN NOT MATCHED THEN
		INSERT (strVendor)
		VALUES (@strVendor)
		OUTPUT INSERTED.ID INTO @tblBigint;
		SELECT TOP 1 @ID = ID FROM @tblBigint
	--keyVendorEnvironCode
	SET @keyVendorEnvironCode = 0;
	DECLARE @tblInt TABLE (ID int)
	DELETE FROM @s 
	INSERT @s VALUES (@strECodeName)
	MERGE otsEnvironCode AS t
	USING @s as s
	ON s.strText = t.strECodeName
	WHEN MATCHED THEN
		UPDATE SET @keyVendorEnvironCode = t.intECode;
	--WHEN NOT MATCHED THEN
		--DO NOT ALLOW DEFINIION OF NEW ECODE HERE!
	--	SET @keyVendorEnvironCode = -1;
	--keyPartStatus
	SET @keyVendorPartStatus = 0;
	DELETE FROM @tblInt
	DELETE FROM @s 
	INSERT @s VALUES (@strStatus)
	MERGE otsPartStatus AS t
	USING @s as s
	ON s.strText = t.strStatus
	WHEN MATCHED THEN
		UPDATE SET @keyVendorPartStatus = t.intStatusCode;
	--WHEN NOT MATCHED THEN
		--DO NOT ALLOW DEFINIION OF NEW ECODE HERE!
	--	SET @keyVendorPartStatus = -1;

	--First, get the old values associated with this VPNID
	--(Note the keyVendor Value value is the only one not stored in 
	--an @OLD_ variable, since it will not change, and is instead returned
	--as an ouput parameter to the calling procedure).
	SELECT 
		@OLD_strVendorPartNumber = strVendorPartNumber,
		@OLD_decLowVolCost = decLowVolCost,
		@OLD_decHighVolCost = decHighVolCost,
		@OLD_decEngCost = decEngCost,
		@OLD_strDatasheetURL = strDatasheetURL,
		@OLD_keyVendorEnvironCode = keyVendorEnvironCode,
		@OLD_keyVendorPartStatus = keyVendorPartStatus,
		@OLD_decMaxHeight = decMaxHeight,
		@keyVendor = keyVendor 
			FROM otsVendorPN 
			WHERE ID = @vpnID;
	--Declare variables to hold the changes.
	DECLARE @changed_strVendorPartNumber varchar(650)
	DECLARE @changed_decLowVolCost varchar(50)
	DECLARE @changed_decHighVolCost varchar(50)
	DECLARE @changed_decEngCost varchar(50)
	DECLARE @changed_strDatasheetURL varchar(650)
	DECLARE @changed_keyVendorEnvironCode varchar(50)
	DECLARE @changed_keyVendorPartStatus varchar(50)
	DECLARE @changed_decMaxHeight varchar(50)
	--Assume no change, then check
	DECLARE @changed tinyint
	SET @changed = 0;
	set @intReturnCode = -1;
	--Possibilites of change with nulls:
	--1) New Value is NULL, Old Value is Not NULL (user removed value)
	--2) New Value is Not NULL, Old VAlue WAS NULL (user defined a value)
	--3) Neither Value is NULL, but both values are different (user changed value)

	--The case where a change DID NOT OCCUR is therefore:
	--1) Neither Value is NULL, and both values are the same
	--2) Both Values are NULL.

	--We'll use the latter condition for comparison.
	IF NOT (
				(@strVendorPartNumber IS NULL
				AND
				@OLD_strVendorPartNumber IS NULL)
				OR
			   (@strVendorPartNumber IS NOT NULL 
				AND 
				@OLD_strVendorPartNumber IS NOT NULL
				AND
				@strVendorPartNumber = @OLD_strVendorPartNumber)
			)
		BEGIN
			SET @changed = 1
			SET @changed_strVendorPartNumber = 
				'FROM[' + @OLD_strVendorPartNumber + ']TO[' + @strVendorPartNumber +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @strVendorPartNumber, @OLD_strVendorPartNumber, @changed
	IF NOT (
				(@decLowVolCost IS NULL
				AND
				@OLD_decLowVolCost IS NULL)
				OR
			   (@decLowVolCost IS NOT NULL 
				AND 
				@OLD_decLowVolCost IS NOT NULL
				AND
				@decLowVolCost = @OLD_decLowVolCost)
			)
		BEGIN
			SET @changed = 2
			SET @changed_decLowVolCost = 
				'FROM[' + @OLD_decLowVolCost + ']TO[' + @decLowVolCost +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @decLowVolCost, @OLD_decLowVolCost, @changed
	IF NOT (
				(@decHighVolCost IS NULL
				AND
				@OLD_decHighVolCost IS NULL)
				OR
			   (@decHighVolCost IS NOT NULL 
				AND 
				@OLD_decHighVolCost IS NOT NULL
				AND
				@decHighVolCost = @OLD_decHighVolCost)
			)
		BEGIN
			SET @changed = 3
			SET @changed_decHighVolCost = 
				'FROM[' + @OLD_decHighVolCost + ']TO[' + @decHighVolCost +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @decHighVolCost, @OLD_decHighVolCost, @changed
	IF NOT (
				(@decEngCost IS NULL
				AND
				@OLD_decEngCost IS NULL)
				OR
			   (@decEngCost IS NOT NULL 
				AND 
				@OLD_decEngCost IS NOT NULL
				AND
				@decEngCost = @OLD_decEngCost)
			)
		BEGIN
			SET @changed = 4
			SET @changed_decEngCost = 
				'FROM[' + @OLD_decEngCost + ']TO[' + @decEngCost +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @decEngCost, @OLD_decEngCost, @changed
	IF NOT (
				(@strDatasheetURL IS NULL
				AND
				@OLD_strDatasheetURL IS NULL)
				OR
			   (@strDatasheetURL IS NOT NULL 
				AND 
				@OLD_strDatasheetURL IS NOT NULL
				AND
				@strDatasheetURL = @OLD_strDatasheetURL)
			)
		BEGIN
			SET @changed = 5
			SET @changed_strDatasheetURL = 
				'FROM[' + @OLD_strDatasheetURL + ']TO[' + @strDatasheetURL +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @strDatasheetURL, @OLD_strDatasheetURL, @changed
	IF NOT (
				(@decMaxHeight IS NULL
				AND
				@OLD_decMaxHeight IS NULL)
				OR
			   (@decMaxHeight IS NOT NULL 
				AND 
				@OLD_decMaxHeight IS NOT NULL
				AND
				@decMaxHeight = @OLD_decMaxHeight)
			)
		BEGIN
			SET @changed = 6
			SET @changed_decMaxHeight = 
				'FROM[' + @OLD_decMaxHeight + ']TO[' + @decMaxHeight +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @decMaxHeight, @OLD_decMaxHeight, @changed
	IF NOT (
				(@keyVendorPartStatus IS NULL
				AND
				@OLD_keyVendorPartStatus IS NULL)
				OR
			   (@keyVendorPartStatus IS NOT NULL 
				AND 
				@OLD_keyVendorPartStatus IS NOT NULL
				AND
				@keyVendorPartStatus = @OLD_keyVendorPartStatus)
			)
		BEGIN
			SET @changed = 7
			DECLARE @oldkvps varchar(50)
			DECLARE @newkvps varchar(50)
			SELECT @oldkvps = strStatus FROM otsPartStatus 
				WHERE ID = @OLD_keyVendorPartStatus
			SELECT @newkvps = strStatus FROM otsPartStatus 
				WHERE ID = @keyVendorPartStatus
			SET @changed_keyVendorPartStatus = 
				'FROM[' + @oldkvps + ']TO[' + @newkvps +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @newkvps, @oldkvps, @changed
	IF NOT (
				(@keyVendorEnvironCode IS NULL
				AND
				@OLD_keyVendorEnvironCode IS NULL)
				OR
			   (@keyVendorEnvironCode IS NOT NULL 
				AND 
				@OLD_keyVendorEnvironCode IS NOT NULL
				AND
				@keyVendorEnvironCode = @OLD_keyVendorEnvironCode)
			)
		BEGIN
			SET @changed = 8
			DECLARE @oldkvec varchar(50)
			DECLARE @newkvec varchar(50)
			SELECT @oldkvec = strECodeName FROM otsEnvironCode 
				WHERE ID = @OLD_keyVendorEnvironCode
			SELECT @newkvec = strECodeName FROM otsEnvironCode
				WHERE ID = @keyVendorEnvironCode
			SET @changed_keyVendorEnvironCode = 
				'FROM[' + @oldkvec + ']TO[' + @newkvec +']';
		END
	IF @@ERROR>0
		RETURN
	SELECT @newkvec, @oldkvec, @changed
	IF @changed = 0
		--No change; stop
		BEGIN
			SET @intReturnCode = 2
			RETURN
		END
				
	--otherwise, proceed with the update.
	--first, get the current version
	DECLARE @version int;
	SELECT @version = intVersion FROM otsVendorPN WHERE ID = @vpnID;
	SET @version = @version +1;
	UPDATE otsVendorPN
		SET
			strVendorPartNumber = @strVendorPartNumber,
			decLowVolCost = @decLowVolCost,
			decHighVolCost = @decHighVolCost,
			decEngCost = @decEngCost,
			strDatasheetURL = @strDatasheetURL,
			keyVendorEnvironCode = @keyVendorEnvironCode,
			keyVendorPartStatus = @keyVendorPartStatus,
			decMaxHeight = @decMaxHeight,
			intVersion = @version
		WHERE
			ID = @vpnID
	SET @intReturnCode = 1

	--Make an entry in the history table.
	INSERT INTO otsVendorPNHistory
		(keyVPNID,
		keyUpdatedBy,
		Part_Status_Change,
		Vendor_Part_Number_Change,
		Low_Vol_Cost_Change,
		High_Vol_Cost_Change,
		Eng_Cost_Change,
		Max_Height_Change,
		Environmental_Code_Change,
		Datasheet_Change,
		Version)
	VALUES
		(@ID,
		@keyUpdatedBy,
		@changed_keyVendorPartStatus,
		@changed_strVendorPartNumber,
		@changed_decLowVolCost,
		@changed_decHighVolCost,
		@changed_decEngCost,
		@changed_decMaxHeight,
		@changed_keyVendorEnvironCode,
		@changed_strDatasheetURL,
		@version)
	
	SET @intReturnCode = 0	


END



GO
